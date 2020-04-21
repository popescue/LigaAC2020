using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Context
{
    public class CulturalHubContext : DbContext
    {
        public CulturalHubContext(DbContextOptions<CulturalHubContext> options) : base(options)
        {
            
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasKey(e => e.Id);
            modelBuilder.Entity<Picture>().HasNoKey();
            modelBuilder.Entity<Location>().HasNoKey();
        }

    }
}
