using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Services.User;
using WebApp.Repositories;

namespace Services.Client
{
    public class ClientEventsService
    {
        private readonly IClientEventsReader _eventsReader;
        private readonly IEventsRepository _eventsRepository;
        private readonly IPicturesRepository _picturesRepository;

        public ClientEventsService(
            IEventsRepository eventsRepository,
            IClientEventsReader eventsReader,
            IPicturesRepository picturesRepository
        )
        {
            _eventsRepository = eventsRepository;
            _eventsReader = eventsReader;
            _picturesRepository = picturesRepository;
        }

        public IEnumerable<UserEventShortInfo> GetEventShortInfoList(Guid clientId)
        {
            var eventsShortInfo = _eventsReader.GetEvents(clientId)
                .Select(e => new UserEventShortInfo
                {
                    ClientId = e.Client,
                    Title = e.Title,
                    StartsAt = e.StartsAt,
                    Pictures = e.Pictures.Select(x => new Uri(x.Link)),
                    Id = e.Id,
                    LocationAddress = e.LocationAddress
                }).ToList();

            return eventsShortInfo;
        }

        public CrudEvent GetCrudEventViewModelById(string eventId)
        {
            var e = _eventsRepository.GetEventById(eventId);

            var crudEvent = new CrudEvent
            {
                Id = e.Id.Value,
                ClientId = e.ClientId.Value,
                Title = e.Title.TitleValue,
                StartsAt = e.StartsAt.Value,
                Address = e.Location.Address,
                LocationType = e.Location.Type,
                Description = e.Description.DescriptionValue,
                EndsAt = e.EndsAt.Value,
                Audience = e.Audience,
                Type = e.Type,
                Pictures = _picturesRepository.GetPicturesForEvent(e.Id.Value).Select(p => p.Link).ToList(),
                IsActive = e.IsActive,
                PublishDate = e.PublishDate.Value
            };

            return crudEvent;
        }

        public ClientEventDetails GetClientEventDetailsById(string eventId)
        {
            var e = _eventsRepository.GetEventById(eventId);

            var eventDetails = new ClientEventDetails
            {
                Id = e.Id.Value,
                ClientId = e.ClientId.Value,
                Title = e.Title.TitleValue,
                StartsAt = e.StartsAt.Value,
                LocationAddress = e.Location.Address,
                LocationType = e.Location.Type.ToString(),
                Description = e.Description.DescriptionValue,
                EndsAt = e.EndsAt.Value,
                Audience = e.Audience.ToString(),
                Type = e.Type.ToString(),
                Pictures = _picturesRepository.GetPicturesForEvent(e.Id.Value).Select(p => p.Link).ToList()
            };
            return eventDetails;
        }

        public CrudEvent AddEvent(CrudEvent crudEvent)
        {
            // Create Event 
            var e = new Event(new EventId(Guid.NewGuid().ToString().Substring(31)),
                new ClientId(crudEvent.ClientId),
                new EventTitle(crudEvent.Title),
                new EventDescription(crudEvent.Description),
                new Location(crudEvent.Address, crudEvent.LocationType),
                new EventDate(crudEvent.StartsAt.Year, crudEvent.StartsAt.Month, crudEvent.StartsAt.Day,
                    crudEvent.StartsAt.Hour, crudEvent.StartsAt.Minute),
                new EventDate(crudEvent.EndsAt.Year, crudEvent.EndsAt.Month, crudEvent.EndsAt.Day,
                    crudEvent.EndsAt.Hour, crudEvent.EndsAt.Minute),
                crudEvent.Type,
                crudEvent.Audience,
                new EventPublishDate(crudEvent.PublishDate),
                crudEvent.IsActive);

            var eFromDB = _eventsRepository.AddEvent(e);
            crudEvent.Id = eFromDB.Id.Value;

            // Create Pictures 
            var pictures = crudEvent.Pictures
                .Where(p => p != null)
                .Select(p => new Picture(new EventId(crudEvent.Id), null, p))
                .ToList();

            _picturesRepository.AddPicturesToEvent(pictures);

            return crudEvent;
        }

        public void EditEvent(CrudEvent crudEvent)
        {
            // Update Event 
            var e = new Event(new EventId(crudEvent.Id),
                new ClientId(crudEvent.ClientId),
                new EventTitle(crudEvent.Title),
                new EventDescription(crudEvent.Description),
                new Location(crudEvent.Address, crudEvent.LocationType),
                new EventDate(crudEvent.StartsAt.Year, crudEvent.StartsAt.Month, crudEvent.StartsAt.Day,
                    crudEvent.StartsAt.Hour, crudEvent.StartsAt.Minute),
                new EventDate(crudEvent.EndsAt.Year, crudEvent.EndsAt.Month, crudEvent.EndsAt.Day,
                    crudEvent.EndsAt.Hour, crudEvent.EndsAt.Minute),
                crudEvent.Type,
                crudEvent.Audience,
                new EventPublishDate(crudEvent.PublishDate),
                crudEvent.IsActive);

            _eventsRepository.EditEvent(e);

            // Update Pictures 
            _picturesRepository.DeleteAllPicturesFromEvent(e.Id.ToString());

            var pictures = crudEvent.Pictures
                .Where(p => p != null)
                .Select(p => new Picture(e.Id.ToString(), null, p))
                .ToList();

            _picturesRepository.AddPicturesToEvent(pictures);
        }

        public void DeleteEvent(string eventId)
        {
            // Delete Event 
            _eventsRepository.DeleteEvent(eventId);

            // Delete Pictures 
            _picturesRepository.SoftDeleteAllPicturesFromEvent(eventId);
        }
    }

    public interface IClientEventsReader
    {
        IEnumerable<EventWithPictures> GetEvents(Guid clientId);
    }
}