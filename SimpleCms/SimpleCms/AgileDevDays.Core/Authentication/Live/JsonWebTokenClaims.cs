using System;
using System.Runtime.Serialization;

namespace AgileDevDays.Core.Authentication.Live
{
    [DataContract]
    public class JsonWebTokenClaims
    {

        [DataMember(Name = "exp")]
        private int ExpUnixTime
        {
            get;
            set;
        }

        private DateTime? _expiration = null;
        public DateTime Expiration
        {
            get
            {
                if (this._expiration == null)
                {
                    this._expiration = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(ExpUnixTime);
                }

                return (DateTime)this._expiration;
            }
        }

        [DataMember(Name = "iss")]
        public string Issuer
        {
            get;
            private set;
        }

        [DataMember(Name = "aud")]
        public string Audience
        {
            get;
            private set;
        }

        [DataMember(Name = "uid")]
        public string UserId
        {
            get;
            private set;
        }

        [DataMember(Name = "ver")]
        public int Version
        {
            get;
            private set;
        }

        [DataMember(Name = "urn:microsoft:appurl")]
        public string ClientIdentifier
        {
            get;
            private set;
        }
    }
}