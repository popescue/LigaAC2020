using Domain;
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

        public ClientEventsService( IEventsRepository eventsRepository, IPicturesRepository picturesRepository)
        {
            _eventsRepository = eventsRepository;
            _picturesRepository = picturesRepository;
        }

        public List<ClientEventDetails> GetClientEventDetailsList()
        {
            var eventDetailsViewModels = _eventsRepository.GetEvents().Select(e =>
            {
                var eventDetailsViewModel = new ClientEventDetails()
                {
                    Id = e.Id.IdValue,
                    Title = e.Title.TitleValue,
                    StartsAt = e.StartsAt.StartDateValue,
                    LocationAddress = e.Location.Address,
                    LocationType = e.Location.Type.ToString(),
                    Description = e.Description.DescriptionValue,
                    Duration = e.Duration.DurationValue,
                    PublishDate = e.PublishDate.PublishDateValue,
                    IsPublished = e.IsPublished,
                    IsActive=e.IsActive,
                    Audience = e.Audience.ToString(),
                    Type = e.Type.ToString(),
                    Pictures = _picturesRepository.GetPicturesForEvent(e.Id.IdValue).Select(p => p.Link).ToList()
                };

                return eventDetailsViewModel;

            }).ToList();

            return eventDetailsViewModels;
        }

        public List<ClientEventShortInfo> GetClientEventShortInfoList()
        {
            var eventShortInfoViewModels = _eventsRepository.GetEvents().Select(e =>
            {
                var eventShortInfoViewModel = new ClientEventShortInfo()
                {
                    Id = e.Id.IdValue,
                    Title = e.Title.TitleValue,
                    StartsAt = e.StartsAt.StartDateValue,
                    PublishDate=e.PublishDate.PublishDateValue,
                    IsActive =e.IsActive,
                    IsPublished=e.IsPublished,
                    Pictures = _picturesRepository.GetPicturesForEvent(e.Id.IdValue).Select(p => p.Link).ToList()
                };

                return eventShortInfoViewModel;
            }).ToList();

            return eventShortInfoViewModels;
        }

        public ClientEventDetails GetEventDetailsById(string eventId)
        {
            var e = _eventsRepository.GetEventById(eventId);

            var eventDetailsViewModel = new ClientEventDetails()
            {
                Id = e.Id.IdValue,
                Title = e.Title.TitleValue,
                StartsAt = e.StartsAt.StartDateValue,
                LocationAddress = e.Location.Address,
                LocationType = e.Location.Type.ToString(),
                Description = e.Description.DescriptionValue,
                Duration = e.Duration.DurationValue,
                Audience = e.Audience.ToString(),
                PublishDate = e.PublishDate.PublishDateValue,
                IsPublished = e.IsPublished,
                IsActive=e.IsActive,
                Type = e.Type.ToString(),
                Pictures = _picturesRepository.GetPicturesForEvent(e.Id.IdValue).Select(p => p.Link).ToList()
            };

            return eventDetailsViewModel;
        }
        public CrudEvent GetCrudEventViewModelById(string eventId)
        {
            var e = _eventsRepository.GetEventById(eventId);

            var crudEvent = new CrudEvent()
            {
                Id = e.Id,
                Title = e.Title,
                StartsAt = e.StartsAt,
                Address = e.Location.Address,
                LocationType = e.Location.Type,
                Description = e.Description,
                Duration = e.Duration,
                Audience = e.Audience,
                PublishDate = e.PublishDate,
                IsPublished = e.IsPublished,
                IsActive = e.IsActive,
                Type = e.Type,
                Pictures = _picturesRepository.GetPicturesForEvent(e.Id.IdValue).Select(p => p.Link).ToList()
            };

            return crudEvent;

        }
        public CrudEvent AddEvent(CrudEvent crudEvent)
        {
            // Create Event 
            var e = new Event(crudEvent.Id,                      
                            crudEvent.Title,
                            crudEvent.Description,
                            new Location (crudEvent.Address, crudEvent.LocationType),
                            crudEvent.StartsAt,
                            crudEvent.Duration,
                            crudEvent.Type,
                            crudEvent.Audience,
                            crudEvent.PublishDate,
                            crudEvent.IsActive);

            var eFromDB = _eventsRepository.AddEvent(e);
            crudEvent.Id = eFromDB.Id;

            // Create Pictures 
            var pictures = crudEvent.Pictures
                .Where(p => p != null)
                .Select(p => new Picture(eFromDB.Id.IdValue, null, p))
                //.Select(p => new PictureStorageModel()
                //{
                //    EventId = p.EventId,
                //    Description = p.Description,
                //    Link = p.Link
                //})
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
                new Location(crudEvent.Address, crudEvent.LocationType),
                crudEvent.StartsAt,
                crudEvent.Duration,
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

