using System;
using System.ComponentModel.DataAnnotations;

namespace AgileDevDays.Infrastructure.Models
{
    public class ChallengeEntity
    {
        [Key]
        public int ChallengeId { get; set; }
        public string ChallengeTitle { get; set; }
        public Guid AuthorId { get; set; }
        public string ChallengeLocation { get; set; }
        public string ChallengeType { get; set; }
    }
}