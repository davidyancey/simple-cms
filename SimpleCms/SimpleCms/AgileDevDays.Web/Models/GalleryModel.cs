using System;
using System.Collections.Generic;

namespace AgileDevDays.Web.Models
{
    public class GalleryModel
    {
        public int GalleryId { get; set; }

        public string GalleryName { get; set; }
        public Guid UserId { get; set; }
        public List<ImageModel> Images { get; set; }
    }
}