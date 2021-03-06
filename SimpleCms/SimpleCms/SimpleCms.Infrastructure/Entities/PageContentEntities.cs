using System.Data.Entity;
using SimpleCms.Infrastructure.Models;

namespace SimpleCms.Infrastructure.Entities
{
    public class PageContentEntities : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var applicationConfig = modelBuilder.Entity<PageEntity>();
            applicationConfig.ToTable("Site_Page");

            var authenticationConfig = modelBuilder.Entity<PageContentEntity>();
            authenticationConfig.ToTable("Site_PageContent");
        }

        public DbSet<PageEntity> PageEntitys { get; set; }
        public DbSet<PageContentEntity> PageContentEntitys { get; set; }
        
    }
}