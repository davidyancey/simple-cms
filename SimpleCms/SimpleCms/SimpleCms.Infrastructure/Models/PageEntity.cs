using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCms.Infrastructure.Models
{
    public class PageEntity
    {
        [Key]
        public int PageId { get; set; }
        public Guid ApplicationId { get; set; }
        public string SectionName { get; set; }
        public string AreaName { get; set; }
        public DateTime createDate { get; set; }
    }
}