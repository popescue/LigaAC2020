using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApp.Repositories;

namespace Services.User
{
    public class UserEventsService
    {
        private IEventsRepository _eventsRepository;
        private IPicturesRepository _picturesRepository;

        public UserEventsService(IEventsRepository eventsRepository, IPicturesRepository picturesRepository)
        {
            _eventsRepository = eventsRepository;
            _picturesRepository = picturesRepository;
        }

        public List<UserEventShortInfo> GetUserEventShortInfoList()
        {
            var eventsShortInfo = _eventsRepository.GetEvents().Select(e =>
            {
                var eventShortInfo = new UserEventShortInfo()
                {
                    Id = e.Id.IdValue,
                    Title = e.Title.TitleValue,
                    StartsAt = e.StartsAt.StartDateValue,
                    LocationAddress = e.Location.Address,
                    Pictures = _picturesRepository.GetPicturesForEvent(e.Id.IdValue).Select(p => p.Link).ToList()
                };

                return eventShortInfo;
            }).ToList();

            return eventsShortInfo;
        }

        public UserEventDetails GetUserEventDetailsById(string eventId)
        {
            var e = _eventsRepository.GetEventById(eventId);

            var eventDetails = new UserEventDetails()
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
    }
}
