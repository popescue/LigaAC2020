using System;
using System.Linq;
using System.Collections.Generic;
using Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Services.User;

namespace Repository.SQL
{
    public class UserEventsReader : IUserEventsReader
    {
        private readonly CulturalHubContext _culturalHubContext;

        public UserEventsReader(CulturalHubContext culturalHubContext)
        {
            _culturalHubContext = culturalHubContext;
        }

        public IEnumerable<EventWithPictures> GetEvents()
        {
            return _culturalHubContext.Events.Include(e => e.Pictures)
                .Select(e => new EventWithPictures
                {
                    Id = e.Id,
                    Client = e.ClientId,
                    Title = e.Title,
                    StartsAt = e.StartsAt,
                    LocationAddress = e.LocationAddress,
                    Pictures = e.Pictures.Select(x => new SimplePicture
                    {
                        Description = x.Description,
                        Link = x.Link
                    })
                }).ToList();
        }

        public IEnumerable<EventWithPictures> GetFavoriteEvents(Guid userId)
        {
            var userFavoriteEventList = _culturalHubContext
                .FavoriteEvents
                .Single(x => x.UserId == userId.ToString())
                .EventList;

            return _culturalHubContext.Events.Include(e => e.Pictures)
                .Where(e => userFavoriteEventList.Contains(e.Id))
                .Select(e => new EventWithPictures
                {
                    Id = e.Id,
                    Client = e.ClientId,
                    Title = e.Title,
                    StartsAt = e.StartsAt,
                    LocationAddress = e.LocationAddress,
                    Pictures = e.Pictures.Select(x => new SimplePicture
                    {
                        Description = x.Description,
                        Link = x.Link
                    })
                }).ToList();
        }
    }
}