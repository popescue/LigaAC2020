using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using Domain;

namespace WebApp.Data
{
    public class CulturalHubContext: DbContext
    {
        public CulturalHubContext(DbContextOptions<CulturalHubContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().ToTable("DbEvent");
            
        }

    }
}
