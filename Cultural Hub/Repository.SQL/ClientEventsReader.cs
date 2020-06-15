using System;
using System.Linq;
using System.Collections.Generic;
using Services.Client;
using Services.User;

namespace Repository.SQL
{
    public class ClientEventsReader : IClientEventsReader
    {
        private readonly CulturalHubContext _culturalHubContext;

        public ClientEventsReader(CulturalHubContext culturalHubContext)
        {
            _culturalHubContext = culturalHubContext;
        }

        public IEnumerable<EventWithPictures> GetEvents(Guid clientId)
        {
            var eventsWihPictures = from e in _culturalHubContext.Events.AsEnumerable()
                join p in _culturalHubContext.Pictures on e.Id equals p.EventId into ps
                where e.ClientId == clientId
                select new EventWithPictures
                {
                    Id = e.Id,
                    Client = e.ClientId,
                    Title = e.Title,
                    StartsAt = e.StartsAt,
                    LocationAddress = e.LocationAddress,
                    Pictures = ps.Select(x => new SimplePicture
                    {
                        Description = x.Description,
                        Link = x.Link
                    })
                };

            return eventsWihPictures.ToList();
        }
    }
}