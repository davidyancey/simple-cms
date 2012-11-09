using System.Data.Entity;
using AgileDevDays.Core.Models;

namespace AgileDevDays.Infrastructure.Entities
{
    public class ImageGalleryEntities : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var applicationConfig = modelBuilder.Entity<ImageEntity>();
            applicationConfig.ToTable("Site_Image");

            var authenticationConfig = modelBuilder.Entity<GalleryEntity>();
            authenticationConfig.ToTable("Site_Gallery");
        }

        public DbSet<ImageEntity> ImageEntitys { get; set; }
        public DbSet<GalleryEntity> GalleryEntitys { get; set; }
    }
}