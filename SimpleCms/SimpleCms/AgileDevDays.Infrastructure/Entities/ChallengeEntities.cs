using System.Data.Entity;
using AgileDevDays.Infrastructure.Models;

namespace AgileDevDays.Infrastructure.Entities
{
    public class ChallengeEntities : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var applicationConfig = modelBuilder.Entity<ChallengeEntity>();
            applicationConfig.ToTable("Site_Challenge");

        }

        public DbSet<ChallengeEntity> ImageEntitys { get; set; }
    }
}