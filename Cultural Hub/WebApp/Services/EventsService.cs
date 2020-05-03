﻿using Domain;
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
    }
}

