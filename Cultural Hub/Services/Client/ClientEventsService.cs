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

        public ClientEventsService(
            IEventsRepository eventsRepository,
            IPicturesRepository picturesRepository
            )
        {
            _eventsRepository = eventsRepository;
            _picturesRepository = picturesRepository;
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
                    LocationAddress = e.Location.Address,
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
                Id = e.Id.IdValue,
                Title = e.Title.TitleValue,
                StartsAt = e.StartsAt.StartDateValue,
                Address = e.Location.Address,
                LocationType = e.Location.Type,
                Description = e.Description.DescriptionValue,
                Duration = (int)e.Duration.DurationValue.TotalHours,
                Audience = e.Audience,
                Type = e.Type,
                Pictures = _picturesRepository.GetPicturesForEvent(e.Id.IdValue).Select(p => p.Link).ToList(),
                IsActive = e.IsActive,
                PublishDate = e.PublishDate.PublishDateValue
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
                Pictures = _picturesRepository.GetPicturesForEvent(e.Id.IdValue).Select(p => p.Link).ToList()
            };
            return eventDetails;
        }

        public CrudEvent AddEvent(CrudEvent crudEvent)
        {
            // Create Event 
            var e = new Event(new EventId(Guid.NewGuid().ToString().Substring(31)),
                            new EventTitle(crudEvent.Title),
                            new EventDescription(crudEvent.Description),
                            new Location(crudEvent.Address, crudEvent.LocationType),
                            new EventStartDate(crudEvent.StartsAt.Year, crudEvent.StartsAt.Month, crudEvent.StartsAt.Day, crudEvent.StartsAt.Hour, crudEvent.StartsAt.Minute),
                            new EventDuration(new TimeSpan(crudEvent.Duration, 0, 0)),
                            crudEvent.Type,
                            crudEvent.Audience,
                            new EventPublishDate(crudEvent.PublishDate),
                            crudEvent.IsActive);


            var eFromDB = _eventsRepository.AddEvent(e);
            crudEvent.Id = eFromDB.Id.ToString();

            // Create Pictures 
            var pictures = crudEvent.Pictures
                .Where(p => p != null)
                .Select(p => new Picture(eFromDB.Id.ToString(), null, p))
                .ToList();

            _picturesRepository.AddPicturesToEvent(pictures);

            return crudEvent;
        }


        public void EditEvent(CrudEvent crudEvent)
        {
            // Update Event 
            var e = new Event(new EventId(crudEvent.Id),
                            new EventTitle(crudEvent.Title),
                            new EventDescription(crudEvent.Description),
                            new Location(crudEvent.Address, crudEvent.LocationType),
                            new EventStartDate(crudEvent.StartsAt.Year, crudEvent.StartsAt.Month, crudEvent.StartsAt.Day, crudEvent.StartsAt.Hour, crudEvent.StartsAt.Minute),
                            new EventDuration(new TimeSpan(crudEvent.Duration, 0, 0)),
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
}

