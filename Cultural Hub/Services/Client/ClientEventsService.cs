using Domain;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Repositories;


namespace Services.Client
{
    public class ClientEventsService
    {
        private IEventsRepository _eventsRepository;
        private IPicturesRepository _picturesRepository;

        public ClientEventsService(
            IEventsRepository eventsRepository,
            IPicturesRepository picturesRepository
            )
        {
            _eventsRepository = eventsRepository;
            _picturesRepository = picturesRepository;
        }

        public List<ClientEventDetails> GetClientEventDetailsList()
        {
            var eventDetailsList = _eventsRepository.GetEvents().Select(e =>
            {
                var eventDetails = new ClientEventDetails()
                {
                    Id = e.Id.IdValue,
                    Title = e.Title.TitleValue,
                    StartsAt = e.StartsAt.StartDateValue,
                    LocationAddress = e.Location.Address,
                    LocationType = e.Location.Type.ToString(),
                    Description = e.Description.DescriptionValue,
                    Duration = e.Duration.DurationValue,
                    Audience = e.Audience.ToString(),
                    Type = e.Type.ToString(),
                    Pictures = _picturesRepository.GetPicturesForEvent(e.Id.IdValue).Select(p => p.Link).ToList()
                };

                return eventDetails;
            }).ToList();

            return eventDetailsList;
        }

        public List<ClientEventShortInfo> GetClientEventShortInfoList()
        {
            var eventShortInfoList = _eventsRepository.GetEvents().Select(e =>
            {
                var eventShortInfo = new ClientEventShortInfo()
                {
                    Id = e.Id.IdValue,
                    Title = e.Title.TitleValue,
                    StartsAt = e.StartsAt.StartDateValue,
                    PublishDate=e.PublishDate.PublishDateValue,
                    IsActive=e.IsActive,
                    IsPublished=e.IsPublished,
                    Pictures = _picturesRepository.GetPicturesForEvent(e.Id.IdValue).Select(p => p.Link).ToList()
                };

                return eventShortInfo;
            }).ToList();

            return eventShortInfoList;
        }

        public CrudEvent GetCrudEventViewModelById(string eventId)
        {
            var e = _eventsRepository.GetEventById(eventId);

            var crudEvent = new CrudEvent()
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Location=e.Location,
                Audience = e.Audience,
                Duration = e.Duration,
                Type = e.Type,
                PublishDate = e.PublishDate,
                IsActive = e.IsActive,
                StartsAt = e.StartsAt,
                Pictures = _picturesRepository.GetPicturesForEvent(e.Id.IdValue).Select(p => p.Link).ToList()
            };

            return crudEvent;
        }

        public ClientEventDetails GetClientEventDetailsById(string eventId)
        {
            var e = _eventsRepository.GetEventById(eventId);

            var eventDetails = new ClientEventDetails()
            {
                Id = e.Id.IdValue,
                Title = e.Title.TitleValue,
                StartsAt = e.StartsAt.StartDateValue,
                LocationAddress = e.Location.Address,
                LocationType = e.Location.Type.ToString(),
                Description = e.Description.DescriptionValue,
                Duration = e.Duration.DurationValue,
                Audience = e.Audience.ToString(),
                Type = e.Type.ToString(),
                PublishDate=e.PublishDate.PublishDateValue,
                IsActive=e.IsActive,
                IsPublished=e.IsPublished,
                Pictures = _picturesRepository.GetPicturesForEvent(e.Id.IdValue).Select(p => p.Link).ToList()
            };

            return eventDetails;
        }

        public CrudEvent AddEvent(CrudEvent crudEvent)
        {
            // Create Event 
            var e = new Event(crudEvent.Id,
                            crudEvent.Title,
                            crudEvent.Description,
                            new Location(crudEvent.Location.Address, crudEvent.Location.Type),
                            crudEvent.StartsAt,
                            new EventDuration(crudEvent.Duration.DurationValue),
                            crudEvent.Type,
                            crudEvent.Audience,
                            crudEvent.PublishDate,
                            crudEvent.IsActive
                            );


            var eFromDB = _eventsRepository.AddEvent(e);
            crudEvent.Id.IdValue = eFromDB.Id.IdValue;

            // Create Pictures 
            var pictures = crudEvent.Pictures
                .Where(p => p != null)
                .Select(p => new Picture(eFromDB.Id.IdValue, null, p))
                .ToList();

            _picturesRepository.AddPicturesToEvent(pictures);

            return crudEvent;
        }


        public void EditEvent(CrudEvent crudEvent)
        {
            // Update Event 
            var e = new Event(crudEvent.Id,
                crudEvent.Title,
                crudEvent.Description,
                new Location(crudEvent.Location.Address, crudEvent.Location.Type),
                crudEvent.StartsAt,
                new EventDuration(crudEvent.Duration.DurationValue),
                crudEvent.Type,
                crudEvent.Audience,
                crudEvent.PublishDate,
                crudEvent.IsActive);

            _eventsRepository.EditEvent(e);

            // Update Pictures 
            _picturesRepository.DeleteAllPicturesFromEvent(e.Id.IdValue);

            var pictures = crudEvent.Pictures
                .Where(p => p != null)
                .Select(p => new Picture(e.Id.IdValue, null, p))
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
}

