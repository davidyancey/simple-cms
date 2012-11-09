using System;
using System.ComponentModel.DataAnnotations;

namespace AgileDevDays.Core.Models
{
    public class GalleryEntity
    {
        [Key]
        public int GalleryId { get; set; }

        public string GalleryName { get; set; }
        public Guid UserId { get; set; }
    }
}