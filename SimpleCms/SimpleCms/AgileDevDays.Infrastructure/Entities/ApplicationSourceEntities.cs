﻿using System.Data.Entity;
using AgileDevDays.Core.Models;
using AgileDevDays.Infrastructure.Models;

namespace AgileDevDays.Infrastructure.Entities
{
    public class ApplicationSourceEntities : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var applicationConfig = modelBuilder.Entity<ApplicationEntity>();
            applicationConfig.ToTable("aspnet_Applications");

            var authenticationConfig = modelBuilder.Entity<AuthenticationSourceEntity>();
            authenticationConfig.ToTable("aspnet_AuthenticationSources");
        }

        public DbSet<ApplicationEntity> Applications { get; set; }
        public DbSet<AuthenticationSourceEntity> AuthenticationSources { get; set; }
    }
}