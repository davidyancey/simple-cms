using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCms.Infrastructure.Models
{
    public class GalleryEntity
    {
        [Key]
        public int GalleryId { get; set; }
        public Guid ApplicationId { get; set; }
        public string GalleryName { get; set; }
        public Guid UserId { get; set; }
    }
}