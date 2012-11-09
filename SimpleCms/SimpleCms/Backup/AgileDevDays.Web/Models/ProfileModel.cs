using System.Web.Security;

namespace AgileDevDays.Web.Models
{
    public class ProfileModel : MembershipUser
    {
        public ImageModel ProfileImage { get; set; }
    }
}