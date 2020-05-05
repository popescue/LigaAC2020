using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApp.Repositories;

namespace Services.Global
{
    public class GlobalEventsService
    {
        private IEventsRepository _eventsRepository;
        private IPicturesRepository _picturesRepository;

        public GlobalEventsService(IEventsRepository eventsRepository, IPicturesRepository picturesRepository)
        {
            _eventsRepository = eventsRepository;
            _picturesRepository = picturesRepository;
        }

        public List<GlobalEventDetails> GetGlobalEventDetailsList()
        {
            var eventDetailsViewModels = _eventsRepository.GetEvents().Select(e =>
            {
                var eventDetailsViewModel = new GlobalEventDetails()
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

                return eventDetailsViewModel;

            }).ToList();

            return eventDetailsViewModels;
        }
        public List<GlobalEventShortInfo> GetGlobalEventShortInfoList()
        {
            var eventShortInfoViewModels = _eventsRepository.GetEvents().Select(e =>
            {
                var eventShortInfoViewModel = new GlobalEventShortInfo()
                {
                    Id = e.Id.IdValue,
                    Title = e.Title.TitleValue,
                    StartsAt = e.StartsAt.StartDateValue,
                    LocationAddress = e.Location.Address,
                    Pictures = _picturesRepository.GetPicturesForEvent(e.Id.IdValue).Select(p => p.Link).ToList()
                };

                return eventShortInfoViewModel;
            }).ToList();

            return eventShortInfoViewModels;
        }
        public GlobalEventDetails GetGlobalEventDetailsById(string eventId)
        {
            var e = _eventsRepository.GetEventById(eventId);

            var eventDetailsViewModel = new GlobalEventDetails()
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

            return eventDetailsViewModel;
        }
    }
}
