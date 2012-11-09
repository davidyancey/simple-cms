namespace AgileDevDays.Web.Models
{
    public class ImageModel
    {
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