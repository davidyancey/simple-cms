using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleCms.Infrastructure.Models
{
    public class ProfileEntity
    {
        public System.Guid UserId { get; set; }

        [Key]
        public int ProfileId { get; set; }

        public List<ImageEntity> ProfileImages { get; set; }
    }
}