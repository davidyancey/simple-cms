using System;
using System.ComponentModel.DataAnnotations;


namespace SimpleCms.Infrastructure.Models
{
    public class ImageEntity
    {
        public bool IsPrimary { get; set; }
        public Guid UserId { get; set; }

        [Key]
        public int ImageId { get; set; }
        public byte[] ThumbNail { get; set; }
        public byte[] FullImage { get; set; }
        public string ImageUrl { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Alt { get; set; }
        public int GalleryID { get; set; }
        public string ImageFormat { get; set; }
        public string ImageName { get; set; }
    }
}