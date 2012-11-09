using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgileDevDays.Core.Models
{
    public class ProfileEntity
    {
        public System.Guid UserId { get; set; }

        [Key]
        public int ProfileId { get; set; }

        public List<Core.Models.ImageEntity> ProfileImages { get; set; }
    }
}