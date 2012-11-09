using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AgileDevDays.Web.Models
{
    public class ContentModel
    {
        public int ContentId { get; set; }
        public int PageId { get; set; }
        public string Title { get; set; }
        public DateTime PostedOn { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public bool ShowEdit { get; set; }

        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Content { get; set; }
    }
}