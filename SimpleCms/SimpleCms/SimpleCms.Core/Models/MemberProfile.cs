using System.Web.Security;
using SimpleCms.Core.Models;

namespace SimpleCms.Core.Membership
{
    public class MemberProfile : MembershipUser
    {
        public Image ProfileImage { get; set; }
    }
}