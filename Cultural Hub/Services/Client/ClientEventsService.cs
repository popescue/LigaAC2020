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
                    Id = e.Id.Value,
                    Title = e.Title.TitleValue,
                    StartsAt = e.StartsAt.Value,
                    LocationAddress = e.Location.Address,
                    Pictures = _picturesRepository.GetPicturesForEvent(e.Id.Value).Select(p => p.Link).ToList()
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
                Id = e.Id.Value,
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
                PublishDate = e.PublishDate.PublishDateValue
            };

            return crudEvent;
        }

        public ClientEventDetails GetClientEventDetailsById(string eventId)
        {
            var e = _eventsRepository.GetEventById(eventId);

            var eventDetails = new ClientEventDetails()
            {
                Id = e.Id.Value,
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
                            new EventTitle(crudEvent.Title),
                            new EventDescription(crudEvent.Description),
                            new Location(crudEvent.Address, crudEvent.LocationType),
                            new EventDate(crudEvent.StartsAt.Year, crudEvent.StartsAt.Month, crudEvent.StartsAt.Day, crudEvent.StartsAt.Hour, crudEvent.StartsAt.Minute),
                            new EventDate(crudEvent.EndsAt.Year, crudEvent.EndsAt.Month, crudEvent.EndsAt.Day, crudEvent.EndsAt.Hour, crudEvent.EndsAt.Minute),
                            crudEvent.Type,
                            crudEvent.Audience,
                            new EventPublishDate(crudEvent.PublishDate),
                            crudEvent.IsActive);


            var eFromDB = _eventsRepository.AddEvent(e);
            crudEvent.Id = eFromDB.Id.Value.ToString();

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
                            new EventTitle(crudEvent.Title),
                            new EventDescription(crudEvent.Description),
                            new Location(crudEvent.Address, crudEvent.LocationType),
                            new EventDate(crudEvent.StartsAt.Year, crudEvent.StartsAt.Month, crudEvent.StartsAt.Day, crudEvent.StartsAt.Hour, crudEvent.StartsAt.Minute),
                            new EventDate(crudEvent.EndsAt.Year, crudEvent.EndsAt.Month, crudEvent.EndsAt.Day, crudEvent.EndsAt.Hour, crudEvent.EndsAt.Minute),
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

