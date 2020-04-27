using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Context;
using WebApp.Models;
using WebApp.StorageModels;

namespace WebApp.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly CulturalHubContext _culturalHubContext;

        public EventsRepository(CulturalHubContext culturalHubContext)
        {
            _culturalHubContext = culturalHubContext;
        }

        public Event GetEventById(string eventId)
        {
            var e = _culturalHubContext.Events.Find(eventId);
            if (e.Deleted != null) return null;

            return new Event(e.Id,
                            e.Title,
                            e.Description,
                            new Location(e.LocationAddress, (LocationType)e.LocationType),
                            e.StartsAt,
                            e.Duration,
                            (EventType)e.Type,
                            (Audience)e.Audience,
                            e.PublishDate,
                            e.IsActive);
        }

        public List<Event> GetEvents()
        {
            return _culturalHubContext.Events
                .Where(e => e.Deleted == null)
                .Select(e =>
                        new Event(e.Id,
                        e.Title, e.Description,
                        new Location(e.LocationAddress, (LocationType)e.LocationType),
                        e.StartsAt, e.Duration, (EventType)e.Type, (Audience)e.Audience,
                        e.PublishDate, e.IsActive)
                )
                .ToList();
        }

        public Event AddEvent(EventStorageModel e)
        {
            e.Id = Guid.NewGuid().ToString();
            _culturalHubContext.Events.Add(e);

            _culturalHubContext.SaveChanges();

            return new Event(e.Id,
                        e.Title, e.Description,
                        new Location(e.LocationAddress, (LocationType)e.LocationType),
                        e.StartsAt, e.Duration, (EventType)e.Type, (Audience)e.Audience,
                        e.PublishDate, e.IsActive);
        }

        public void EditEvent(EventStorageModel e)
        {
            var eDB = _culturalHubContext.Events.Find(e.Id);

            eDB.Title = e.Title;
            eDB.StartsAt = e.StartsAt;
            eDB.Description = e.Description;
            eDB.LocationAddress = e.LocationAddress;
            eDB.LocationType = e.LocationType;
            eDB.Duration = e.Duration;
            eDB.Audience = e.Audience;
            eDB.Type = e.Type;
            eDB.PublishDate = e.PublishDate;
            eDB.IsActive = e.IsActive;

            _culturalHubContext.Events.Update(eDB);

            _culturalHubContext.SaveChanges();
        }

        public void DeleteEvent(EventStorageModel e)
        {
            var eDB = _culturalHubContext.Events.Find(e.Id);

            _culturalHubContext.Events.Remove(eDB);

            _culturalHubContext.SaveChanges();
        }
    }
}
