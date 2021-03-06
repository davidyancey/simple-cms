﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCms.Infrastructure.Models
{
    public class PageContentEntity
    {
        [Key]
        public int ContentId { get; set; }

        public int PageId { get; set; }
        public string ContentBody { get; set; }
        public string ContentTitle { get; set; }
        public Guid ContentAuthor { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}