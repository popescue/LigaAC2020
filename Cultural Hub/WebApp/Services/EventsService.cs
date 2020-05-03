using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Repositories;
using WebApp.Models;

namespace WebApp.Services
{
    public class EventsService
    {
        public List<EventDetailsViewModel> GetEventDetails()
        {
            var eventDetailsViewModels = EventRepository.GetEvents().Select(e =>
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
            var eventListViewModels = EventRepository.GetEvents().Select(e =>
            {
                List<Picture> pictures = PictureRepository.GetPicturesForEvent(e.Id);
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

        public EventDetailsViewModel GetEventDetailsById(string id)
        {
            return GetEventDetails().Find(e => e.Id == id);
        }

        public void CreateEvent(EventCreateModel _event)
        {

            EventRepository.CreateEvent(_event);
            PictureRepository.CreateEvent(_event);
        }

        public void DeleteEvent(string id)
        {

            var _event = EventRepository.GetEventById(id);

            EventRepository.DeleteEvent(_event);

            var _picture = PictureRepository.GetPictureById(id);

            PictureRepository.DeletePicture(_picture);
        }

        public static Event GetEventById(string id)
        {
            return EventRepository.GetEventById(id);
        }

        public static Picture GetPictureById(string id)
        {
            return PictureRepository.GetPictureById(id);
        }

        public static EventEditViewModel GetEventAndPictures(string id)
        {
            Event _event = GetEventById(id);
            List<Picture> _pictures = PictureRepository.GetPicturesForEvent(id);

            EventEditViewModel eventEditViewModel = new EventEditViewModel()
            {
                Id = _event.Id,
                Title = _event.Title,
                Description = _event.Description,
                Address = _event.Location.Address,
                LocationType = _event.Location.Type,
                StartsAt = _event.StartsAt,
                Duration = _event.Duration.Hours,
                Type = _event.Type,
                Audience = _event.Audience,
                PublishDate = _event.PublishDate,
                IsActive = _event.IsActive,
                Pictures = _pictures.Select(p => p.Link).ToList()
            };

            return eventEditViewModel;
        }
    }
}

