﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Factories;
using WebApp.Models;

namespace WebApp.Services
{
    public class EventsService
    {
        public List<EventDetailsViewModel> GetEventDetails()
        {
            var eventDetailsViewModels = EventsStore.GetEvents().Select(e =>
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
            var eventListViewModels = EventsStore.GetEvents().Select(e =>
            {
                var eventListViewModel = new EventListViewModel()
                {
                    Id = e.Id,
                    Title = e.Title,
                    StartsAt = e.StartsAt,
                    LocationAddress = e.Location.Address
                };

                return eventListViewModel;
            }).ToList();

            return eventListViewModels;
        }

        public EventDetailsViewModel GetEventDetailsById(string id)
        {
            return GetEventDetails().Find(e => e.Id == id);
        }
    }
}
