using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Runtime.Serialization.Json;
using System.Text;
using System.IO;

namespace AgileDevDays.Core.Authentication.Live
{

    // Reference: http://tools.ietf.org/search/draft-jones-json-web-token-00
    //
    // JWT is made up of 3 parts: Envelope, Claims, Signature.
    // - Envelope - specifies the token type and signature algorithm used to produce 
    //              signature segment.  This is in JSON format.
    // - Claims - specifies claims made by the token.  This is in JSON format.
    // - Signature - Cryptographic signature use to maintain data integrity.
    // 
    // To produce a JWT token:
    // 1. Create Envelope segment in JSON format.
    // 2. Create Claims segment in JSON format.
    // 3. Create signature.
    // 4. Base64url encode each part and append together separated by ".". 



    public class JsonWebToken
    {
      #region Properties

        private static readonly DataContractJsonSerializer ClaimsJsonSerializer = new DataContractJsonSerializer(typeof(JsonWebTokenClaims));
        private static readonly DataContractJsonSerializer EnvelopeJsonSerializer = new DataContractJsonSerializer(typeof(JsonWebTokenEnvelope));
        private static readonly UTF8Encoding Utf8Encoder = new UTF8Encoding(true, true);
        private static readonly SHA256Managed Sha256Provider = new SHA256Managed();

        private readonly string _claimsTokenSegment;
        public JsonWebTokenClaims Claims
        {
            get;
            private set;
        }

        private readonly string _envelopeTokenSegment;
        public JsonWebTokenEnvelope Envelope
        {
            get;
            private set;
        }

        public string Signature
        {
            get;
            private set;
        }

        public bool IsExpired
        {
            get
            {
                return this.Claims.Expiration < DateTime.Now;
            }
        }

        #endregion

        #region Constructors
        public JsonWebToken(string token, Dictionary<int, string> keyIdsKeys)
        {
            // Get the token segments and perform validation.
            string[] tokenSegments = this.SplitToken(token);

            // Decode and deserialize the claims.
            this._claimsTokenSegment = tokenSegments[1];
            this.Claims = this.GetClaimsFromTokenSegment(this._claimsTokenSegment);

            // Decode and deserialize the envelope.
            this._envelopeTokenSegment = tokenSegments[0];
            this.Envelope = this.GetEnvelopeFromTokenSegment(this._envelopeTokenSegment);

            // Get the signature.
            this.Signature = tokenSegments[2];

            // Ensure that the tokens KeyId exists in the secret keys list.
            if (!keyIdsKeys.ContainsKey(this.Envelope.KeyId))
            {
                throw new Exception(string.Format("Could not find key with id {0}.", this.Envelope.KeyId));
            }

            // Validation
            this.ValidateEnvelope(this.Envelope);
            this.ValidateSignature(keyIdsKeys[this.Envelope.KeyId]);
        }

        private JsonWebToken()
        {
        }
        #endregion

        #region Parsing Methods

        private JsonWebTokenClaims GetClaimsFromTokenSegment(string claimsTokenSegment)
        {
            byte[] claimsData = this.Base64UrlDecode(claimsTokenSegment);
            using (var memoryStream = new MemoryStream(claimsData))
            {
                return ClaimsJsonSerializer.ReadObject(memoryStream) as JsonWebTokenClaims;
            }
        }

        private JsonWebTokenEnvelope GetEnvelopeFromTokenSegment(string envelopeTokenSegment)
        {
            byte[] envelopeData = this.Base64UrlDecode(envelopeTokenSegment);
            using (var memoryStream = new MemoryStream(envelopeData))
            {
                return EnvelopeJsonSerializer.ReadObject(memoryStream) as JsonWebTokenEnvelope;
            }
        }

        private string[] SplitToken(string token)
        {
            // Expected token format: Envelope.Claims.Signature

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Token is empty or null.");
            }

            string[] segments = token.Split('.');

            if (segments.Length != 3)
            {
                throw new Exception("Invalid token format. Expected Envelope.Claims.Signature.");
            }

            if (string.IsNullOrEmpty(segments[0]))
            {
                throw new Exception("Invalid token format. Envelope must not be empty.");
            }

            if (string.IsNullOrEmpty(segments[1]))
            {
                throw new Exception("Invalid token format. Claims must not be empty.");
            }

            if (string.IsNullOrEmpty(segments[2]))
            {
                throw new Exception("Invalid token format. Signature must not be empty.");
            }

            return segments;
        }

        #endregion

        #region Validation Methods

        private void ValidateEnvelope(JsonWebTokenEnvelope envelope)
        {
            if (envelope.Type != "JWT")
            {
                throw new Exception("Unsupported token type.");
            }

            if (envelope.Algorithm != "HS256")
            {
                throw new Exception("Unsupported crypto algorithm.");
            }
        }

        private void ValidateSignature(string key)
        {
            // Derive signing key, Signing key = SHA256(secret + "JWTSIG")
            byte[] bytes = Utf8Encoder.GetBytes(key + "JWTSig");
            byte[] signingKey = Sha256Provider.ComputeHash(bytes);

            // To Validate:
            // 
            // 1. Take the bytes of the UTF-8 representation of the JWT Claim
            //    Segment and calculate an HMAC SHA-256 MAC on them using the
            //    shared key.
            //
            // 2. Base64url encode the previously generated HMAC as defined in this
            //    document.
            //
            // 3. If the JWT Crypto Segment and the previously calculated value
            //    exactly match in a character by character, case sensitive
            //    comparison, then one has confirmation that the key was used to
            //    generate the HMAC on the JWT and that the contents of the JWT
            //    Claim Segment have not be tampered with.
            //
            // 4. If the validation fails, the token must be rejected.

            // UFT-8 representation of the JWT envelope.claim segment.
            byte[] input = Utf8Encoder.GetBytes(this._envelopeTokenSegment + "." + this._claimsTokenSegment);

            // Calculate an HMAC SHA-256 MAC.
            using (HMACSHA256 hashProvider = new HMACSHA256(signingKey))
            {
                byte[] myHashValue = hashProvider.ComputeHash(input);

                // Base64 url encode the hash.
                string base64urlEncodedHash = this.Base64UrlEncode(myHashValue);

                // Now compare the two hash values.
                if (base64urlEncodedHash != this.Signature)
                {
                    throw new Exception("Signature does not match.");
                }
            }
        }

        #endregion

        #region Base64 Encode / Decode Functions
        // Reference: http://tools.ietf.org/search/draft-jones-json-web-token-00

        public byte[] Base64UrlDecode(string encodedSegment)
        {
            string s = encodedSegment;
            s = s.Replace('-', '+'); // 62nd char of encoding.
            s = s.Replace('_', '/'); // 63rd char of encoding.
            switch (s.Length % 4) // Pad with trailing '='s.
            {
                case 0: break; // No pad chars in this case.
                case 2: s += "=="; break; // Two pad chars.
                case 3: s += "="; break; // One pad char.
                default: throw new System.Exception("Illegal base64url string");
            }
            return Convert.FromBase64String(s); // Standard base64 decoder.
        }

        public string Base64UrlEncode(byte[] arg)
        {
            string s = Convert.ToBase64String(arg); // Standard base64 encoder.
            s = s.Split('=')[0]; // Remove any trailing '='s.
            s = s.Replace('+', '-'); // 62nd char of encoding.
            s = s.Replace('/', '_'); // 63rd char of encoding.
            return s;
        }
        #endregion
    }
}