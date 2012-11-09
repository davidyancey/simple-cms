using System.ComponentModel.DataAnnotations;

namespace AgileDevDays.Core.Models
{
    public class AuthenticationSourceEntity
    {
        public System.Guid UserId { get; set; }
        public string AuthenticationSource { get; set; }
        public System.Guid ApplicationId { get; set; }
        [Key]
        public System.Guid AuthenticationSourceID { get; set; }
    }
}