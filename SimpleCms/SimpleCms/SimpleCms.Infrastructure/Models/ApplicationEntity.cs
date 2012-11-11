using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCms.Infrastructure.Models
{
    public class ApplicationEntity
    {
        public string ApplicationName { get; set; }
        public string LoweredApplicationName { get; set; }
        [Key]
        public Guid ApplicationId { get; set; }
        public string Description { get; set; }
    }
}