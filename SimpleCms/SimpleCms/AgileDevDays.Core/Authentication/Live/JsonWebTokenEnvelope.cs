using System.Runtime.Serialization;

namespace AgileDevDays.Core.Authentication.Live
{
    [DataContract]
    public class JsonWebTokenEnvelope
    {
        [DataMember(Name = "typ")]
        public string Type
        {
            get;
            private set;
        }

        [DataMember(Name = "alg")]
        public string Algorithm
        {
            get;
            private set;
        }

        [DataMember(Name = "kid")]
        public int KeyId
        {
            get;
            private set;
        }
    }
}