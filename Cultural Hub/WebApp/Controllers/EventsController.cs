using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;
using Services.Client;

namespace WebApp.Controllers
{

    public class EventsController : Controller
    {
        private ClientEventsService _eventsService;

        public EventsController(
            ClientEventsService eventsService
            )
        {
            _eventsService = eventsService;
        }

        [HttpGet("{id}")]
        public IActionResult Details(string id)
        {

            return View(_eventsService.GetEventDetailsById(id));
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
        public IActionResult AddEvent(CrudEvent _crudEvent)
        {
            _crudEvent.Id.IdValue = Guid.NewGuid().ToString();
            //crudEventViewModel.Pictures.Select(p => new Picture(p.Id = Guid.NewGuid().ToString());

            //var pictures = new List<Picture>() {}

            var crudEvent = new CrudEvent()           
            {
                Id = _crudEvent.Id,
                Title = _crudEvent.Title,
                Description = _crudEvent.Description,
                Address = _crudEvent.Address,
                LocationType = _crudEvent.LocationType,
                Audience = _crudEvent.Audience,
                Duration = _crudEvent.Duration,
                Type = _crudEvent.Type,
                PublishDate = _crudEvent.PublishDate,
                IsActive = _crudEvent.IsActive,
                StartsAt = _crudEvent.StartsAt,
                Pictures = _crudEvent.Pictures,
                IsPublished = _crudEvent.IsPublished
            };

            _eventsService.AddEvent(crudEvent);

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

            var e = _eventsService.GetCrudEventViewModelById(eventId);

            return View(e);
        }

        public IActionResult EditEvent(CrudEvent crudEventViewModel)
        {
            var crudEvent = new CrudEvent()
            {
                Id = crudEventViewModel.Id,
                Title = crudEventViewModel.Title,
                Description = crudEventViewModel.Description,
                Address = crudEventViewModel.Address,
                LocationType = crudEventViewModel.LocationType,
                Audience = crudEventViewModel.Audience,
                Duration = crudEventViewModel.Duration,
                Type = crudEventViewModel.Type,
                PublishDate = crudEventViewModel.PublishDate,
                IsActive = crudEventViewModel.IsActive,
                StartsAt = crudEventViewModel.StartsAt,
                Pictures = crudEventViewModel.Pictures
            };

            _eventsService.EditEvent(crudEvent);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult DeleteEvent(string eventId)
        {
            var e = _eventsService.GetEventDetailsById(eventId);

            return View(e);
        }

        public IActionResult Delete(string eventId)
        {
            _eventsService.DeleteEvent(eventId);

            return RedirectToAction("Index", "Home");
        }
    }
}