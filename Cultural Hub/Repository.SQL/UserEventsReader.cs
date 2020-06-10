using System;
using System.Linq;
using System.Collections.Generic;
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
            var eventsWihPictures = from e in _culturalHubContext.Events
                //join p in _culturalHubContext.Pictures on e.Id equals p.EventId into ps
                select new EventWithPictures
                {
                    Id = e.Id,
                    Client = e.ClientId,
                    Title = e.Title,
                    StartsAt = e.StartsAt,
                    LocationAddress = e.LocationAddress,
                    //Pictures = ps.Select(x => new SimplePicture
                    //{
                    //    Description = x.Description,
                    //    Link = x.Link
                    //})
                };

            return eventsWihPictures.ToList();
        }
    }
}