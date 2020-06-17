using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Repository.SQL;
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

            return new Event(new EventId(e.Id),
                new ClientId(e.ClientId),
                new EventTitle(e.Title),
                new EventDescription(e.Description),
                new Location(e.LocationAddress, (LocationType) e.LocationType),
                new EventDate(e.StartsAt.Year, e.StartsAt.Month, e.StartsAt.Day, e.StartsAt.Hour, e.StartsAt.Minute),
                new EventDate(e.EndsAt.Year, e.EndsAt.Month, e.EndsAt.Day, e.EndsAt.Hour, e.EndsAt.Minute),
                (EventType) e.Type,
                (Audience) e.Audience,
                new EventPublishDate(e.PublishDate),
                e.IsActive);
        }

        //public IEnumerable<Event> GetEvents()
        //{
        //    return _culturalHubContext.Events
        //        .Where(e => e.Deleted == null)
        //        .Select(e =>
        //            new Event(new EventId(e.Id),
        //                new ClientId(e.ClientId),
        //                new EventTitle(e.Title),
        //                new EventDescription(e.Description),
        //                new Location(e.LocationAddress, (LocationType) e.LocationType),
        //                new EventDate(e.StartsAt.Year, e.StartsAt.Month, e.StartsAt.Day, e.StartsAt.Hour,
        //                    e.StartsAt.Minute),
        //                new EventDate(e.EndsAt.Year, e.EndsAt.Month, e.EndsAt.Day, e.EndsAt.Hour, e.EndsAt.Minute),
        //                (EventType) e.Type,
        //                (Audience) e.Audience,
        //                new EventPublishDate(e.PublishDate),
        //                e.IsActive)
        //        );
        //}

        //public List<Event> GetEvents()
        //{
        //    return _culturalHubContext.Events
        //        .Where(e => e.Deleted == null)
        //        .Select(e =>
        //            new Event(new EventId(e.Id),
        //                new ClientId(e.ClientId),
        //                new EventTitle(e.Title),
        //                new EventDescription(e.Description),
        //                new Location(e.LocationAddress, (LocationType) e.LocationType),
        //                new EventDate(e.StartsAt.Year, e.StartsAt.Month, e.StartsAt.Day, e.StartsAt.Hour,
        //                    e.StartsAt.Minute),
        //                new EventDate(e.EndsAt.Year, e.EndsAt.Month, e.EndsAt.Day, e.EndsAt.Hour, e.EndsAt.Minute),
        //                (EventType) e.Type,
        //                (Audience) e.Audience,
        //                new EventPublishDate(e.PublishDate),
        //                e.IsActive)
        //        )
        //        .ToList();
        //}

        public Event AddEvent(Event e)
        {
            var eventStorageModel = new EventStorageModel
            {
                Id = e.Id.Value,
                ClientId = e.ClientId.Value,
                Title = e.Title.TitleValue,
                StartsAt = e.StartsAt.Value,
                Description = e.Description.DescriptionValue,
                EndsAt = e.EndsAt.Value,
                Audience = (int) e.Audience,
                Type = (int) e.Type,
                PublishDate = e.PublishDate.Value,
                IsActive = e.IsActive,
                LocationAddress = e.Location.Address,
                LocationType = (int) e.Location.Type
            };

            _culturalHubContext.Events.Add(eventStorageModel);

            _culturalHubContext.SaveChanges();

            return e;
        }

        public void EditEvent(Event e)
        {
            var eDB = _culturalHubContext.Events.Find(e.Id.Value);

            eDB.Title = e.Title.TitleValue;
            eDB.StartsAt = e.StartsAt.Value;
            eDB.Description = e.Description.DescriptionValue;
            eDB.LocationAddress = e.Location.Address;
            eDB.LocationType = (int) e.Location.Type;
            eDB.EndsAt = e.EndsAt.Value;
            eDB.Audience = (int) e.Audience;
            eDB.Type = (int) e.Type;
            eDB.PublishDate = e.PublishDate.Value;
            eDB.IsActive = e.IsActive;

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

        public void AddToFavorites(Guid userId, string id)
        {
            var favoritesEntity = _culturalHubContext
                .FavoriteEvents
                .Single(x => x.UserId == userId.ToString());

            favoritesEntity.AddFavorite(id);

            _culturalHubContext.Update(favoritesEntity);

            _culturalHubContext.SaveChanges();
        }
    }
}