using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Repositories;

namespace Services.User
{
    public class UserEventsService
    {
        private readonly IUserEventsReader _eventsReader;
        private readonly IEventsRepository _eventsRepository;
        private readonly IPicturesRepository _picturesRepository;

        public UserEventsService(IEventsRepository eventsRepository, IUserEventsReader eventsReader,
            IPicturesRepository picturesRepository)
        {
            _eventsRepository = eventsRepository;
            _picturesRepository = picturesRepository;

            _eventsReader = eventsReader;
        }

        public UserEventDetails GetUserEventDetailsById(string eventId)
        {
            var e = _eventsRepository.GetEventById(eventId);

            var eventDetails = new UserEventDetails
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

        public IEnumerable<UserEventShortInfo> GetEventShortInfoList()
        {
            return _eventsReader.GetEvents()
                .Select(e => new UserEventShortInfo
                {
                    ClientId = e.Client,
                    Title = e.Title,
                    StartsAt = e.StartsAt,
                    Pictures = e.Pictures.Select(x => new Uri(x.Link)),
                    Id = e.Id,
                    LocationAddress = e.LocationAddress
                }).ToList();
        }
    }

    public class SimplePicture
    {
        public string Description { get; set; }
        public string Link { get; set; }
    }

    public class EventWithPictures
    {
        public string Id { get; set; }
        public Guid Client { get; set; }
        public IEnumerable<SimplePicture> Pictures { get; set; }
        public string Title { get; set; }
        public DateTime StartsAt { get; set; }
        public string LocationAddress { get; set; }
    }

    public interface IUserEventsReader
    {
        IEnumerable<EventWithPictures> GetEvents();
    }
}