using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using AgileDevDays.Core.Common;
using AgileDevDays.Core.Enumerations;

namespace AgileDevDays.Core.Authentication.OAuth
{
    public class Utility
    {
        private static string GenerateSignature(bool multipart, Dictionary<string, object> parameters, Uri requestUri, AccessTokens tokens, HttpVerb verb )
        {
            IEnumerable<KeyValuePair<string, object>> nonSecretParameters = null;

            if (multipart)
            {
                nonSecretParameters = (from p in parameters
                                       where (!Common.SecretParameters.Contains(p.Key) && p.Key.StartsWith("oauth_"))
                                       select p);
            }
            else
            {
                nonSecretParameters = (from p in parameters
                                       where (!Common.SecretParameters.Contains(p.Key))
                                       select p);
            }

            Uri urlForSigning = requestUri;

            // Create the base string. This is the string that will be hashed for the signature.
            string signatureBaseString = string.Format(
                CultureInfo.InvariantCulture,
                "{0}&{1}&{2}",
                verb.ToString().ToUpper(CultureInfo.InvariantCulture),
                UrlUtility.UrlEncode(UrlUtility.NormalizeUrl(urlForSigning)),
                UrlUtility.UrlEncode(nonSecretParameters));

            // Create our hash key (you might say this is a password)
            string key = string.Format(
                CultureInfo.InvariantCulture,
                "{0}&{1}",
                UrlUtility.UrlEncode(tokens.ConsumerSecret),
                UrlUtility.UrlEncode(tokens.AccessTokenSecret));


            // Generate the hash
            HMACSHA1 hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            byte[] signatureBytes = hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(signatureBaseString));
            return Convert.ToBase64String(signatureBytes);
        }

        /// <summary>
        /// Generates the authorization header.
        /// </summary>
        /// <returns>The string value of the HTTP header to be included for OAuth requests.</returns>
        public static string GenerateAuthorizationHeader(string realm, bool multipart, Dictionary<string, object> parameters, Uri requestUri, AccessTokens tokens, HttpVerb verb)
        {
            StringBuilder authHeaderBuilder = new StringBuilder();
            authHeaderBuilder.AppendFormat("OAuth realm=\"{0}\"", realm);

            var sortedParameters = from p in parameters
                                   where Common.OAuthParametersToIncludeInHeader.Contains(p.Key)
                                   orderby p.Key, UrlUtility.UrlEncode((p.Value.GetType() == typeof(string)) ? p.Value as string : "")
                                   select p;

            foreach (var item in sortedParameters)
            {
                authHeaderBuilder.AppendFormat(
                    ",{0}=\"{1}\"",
                    UrlUtility.UrlEncode(item.Key),
                    UrlUtility.UrlEncode(item.Value as string));
            }

            authHeaderBuilder.AppendFormat(",oauth_signature=\"{0}\"", UrlUtility.UrlEncode(parameters["oauth_signature"] as string));

            return authHeaderBuilder.ToString();
        }

        public static void SetupOAuth(bool multipart, HttpVerb verb, bool useOAuth, Dictionary<string, object> parameters, AccessTokens tokens, Uri requestUri)
        {
            // We only sign oauth requests
            if (!useOAuth)
            {
                return;
            }

            // Add the OAuth parameters
            parameters.Add("oauth_version", "1.0");
            parameters.Add("oauth_nonce", Core.Common.Utility.GenerateNonce());
            parameters.Add("oauth_timestamp", Core.Common.Utility.GenerateTimeStamp());
            parameters.Add("oauth_signature_method", "HMAC-SHA1");
            parameters.Add("oauth_consumer_key", tokens.ConsumerKey);
            parameters.Add("oauth_consumer_secret", tokens.ConsumerSecret);

            if (!string.IsNullOrEmpty(tokens.AccessToken))
            {
                parameters.Add("oauth_token", tokens.AccessToken);
            }

            if (!string.IsNullOrEmpty(tokens.AccessTokenSecret))
            {
                parameters.Add("oauth_token_secret", tokens.AccessTokenSecret);
            }

            string signature = GenerateSignature(multipart, parameters, requestUri, tokens, verb);

            // Add the signature to the oauth parameters
            parameters.Add("oauth_signature", signature);
        }
    }
}