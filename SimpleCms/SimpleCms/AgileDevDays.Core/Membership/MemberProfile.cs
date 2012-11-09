using System.Web.Security;
using AgileDevDays.Core.ImageGallery;

namespace AgileDevDays.Core.Membership
{
    public class MemberProfile : MembershipUser
    {
        public Image ProfileImage { get; set; }
    }
}