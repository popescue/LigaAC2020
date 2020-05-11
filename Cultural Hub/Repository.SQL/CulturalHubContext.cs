using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.StorageModels;

namespace WebApp.Context
{
    public class CulturalHubContext : IdentityDbContext<IdentityUser>
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
            modelBuilder.Entity<PictureStorageModel>().HasKey(p => p.Id);
        }
    }
}
