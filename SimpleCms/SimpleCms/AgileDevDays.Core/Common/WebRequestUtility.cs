using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using AgileDevDays.Core.Authentication.OAuth;
using AgileDevDays.Core.Enumerations;


namespace AgileDevDays.Core.Common
{
    public class WebRequestUtility
    {
        private byte[] formData = null;
        public string Realm;
        public Uri RequestUri { get; set; }
        public Dictionary<string, object> Parameters { get; private set; }
        public HttpVerb Verb { get; set; }
        public AccessTokens Tokens { private get; set; }
        public Boolean KeepAlive { private get; set; }
        public String UserAgent { private get; set; }
        public NetworkCredential NetworkCredentials { private get; set; }
        public bool Multipart { get; set; }
        public WebProxy Proxy { get; set; }

        public bool UseOAuth { get; private set; }
        

        public WebRequestUtility(string realm, Uri requestUri, HttpVerb verb, Boolean keepAlive, String userAgent, NetworkCredential networkCredentials)
        {
            if (requestUri == null)
                throw new ArgumentNullException("requestUri");

            this.Realm = realm;
            this.RequestUri = requestUri;
            this.Verb = verb;
            this.KeepAlive = keepAlive;
            this.UserAgent = userAgent;
            this.UseOAuth = false;
            if (networkCredentials != null)
                this.NetworkCredentials = networkCredentials;

            this.Parameters = new Dictionary<string, object>();

            if (string.IsNullOrEmpty(this.RequestUri.Query)) return;

            foreach (Match item in Regex.Matches(this.RequestUri.Query, @"(?<key>[^&?=]+)=(?<value>[^&?=]+)"))
            {
                this.Parameters.Add(item.Groups["key"].Value, item.Groups["value"].Value);
            }

            this.RequestUri = new Uri(this.RequestUri.AbsoluteUri.Replace(this.RequestUri.Query, ""));
        }


        public WebRequestUtility(string realm, Uri requestUri, HttpVerb verb, AccessTokens tokens, Boolean keepAlive = false, String userAgent = "")
            : this(realm, requestUri, verb, keepAlive, userAgent, null)
        {
            this.Tokens = tokens;

            if (tokens != null)
            {
                if (string.IsNullOrEmpty(this.Tokens.ConsumerKey) || string.IsNullOrEmpty(this.Tokens.ConsumerSecret))
                {
                    throw new ArgumentException("Consumer key and secret are required for OAuth requests.");
                }

                if (string.IsNullOrEmpty(this.Tokens.AccessToken) ^ string.IsNullOrEmpty(this.Tokens.AccessTokenSecret))
                {
                    throw new ArgumentException("The access token is invalid. You must specify the key AND secret values.");
                }

                this.UseOAuth = true;
            }
        }

        /// <summary>
        /// Executes the request.
        /// </summary>
        /// <returns></returns>
        public HttpWebResponse ExecuteRequest()
        {
            HttpWebRequest request = PrepareRequest();

            return (HttpWebResponse)request.GetResponse();
        }

