using System;

namespace AgileDevDays.Web.Models
{
    public class ChallengeModel
    {
        public int ChallengeId { get; set; }
        public string ChallengeTitle { get; set; }
        public string AuthorName { get; set; }
        public string ChallengeLocation { get; set; }
        public string ChallengeType { get; set; }
    }
}