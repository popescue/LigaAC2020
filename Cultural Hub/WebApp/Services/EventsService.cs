using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Repositories;
using WebApp.Models;
using WebApp.StorageModels;

namespace WebApp.Services
{
    public class EventsService : IEventsService
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

        public List<EventDetailsViewModel> GetEventDetailsList()
        {
            var eventDetailsViewModels = _eventsRepository.GetEvents().Select(e =>
            {
                var eventDetailsViewModel = new EventDetailsViewModel()
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

        public List<EventShortInfoViewModel> GetEventShortInfoList()
        {
            var eventShortInfoViewModels = _eventsRepository.GetEvents().Select(e =>
            {
                var eventShortInfoViewModel = new EventShortInfoViewModel()
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

        public EventDetailsViewModel GetEventDetailsById(string eventId)
        {
            var e = _eventsRepository.GetEventById(eventId);

            var eventDetailsViewModel = new EventDetailsViewModel()
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

        public CrudEventViewModel AddEvent(CrudEventViewModel crudEventViewModel)
        {
            var e = new Event(crudEventViewModel.Id,
                            crudEventViewModel.Title,
                            crudEventViewModel.Description,
                            new Location(crudEventViewModel.Address, crudEventViewModel.LocationType),
                            crudEventViewModel.StartsAt,
                            new TimeSpan(crudEventViewModel.Duration),
                            crudEventViewModel.Type,
                            crudEventViewModel.Audience,
                            crudEventViewModel.PublishDate,
                            crudEventViewModel.IsActive);

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

            var eFromDB = _eventsRepository.AddEvent(eventStorageModel);
            crudEventViewModel.Id = eFromDB.Id;

            var pictureStorageModels = crudEventViewModel.Pictures
                .Where(p => p != null)
                .Select(p => new Picture(eFromDB.Id, null, p))
                .Select(p => new PictureStorageModel()
                {
                    EventId = p.EventId,
                    Description = p.Description,
                    Link = p.Link
                })
                .ToList();

            _picturesRepository.AddPicturesToEvent(pictureStorageModels);

            return crudEventViewModel;
        }


        public CrudEventViewModel EditEvent(CrudEventViewModel CrudEventViewModel)
        {
            //Enum.TryParse(CrudEventViewModel.Type, out EventType eventType);
            //Enum.TryParse(CrudEventViewModel.Audience, out Audience audience);
            //Enum.TryParse(CrudEventViewModel.LocationType, out LocationType locationType);

            //var e = _eventsRepository.GetEventById(CrudEventViewModel.Id);

            //e.Title = CrudEventViewModel.Title;
            //e.StartsAt = CrudEventViewModel.StartsAt;
            //e.Description = CrudEventViewModel.Description;
            //e.Duration = CrudEventViewModel.Duration;
            //e.Audience = audience;
            //e.Type = eventType;
            //e.PublishDate = CrudEventViewModel.PublishDate;
            //e.IsActive = CrudEventViewModel.IsActive;

            //_eventsRepository.EditEvent(e);

            //var location = _locationsRepository.GetLocationByEventId(CrudEventViewModel.Id);

            //location.Address = CrudEventViewModel.LocationAddress;
            //location.Type = locationType;

            //_locationsRepository.EditLocation(location);

            //var pictures = _picturesRepository.GetPicturesForEvent(CrudEventViewModel.Id);

            //pictures.ForEach(p => _picturesRepository.DeletePicture(p));

            //CrudEventViewModel.Pictures.ForEach(p =>
            //{
            //    var picture = new Picture()
            //    {
            //        Link = p,
            //        EventId = e.Id
            //    };

            //    _picturesRepository.AddPicture(picture);
            //});

            return CrudEventViewModel;
        }

        public void DeleteEvent(string eventId)
        { 
            _eventsRepository.DeleteEvent(eventId);

            _picturesRepository.DeleteAllPicturesFromEvent(eventId);
        }
    }
}

