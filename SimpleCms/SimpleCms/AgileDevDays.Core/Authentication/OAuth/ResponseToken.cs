namespace AgileDevDays.Core.Authentication.OAuth
{
    public class ResponseToken
    {
        public string Token { get; set; }
        public string TokenSecret { get; set; }
        public decimal UserId { get; set; }
        public string ScreenName { get; set; }
        public string VerificationString { get; set; }
    }
}