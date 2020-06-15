using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.StorageModels;

namespace Repository.SQL
{
    public class CulturalHubContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public CulturalHubContext(DbContextOptions<CulturalHubContext> options) : base(options)
        {
        }

        public DbSet<EventStorageModel> Events { get; set; }
        public DbSet<PictureStorageModel> Pictures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EventStorageModel>().HasKey(e => e.Id);
            //modelBuilder.Entity<EventStorageModel>().Property(x => x.Id).HasMaxLength(5).IsFixedLength();

            modelBuilder.Entity<EventStorageModel>()
                .HasMany(x => x.Pictures)
                .WithOne(x => x.Event);

            modelBuilder.Entity<PictureStorageModel>().HasKey(p => p.Id);
        }
    }
}