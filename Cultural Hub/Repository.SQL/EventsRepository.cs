using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Context;
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

        public Event AddEvent(Event e)
        {
            var eventStorageModel = new EventStorageModel()
            {
                Id = e.Id,
                Title = e.Title,
                StartsAt = e.StartsAt,
                Description = e.Description,
                Duration = e.Duration,
                Audience = (int)e.Audience,
                Type = (int)e.Type,
                PublishDate = e.PublishDate,
                IsActive = e.IsActive,
                LocationAddress = e.Location.Address,
                LocationType = (int)e.Location.Type
            };

            _culturalHubContext.Events.Add(eventStorageModel);

            _culturalHubContext.SaveChanges();

            return new Event(e.Id,
                        e.Title, e.Description,
                        new Location(e.Location.Address, e.Location.Type),
                        e.StartsAt, e.Duration, (EventType)e.Type, (Audience)e.Audience,
                        e.PublishDate, e.IsActive);
        }

        public void EditEvent(Event e)
        {
            var eventStorageModel = new EventStorageModel()
            {
                Title = e.Title,
                StartsAt = e.StartsAt,
                Description = e.Description,
                Duration = e.Duration,
                Audience = (int)e.Audience,
                Type = (int)e.Type,
                PublishDate = e.PublishDate,
                IsActive = e.IsActive,
                LocationAddress = e.Location.Address,
                LocationType = (int)e.Location.Type
            };

            var eDB = _culturalHubContext.Events.Find(eventStorageModel.Id);

            eDB.Title = eventStorageModel.Title;
            eDB.StartsAt = eventStorageModel.StartsAt;
            eDB.Description = eventStorageModel.Description;
            eDB.LocationAddress = eventStorageModel.LocationAddress;
            eDB.LocationType = eventStorageModel.LocationType;
            eDB.Duration = eventStorageModel.Duration;
            eDB.Audience = eventStorageModel.Audience;
            eDB.Type = eventStorageModel.Type;
            eDB.PublishDate = eventStorageModel.PublishDate;
            eDB.IsActive = eventStorageModel.IsActive;

            _culturalHubContext.Events.Update(eDB);

            _culturalHubContext.SaveChanges();
        }

        public void DeleteEvent(string eventId)
        {
            var eDB = _culturalHubContext.Events.Find(eventId);
            eDB.Deleted = DateTime.Now;

            _culturalHubContext.Events.Update(eDB);

            _culturalHubContext.SaveChanges();
        }
    }
}
