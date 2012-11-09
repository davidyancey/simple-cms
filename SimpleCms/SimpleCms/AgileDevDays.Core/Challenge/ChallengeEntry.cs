using System;

namespace AgileDevDays.Core.Challenge
{
    public class ChallengeEntry
    {
        public int ChallengeId { get; set; }
        public string ChallengeTitle { get; set; }
        public Guid AuthorId { get; set; }
        public string ChallengeLocation { get; set; }
        public string ChallengeType { get; set; }
    }
}