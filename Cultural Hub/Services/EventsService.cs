using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Repositories;


namespace Services
{
    public class EventsService
    {
        private IEventsRepository _eventsRepository;
        private IPicturesRepository _picturesRepository;

        public EventsService(
            IEventsRepository eventsRepository,
            IPicturesRepository picturesRepository
            )
        {
            _eventsRepository = eventsRepository;
            _picturesRepository = picturesRepository;
        }

        public List<EventDetails> GetEventDetailsList()
        {
            var eventDetailsViewModels = _eventsRepository.GetEvents().Select(e =>
            {
                var eventDetailsViewModel = new EventDetails()
                {
                    Id = e.Id,
                    Title = e.Title,
                    StartsAt = e.StartsAt,
                    LocationAddress = e.Location.Address,
                    LocationType = e.Location.Type.ToString(),
                    Description = e.Description,
                    Duration = e.Duration,
                    Audience = e.Audience.ToString(),
                    Type = e.Type.ToString(),
                    Pictures = _picturesRepository.GetPicturesForEvent(e.Id).Select(p => p.Link).ToList()
                };

                return eventDetailsViewModel;
            }).ToList();

            return eventDetailsViewModels;
        }

        public List<EventShortInfo> GetEventShortInfoList()
        {
            var eventShortInfoViewModels = _eventsRepository.GetEvents().Select(e =>
            {
                var eventShortInfoViewModel = new EventShortInfo()
                {
                    Id = e.Id,
                    Title = e.Title,
                    StartsAt = e.StartsAt,
                    LocationAddress = e.Location.Address,
                    Pictures = _picturesRepository.GetPicturesForEvent(e.Id).Select(p => p.Link).ToList()
                };

                return eventShortInfoViewModel;
            }).ToList();

            return eventShortInfoViewModels;
        }

        public CrudEvent GetCrudEventViewModelById(string eventId)
        {
            var e = _eventsRepository.GetEventById(eventId);

            var crudEventViewModel = new CrudEvent()
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Address = e.Location.Address,
                LocationType = e.Location.Type,
                Audience = e.Audience,
                Duration = (int)e.Duration.TotalHours,
                Type = e.Type,
                PublishDate = e.PublishDate,
                IsActive = e.IsActive,
                StartsAt = e.StartsAt,
                Pictures = _picturesRepository.GetPicturesForEvent(e.Id).Select(p => p.Link).ToList()
            };

            return crudEventViewModel;
        }

        public EventDetails GetEventDetailsById(string eventId)
        {
            var e = _eventsRepository.GetEventById(eventId);

            var eventDetailsViewModel = new EventDetails()
            {
                Id = e.Id,
                Title = e.Title,
                StartsAt = e.StartsAt,
                LocationAddress = e.Location.Address,
                LocationType = e.Location.Type.ToString(),
                Description = e.Description,
                Duration = e.Duration,
                Audience = e.Audience.ToString(),
                Type = e.Type.ToString(),
                Pictures = _picturesRepository.GetPicturesForEvent(e.Id).Select(p => p.Link).ToList()
            };

            return eventDetailsViewModel;
        }

        public CrudEvent AddEvent(CrudEvent crudEvent)
        {
            // Create Event 
            var e = new Event(crudEvent.Id,
                            crudEvent.Title,
                            crudEvent.Description,
                            new Location(crudEvent.Address, crudEvent.LocationType),
                            crudEvent.StartsAt,
                            new TimeSpan(crudEvent.Duration, 0, 0),
                            crudEvent.Type,
                            crudEvent.Audience,
                            crudEvent.PublishDate,
                            crudEvent.IsActive);


            var eFromDB = _eventsRepository.AddEvent(e);
            crudEvent.Id = eFromDB.Id;

            // Create Pictures 
            var pictures = crudEvent.Pictures
                .Where(p => p != null)
                .Select(p => new Picture(eFromDB.Id, null, p))
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
                new TimeSpan(crudEvent.Duration, 0, 0),
                crudEvent.Type,
                crudEvent.Audience,
                crudEvent.PublishDate,
                crudEvent.IsActive);

            _eventsRepository.EditEvent(e);

            // Update Pictures 
            _picturesRepository.DeleteAllPicturesFromEvent(e.Id);

            var pictures = crudEvent.Pictures
                .Where(p => p != null)
                .Select(p => new Picture(e.Id, null, p))
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

