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

        public CrudEventViewModel GetCrudEventViewModelById(string eventId)
        {
            var e = _eventsRepository.GetEventById(eventId);

            var crudEventViewModel = new CrudEventViewModel()
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
            // Create Event 
            var e = new Event(crudEventViewModel.Id,
                            crudEventViewModel.Title,
                            crudEventViewModel.Description,
                            new Location(crudEventViewModel.Address, crudEventViewModel.LocationType),
                            crudEventViewModel.StartsAt,
                            new TimeSpan(crudEventViewModel.Duration, 0, 0),
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

            // Create Pictures 
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


        public void EditEvent(CrudEventViewModel crudEventViewModel)
        {
            // Update Event 
            var e = new Event(crudEventViewModel.Id,
                crudEventViewModel.Title,
                crudEventViewModel.Description,
                new Location(crudEventViewModel.Address, crudEventViewModel.LocationType),
                crudEventViewModel.StartsAt,
                new TimeSpan(crudEventViewModel.Duration, 0, 0),
                crudEventViewModel.Type,
                crudEventViewModel.Audience,
                crudEventViewModel.PublishDate,
                crudEventViewModel.IsActive);

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

            _eventsRepository.EditEvent(eventStorageModel);

            // Update Pictures 
            _picturesRepository.DeleteAllPicturesFromEvent(eventStorageModel.Id);

            var pictureStorageModels = crudEventViewModel.Pictures
                .Where(p => p != null)
                .Select(p => new Picture(eventStorageModel.Id, null, p))
                .Select(p => new PictureStorageModel()
                {
                    EventId = p.EventId,
                    Description = p.Description,
                    Link = p.Link
                })
                .ToList();

            _picturesRepository.AddPicturesToEvent(pictureStorageModels);
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

