using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;
using Services;
using Services.Global;
using Services.Client;

namespace WebApp.Controllers
{

    public class EventsController : Controller
    {
        private GlobalEventsService _globalEventsService;
        private ClientEventsService _clientEventsService;

        public EventsController(GlobalEventsService globalEventsService, ClientEventsService clientEventsService)
        {
            _globalEventsService = globalEventsService;
            _clientEventsService = clientEventsService;
        }

        [HttpGet("{id}")]
        public IActionResult Details(string id)
        {
            var eventDetails = _globalEventsService.GetGlobalEventDetailsById(id);
      
            return View(new EventDetailsViewModel()
            {
                Id = eventDetails.Id,
                Title = eventDetails.Title,
                Audience = eventDetails.Audience,
                Description = eventDetails.Description,
                Duration = eventDetails.Duration,
                LocationType = eventDetails.LocationType,
                LocationAddress = eventDetails.LocationAddress,
                Type = eventDetails.Type,
                StartsAt = eventDetails.StartsAt,
                Pictures = eventDetails.Pictures
            });
        }

        [HttpGet]
        public IActionResult AddEvent()
        {
            List<Audience> audience = Enum.GetValues(typeof(Audience)).Cast<Audience>().ToList();
            List<EventType> eventType = Enum.GetValues(typeof(EventType)).Cast<EventType>().ToList();
            List<LocationType> locationType = Enum.GetValues(typeof(LocationType)).Cast<LocationType>().ToList();

            ViewBag.RequiredAudience = new SelectList(audience);
            ViewBag.RequiredEventType = new SelectList(eventType);
            ViewBag.RequiredLocationType = new SelectList(locationType);

            return View();
        }

        [HttpPost]
        public IActionResult AddEvent(CrudEventViewModel crudEventViewModel)
        {
            crudEventViewModel.Id = Guid.NewGuid().ToString();

            var crudEvent = new CrudEvent()           
            {
                Id = new EventId(crudEventViewModel.Id),
                Title = new EventTitle(crudEventViewModel.Title),
                Description = new EventDescription(crudEventViewModel.Description),
                Location=new Location(crudEventViewModel.Address,crudEventViewModel.LocationType),
                Audience = crudEventViewModel.Audience,
                Duration = new EventDuration(new TimeSpan(crudEventViewModel.Duration,0,0)),
                Type = crudEventViewModel.Type,
                PublishDate = new EventPublishDate(crudEventViewModel.PublishDate),
                IsActive = crudEventViewModel.IsActive,
                StartsAt = new EventStartDate(crudEventViewModel.StartsAt.Year, crudEventViewModel.StartsAt.Month, crudEventViewModel.StartsAt.Day, crudEventViewModel.StartsAt.Hour, crudEventViewModel.StartsAt.Minute),
                Pictures = crudEventViewModel.Pictures
            };

            _clientEventsService.AddEvent(crudEvent);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult EditEvent(string eventId)
        {
            List<Audience> audience = Enum.GetValues(typeof(Audience)).Cast<Audience>().ToList();
            List<EventType> eventType = Enum.GetValues(typeof(EventType)).Cast<EventType>().ToList();
            List<LocationType> locationType = Enum.GetValues(typeof(LocationType)).Cast<LocationType>().ToList();

            ViewBag.RequiredAudience = new SelectList(audience);
            ViewBag.RequiredEventType = new SelectList(eventType);
            ViewBag.RequiredLocationType = new SelectList(locationType);

            var crudEvent = _clientEventsService.GetCrudEventViewModelById(eventId);

            return View(new CrudEventViewModel()
            {
                Id = crudEvent.Id.IdValue,
                Title = crudEvent.Title.TitleValue,
                Audience = crudEvent.Audience,
                Description = crudEvent.Description.DescriptionValue,
                Duration = (int)crudEvent.Duration.DurationValue.TotalMinutes,
                LocationType = crudEvent.Location.Type,
                Address = crudEvent.Location.Address,
                Type = crudEvent.Type,
                StartsAt = crudEvent.StartsAt.StartDateValue,
                Pictures = crudEvent.Pictures,
                IsActive = crudEvent.IsActive,
                PublishDate = crudEvent.PublishDate.PublishDateValue
            });
        }

        public IActionResult EditEvent(CrudEventViewModel crudEventViewModel)
        {
            var crudEvent = new CrudEvent()
            {
                Id = new EventId(crudEventViewModel.Id),
                Title = new EventTitle(crudEventViewModel.Title),
                Description = new EventDescription(crudEventViewModel.Description),
                Location = new Location(crudEventViewModel.Address, crudEventViewModel.LocationType),
                Audience = crudEventViewModel.Audience,
                Duration = new EventDuration(new TimeSpan(crudEventViewModel.Duration, 0, 0)),
                Type = crudEventViewModel.Type,
                PublishDate = new EventPublishDate(crudEventViewModel.PublishDate),
                IsActive = crudEventViewModel.IsActive,
                StartsAt = new EventStartDate(crudEventViewModel.StartsAt.Year, crudEventViewModel.StartsAt.Month, crudEventViewModel.StartsAt.Day, crudEventViewModel.StartsAt.Hour, crudEventViewModel.StartsAt.Minute),
                Pictures = crudEventViewModel.Pictures
            };

            _clientEventsService.EditEvent(crudEvent);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult DeleteEvent(string eventId)
        {
            var crudEvent = _clientEventsService.GetCrudEventViewModelById(eventId);

            return View(new CrudEventViewModel()
            {
                Id = crudEvent.Id.IdValue,
                Title = crudEvent.Title.TitleValue,
                Audience = crudEvent.Audience,
                Description = crudEvent.Description.DescriptionValue,
                Duration = (int)crudEvent.Duration.DurationValue.TotalMinutes,
                LocationType = crudEvent.Location.Type,
                Address = crudEvent.Location.Address,
                Type = crudEvent.Type,
                StartsAt = crudEvent.StartsAt.StartDateValue,
                Pictures = crudEvent.Pictures,
                IsActive = crudEvent.IsActive,
                PublishDate = crudEvent.PublishDate.PublishDateValue
            });
        }

        public IActionResult Delete(string eventId)
        {
            _clientEventsService.DeleteEvent(eventId);

            return RedirectToAction("Index", "Home");
        }
    }
}