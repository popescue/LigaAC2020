using Domain;
using Microsoft.EntityFrameworkCore;
using WebApp.StorageModels;

namespace WebApp.Context
{
    public class CulturalHubContext : DbContext
    {
        public CulturalHubContext(DbContextOptions<CulturalHubContext> options) : base(options)
        {

        }

        public DbSet<EventStorageModel> Events { get; set; }
        public DbSet<PictureStorageModel> Pictures { get; set; }
  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventStorageModel>().HasKey(e => e.Id);
            modelBuilder.Entity<PictureStorageModel>().HasKey(p => p.Id);
        }
    }
}