        public HttpWebRequest PrepareRequest()
        {
            Authentication.OAuth.Utility.SetupOAuth(Multipart, Verb, UseOAuth, Parameters, Tokens, RequestUri);

            formData = null;
            string contentType = string.Empty;

            if (!Multipart)
            {	//We don't add the paramters to the query if we are multipart-ing
                AddQueryStringParametersToUri();
            }
            else
            {
                string dataBoundary = "--------------------r4nd0m";
                contentType = "multipart/form-data; boundary=" + dataBoundary;

                formData = GetMultipartFormData(Parameters, dataBoundary);

                this.Verb = HttpVerb.Post;
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.RequestUri);

            if (this.Proxy != null)
                request.Proxy = Proxy;
            request.KeepAlive = KeepAlive;

            if (!this.UseOAuth && this.NetworkCredentials != null)
            {
                request.Credentials = this.NetworkCredentials;
                request.UseDefaultCredentials = false;
            }
            else
            {
                request.UseDefaultCredentials = true;
            }

            request.Method = this.Verb.ToString();

            request.ContentLength = Multipart ? formData.Length : 0;


            request.UserAgent = (String.IsNullOrEmpty(UserAgent)) ? string.Format(CultureInfo.InvariantCulture, "Twitterizer/{0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version) : UserAgent;

            request.ServicePoint.Expect100Continue = false;
            if (this.UseOAuth)
            {
                request.Headers.Add("Authorization", Authentication.OAuth.Utility.GenerateAuthorizationHeader(Realm, Multipart, Parameters, RequestUri, Tokens, Verb));
            }

            if (Multipart)
            {	//Parameters are not added to the query string, post them in the request body instead

                request.ContentType = contentType;

                request.BeginGetRequestStream((res) =>
                {
                    res.AsyncWaitHandle.WaitOne();

                    using (Stream requestStream = request.EndGetRequestStream(res))
                    {
                        requestStream.Write(formData, 0, formData.Length);
                    }
                }, null);
            }

            return request;
        }
        private void AddQueryStringParametersToUri()
        {
            StringBuilder requestParametersBuilder = new StringBuilder(this.RequestUri.AbsoluteUri);
            requestParametersBuilder.Append(this.RequestUri.Query.Length == 0 ? "?" : "&");


            Dictionary<string, object> fieldsToInclude = new Dictionary<string, object>(this.Parameters.Where(p => !Authentication.OAuth.Common.OAuthParametersToIncludeInHeader.Contains(p.Key) &&
                                         !Authentication.OAuth.Common.SecretParameters.Contains(p.Key)).ToDictionary(p => p.Key, p => p.Value));

            foreach (KeyValuePair<string, object> item in fieldsToInclude)
            {
                if (item.Value.GetType() == typeof(string))
                    requestParametersBuilder.AppendFormat("{0}={1}&", item.Key, UrlUtility.UrlEncode(item.Value as string));
            }

            if (requestParametersBuilder.Length == 0)
                return;

            requestParametersBuilder.Remove(requestParametersBuilder.Length - 1, 1);

            this.RequestUri = new Uri(requestParametersBuilder.ToString());
        }

        private byte[] GetMultipartFormData(Dictionary<string, object> param, string boundary)
        {
            Stream formDataStream = new MemoryStream();
            Encoding encoding = Encoding.UTF8;

            Dictionary<string, object> fieldsToInclude = new Dictionary<string, object>(param.Where(p => !Authentication.OAuth.Common.OAuthParametersToIncludeInHeader.Contains(p.Key) &&
                             !Authentication.OAuth.Common.SecretParameters.Contains(p.Key)).ToDictionary(p => p.Key, p => p.Value));

            foreach (KeyValuePair<string, object> kvp in fieldsToInclude)
            {
                if (kvp.Value.GetType() == typeof(byte[]))
                {	//assume this to be a byte stream
                    byte[] data = kvp.Value as byte[];

                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\";\r\nContent-Type: application/octet-stream\r\n\r\n",
                        boundary,
                        kvp.Key,
                        kvp.Key);
                    formDataStream.Write(encoding.GetBytes(header), 0, header.Length);
                    formDataStream.Write(data, 0, data.Length);
                }
                else
                {	//this is normal text data
                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n",
                        boundary,
                        kvp.Key,
                        kvp.Value);
                    formDataStream.Write(encoding.GetBytes(header), 0, header.Length);
                }
            }

            string footer = string.Format("\r\n--{0}--\r\n", boundary);
            formDataStream.Write(encoding.GetBytes(footer), 0, footer.Length);
            formDataStream.Position = 0;
            byte[] returndata = new byte[formDataStream.Length];

            formDataStream.Read(returndata, 0, returndata.Length);
            formDataStream.Close();

            return returndata;
        }
    }
}
