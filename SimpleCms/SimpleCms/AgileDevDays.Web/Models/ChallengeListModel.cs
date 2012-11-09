using System.Collections.Generic;

namespace AgileDevDays.Web.Models
{
    public class ChallengeListModel : ContentModel 
    {
        public Dictionary<string, List<ChallengeModel>> Challenges { get; set; }
    }
}