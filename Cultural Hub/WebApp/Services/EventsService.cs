using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Repositories;
using WebApp.Models;

namespace WebApp.Services
{
    public class EventsService : IEventsService
    {
        private IEventsRepository _eventsRepository;
        private IPicturesRepository _picturesRepository;
        private ILocationsRepository _locationsRepository;

        public EventsService(
            IEventsRepository eventsRepository,
            IPicturesRepository picturesRepository,
            ILocationsRepository locationsRepository
            )
        {
            _eventsRepository = eventsRepository;
            _locationsRepository = locationsRepository;
            _picturesRepository = picturesRepository;
        }
        public List<EventDetailsViewModel> GetEventDetails()
        {
            var eventDetailsViewModels = _eventsRepository.GetEvents().Select(e =>
            {
                var eventDetailsViewModel = new EventDetailsViewModel()
                {
                    Id = e.Id,
                    Title = e.Title,
                    StartsAt = e.StartsAt,
                    LocationAddress = e.Location.Address,
                    Description = e.Description,
                    Duration = e.Duration,
                    Audience = e.Audience.ToString(),
                    LocationType = e.Location.Type.ToString(),
                    Type = e.Type.ToString()
                };

                return eventDetailsViewModel;
            }).ToList();

            return eventDetailsViewModels;
        }

        public List<EventListViewModel> GetEventList()
        {
            var eventListViewModels = _eventsRepository.GetEvents().Select(e =>
            {
                List<Picture> pictures = _picturesRepository.GetPicturesForEvent(e.Id);
                var eventListViewModel = new EventListViewModel()
                {
                    Id = e.Id,
                    Title = e.Title,
                    StartsAt = e.StartsAt,
                    LocationAddress = e.Location.Address,
                    Pictures = pictures.Select(p => p.Link).ToList()
                };

                return eventListViewModel;
            }).ToList();

            return eventListViewModels;
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
                Description = e.Description,
                Duration = e.Duration,
                Audience = e.Audience.ToString(),
                LocationType = e.Location.Type.ToString(),
                Type = e.Type.ToString()
            };

            return eventDetailsViewModel;
        }

        public EventViewModel GetEvent(string eventId)
        {
            var e = _eventsRepository.GetEventById(eventId);
            if (e == null) return null;

            var eventViewModel = new EventViewModel()
            {
                Id = e.Id,
                Title = e.Title,
                StartsAt = e.StartsAt,
                LocationAddress = e.Location.Address,
                Description = e.Description,
                Duration = e.Duration,
                Audience = e.Audience.ToString(),
                LocationType = e.Location.Type.ToString(),
                Type = e.Type.ToString(),
                Pictures = _picturesRepository.GetPicturesForEvent(eventId).Select(p => p.Link).ToList()
            };

            return eventViewModel;
        }

        public EventViewModel AddEvent(EventViewModel eventViewModel)
        {
            //Enum.TryParse(eventViewModel.Type, out EventType eventType);
            //Enum.TryParse(eventViewModel.Audience, out Audience audience);

            //var e = new Event()
            //{
            //    Title = eventViewModel.Title,
            //    StartsAt = eventViewModel.StartsAt,
            //    Description = eventViewModel.Description,
            //    Duration = eventViewModel.Duration,
            //    Audience = audience,
            //    Type = eventType,
            //    PublishDate = eventViewModel.PublishDate,
            //    IsActive = eventViewModel.IsActive
            //};

            //e = _eventsRepository.AddEvent(e);
            //eventViewModel.Id = e.Id;

            //Enum.TryParse(eventViewModel.LocationType, out LocationType locationType);
            //var location = new Location(eventViewModel.LocationAddress, locationType);
            //_locationsRepository.AddLocation(location);

            //eventViewModel.Pictures.ForEach(p =>
            //{
            //    var picture = new Picture()
            //    {
            //        Link = p,
            //        EventId = e.Id
            //    };

            //    _picturesRepository.AddPicture(picture);
            //});

            return eventViewModel;
        }


        public EventViewModel EditEvent(EventViewModel eventViewModel)
        {
            //Enum.TryParse(eventViewModel.Type, out EventType eventType);
            //Enum.TryParse(eventViewModel.Audience, out Audience audience);
            //Enum.TryParse(eventViewModel.LocationType, out LocationType locationType);

            //var e = _eventsRepository.GetEventById(eventViewModel.Id);

            //e.Title = eventViewModel.Title;
            //e.StartsAt = eventViewModel.StartsAt;
            //e.Description = eventViewModel.Description;
            //e.Duration = eventViewModel.Duration;
            //e.Audience = audience;
            //e.Type = eventType;
            //e.PublishDate = eventViewModel.PublishDate;
            //e.IsActive = eventViewModel.IsActive;

            //_eventsRepository.EditEvent(e);

            //var location = _locationsRepository.GetLocationByEventId(eventViewModel.Id);

            //location.Address = eventViewModel.LocationAddress;
            //location.Type = locationType;

            //_locationsRepository.EditLocation(location);

            //var pictures = _picturesRepository.GetPicturesForEvent(eventViewModel.Id);

            //pictures.ForEach(p => _picturesRepository.DeletePicture(p));

            //eventViewModel.Pictures.ForEach(p =>
            //{
            //    var picture = new Picture()
            //    {
            //        Link = p,
            //        EventId = e.Id
            //    };

            //    _picturesRepository.AddPicture(picture);
            //});

            return eventViewModel;
        }

        public void DeleteEvent(EventViewModel eventViewModel)
        {
            var e = _eventsRepository.GetEventById(eventViewModel.Id);
            _eventsRepository.DeleteEvent(e);

            var location = _locationsRepository.GetLocationByEventId(eventViewModel.Id);
            _locationsRepository.DeleteLocation(location);

            var pictures = _picturesRepository.GetPicturesForEvent(eventViewModel.Id);
            pictures.ForEach(p => _picturesRepository.DeletePicture(p));
        }
    }
}

