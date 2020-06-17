using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        public DbSet<FavoriteEventListStorage> FavoriteEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EventStorageModel>().HasKey(e => e.Id);
            //modelBuilder.Entity<EventStorageModel>().Property(x => x.Id).HasMaxLength(5).IsFixedLength();

            modelBuilder.Entity<EventStorageModel>()
                .HasMany(x => x.Pictures)
                .WithOne(x => x.Event);

            modelBuilder.Entity<PictureStorageModel>().HasKey(p => p.Id);

            modelBuilder.Entity<FavoriteEventListStorage>().HasKey(x => x.UserId);

            modelBuilder.Entity<FavoriteEventListStorage>().HasOne<IdentityUser>(x => x.User);
        }
    }

    public class FavoriteEventListStorage
    {
        public string UserId { get; set; }

        public string EventListJson { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }

        public void AddFavorite(string id)
        {
            var theList = JsonConvert.DeserializeObject<List<string>>(EventListJson);

            theList.Add(id);

            EventListJson = JsonConvert.SerializeObject(theList);
        }

        [NotMapped] public List<string> EventList => JsonConvert.DeserializeObject<List<string>>(EventListJson);
    }
}