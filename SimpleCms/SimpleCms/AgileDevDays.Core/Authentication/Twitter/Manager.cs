using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using AgileDevDays.Core.Authentication.OAuth;
using AgileDevDays.Core.Common;
using AgileDevDays.Core.Enumerations;

namespace AgileDevDays.Core.Authentication.Twitter
{
    public class Manager
    {
        public ResponseToken GetRequestToken(string consumerKey, string consumerSecret, string callbackAddress)
        {
            if (string.IsNullOrEmpty(consumerKey))
            {
                throw new ArgumentNullException("consumerKey");
            }

            if (string.IsNullOrEmpty(consumerSecret))
            {
                throw new ArgumentNullException("consumerSecret");
            }

            if (string.IsNullOrEmpty(callbackAddress))
            {
                throw new ArgumentNullException("callbackAddress", @"You must always provide a callback url when obtaining a request token. For PIN-based authentication, use ""oob"" as the callback url.");
            }

            var builder = new WebRequestUtility("Twitter API",
                new Uri("https://api.twitter.com/oauth/request_token"),
                HttpVerb.Post,
                new AccessTokens { ConsumerKey = consumerKey, ConsumerSecret = consumerSecret },
                false,
                "");

            if (!string.IsNullOrEmpty(callbackAddress))
            {
                builder.Parameters.Add("oauth_callback", callbackAddress);
            }

            string responseBody = null;

            try
            {
                HttpWebResponse webResponse = builder.ExecuteRequest();
                Stream responseStream = webResponse.GetResponseStream();
                if (responseStream != null) responseBody = new StreamReader(responseStream).ReadToEnd();
            }
            catch (WebException wex)
            {
                throw new ArgumentException(wex.Message, wex);
            }

            return new ResponseToken
            {
                Token = QueryString.Parse("oauth_token", responseBody),
                TokenSecret = QueryString.Parse("oauth_token_secret", responseBody),
                VerificationString = QueryString.Parse("oauth_verifier", responseBody)
            };
        }

        public ResponseToken GetAccessToken(string consumerKey, string consumerSecret, string requestToken, string verifier)
        {
            if (string.IsNullOrEmpty(consumerKey))
            {
                throw new ArgumentNullException("consumerKey");
            }

            if (string.IsNullOrEmpty(consumerSecret))
            {
                throw new ArgumentNullException("consumerSecret");
            }

            if (string.IsNullOrEmpty(requestToken))
            {
                throw new ArgumentNullException("requestToken");
            }

            WebRequestUtility builder = new WebRequestUtility("Twitter API",
                new Uri("https://api.twitter.com/oauth/access_token"),
                HttpVerb.Get,
                new AccessTokens{ ConsumerKey = consumerKey, ConsumerSecret = consumerSecret },
                false);

            if (!string.IsNullOrEmpty(verifier))
            {
                builder.Parameters.Add("oauth_verifier", verifier);
            }

            builder.Parameters.Add("oauth_token", requestToken);

            string responseBody;

            try
            {
                HttpWebResponse webResponse = builder.ExecuteRequest();

                responseBody = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
            }
            catch (WebException wex)
            {
                throw new ArgumentException(wex.Message, wex);
            }

            var response = new ResponseToken
                                {
                                    Token = Regex.Match(responseBody, @"oauth_token=([^&]+)").Groups[1].Value,
                                    TokenSecret = Regex.Match(responseBody, @"oauth_token_secret=([^&]+)").Groups[1].Value,
                                    UserId = long.Parse(Regex.Match(responseBody, @"user_id=([^&]+)").Groups[1].Value,CultureInfo.CurrentCulture),
                                    ScreenName = Regex.Match(responseBody, @"screen_name=([^&]+)").Groups[1].Value
                                };
            return response;
        }

        public ResponseToken GetAccessToken(string consumerKey, string consumerSecret, string requestToken, string verifier, WebProxy proxy)
        {
            if (string.IsNullOrEmpty(consumerKey))
            {
                throw new ArgumentNullException("consumerKey");
            }

            if (string.IsNullOrEmpty(consumerSecret))
            {
                throw new ArgumentNullException("consumerSecret");
            }

            if (string.IsNullOrEmpty(requestToken))
            {
                throw new ArgumentNullException("requestToken");
            }

            var builder = new WebRequestUtility("Twitter API",
                new Uri("https://api.twitter.com/oauth/access_token"),
                HttpVerb.Get,
                new AccessTokens{ ConsumerKey = consumerKey, ConsumerSecret = consumerSecret },
                false,
                "");

            builder.Proxy = proxy;

            if (!string.IsNullOrEmpty(verifier))
            {
                builder.Parameters.Add("oauth_verifier", verifier);
            }

            builder.Parameters.Add("oauth_token", requestToken);

            string responseBody;

            try
            {
                HttpWebResponse webResponse = builder.ExecuteRequest();
                Stream stream = webResponse.GetResponseStream();
                if (stream != null) responseBody = new StreamReader(stream).ReadToEnd();
                else
                {
                    throw new WebException("Web Response Stream is NULL");
                }
            }
            catch (WebException wex)
            {
                throw new ArgumentException(wex.Message, wex);
            }

            var response = new ResponseToken
                               {
                                   Token = Regex.Match(responseBody, @"oauth_token=([^&]+)").Groups[1].Value,
                                   TokenSecret =
                                       Regex.Match(responseBody, @"oauth_token_secret=([^&]+)").Groups[1].Value,
                                   UserId =
                                       long.Parse(Regex.Match(responseBody, @"user_id=([^&]+)").Groups[1].Value,
                                                  CultureInfo.CurrentCulture),
                                   ScreenName = Regex.Match(responseBody, @"screen_name=([^&]+)").Groups[1].Value
                               };
            return response;
        }

        public Uri BuildAuthorizationUri(string requestToken)
        {
            return BuildAuthorizationUri(requestToken, false);
        }
        public Uri BuildAuthorizationUri(string requestToken, bool authenticate)
        {
            var parameters = new StringBuilder("https://twitter.com/oauth/");

            if (authenticate)
            {
                parameters.Append("authenticate");
            }
            else
            {
                parameters.Append("authorize");
            }

            parameters.AppendFormat("?oauth_token={0}", requestToken);

            return new Uri(parameters.ToString());
        }

        public ResponseToken GetAccessTokenDuringCallback(string consumerKey, string consumerSecret)
        {
            HttpContext context = HttpContext.Current;
            if (context == null || context.Request == null)
            {
                throw new ApplicationException("Could not located the HTTP context. GetAccessTokenDuringCallback can only be used in ASP.NET applications.");
            }

            string requestToken = context.Request.QueryString["oauth_token"];
            string verifier = context.Request.QueryString["oauth_verifier"];

            if (string.IsNullOrEmpty(requestToken))
            {
                throw new ApplicationException("Could not locate the request token.");
            }

            if (string.IsNullOrEmpty(verifier))
            {
                throw new ApplicationException("Could not locate the verifier value.");
            }

            return GetAccessToken(consumerKey, consumerSecret, requestToken, verifier);
        }
    }
}