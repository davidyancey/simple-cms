using System;
using System.Collections.Generic;

namespace AgileDevDays.Core.ImageGallery
{
    public class Gallery
    {
        public int GalleryId { get; set; }

        public string GalleryName { get; set; }
        public Guid UserId { get; set; }
        public List<Image> Images { get; set; }
    }
}