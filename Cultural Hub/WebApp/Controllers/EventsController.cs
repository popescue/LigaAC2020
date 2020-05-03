using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;
using WebApp.Services;
using Domain;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
   
    public class EventsController : Controller
    {
        private EventsService _eventsService;

        public EventsController()
        {
            _eventsService = new EventsService();
        }
        
        [HttpGet("{id}")]
        public IActionResult Details(string id)
        {

            return View(_eventsService.GetEventDetailsById(id));
        }

        //GET
        public IActionResult Create()
        {


            List<Audience> audience = Enum.GetValues(typeof(Audience)).Cast<Audience>().ToList();
            List<EventType> eventType = Enum.GetValues(typeof(EventType)).Cast<EventType>().ToList();
            List<LocationType> locationType = Enum.GetValues(typeof(LocationType)).Cast<LocationType>().ToList();

            ViewBag.RequiredAudience = new SelectList(audience);
            ViewBag.RequiredEventType = new SelectList(eventType);
            ViewBag.RequiredLocationType = new SelectList(locationType);

            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EventCreateModel newEvent)
        {
            try
            {
                _eventsService.CreateEvent(newEvent);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        //GET
        public ActionResult Delete(string id)
        {
            var _event = EventsService.GetEventById(id);
            return View(_event);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                _eventsService.DeleteEvent(id);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public IActionResult Edit(string id)
        {


            List<Audience> audience = Enum.GetValues(typeof(Audience)).Cast<Audience>().ToList();
            List<EventType> eventType = Enum.GetValues(typeof(EventType)).Cast<EventType>().ToList();
            List<LocationType> locationType = Enum.GetValues(typeof(LocationType)).Cast<LocationType>().ToList();

            ViewBag.RequiredAudience = new SelectList(audience);
            ViewBag.RequiredEventType = new SelectList(eventType);
            ViewBag.RequiredLocationType = new SelectList(locationType);

            EventEditViewModel eventEditViewModel = EventsService.GetEventAndPictures(id);

            return View(eventEditViewModel);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EventEditViewModel updateEvent)
        {
            try
            {
                Event _event = EventsService.GetEventById(updateEvent.Id);

                /*_event.Title=updateEvent.Id,
                _event.Description=updateEvent.Description,
                _event.Location.Address=updateEvent.Address,
                _event.Location.Type=updateEvent.LocationType,
                _event.StartsAt=updateEvent.StartsAt,
                _event.Duration=updateEvent.Duration,
                _event.Type=updateEvent.Type,
                _event.Audience=updateEvent.Audience,
                _event.PublishDate=updateEvent.PublishDate,
                _event.IsActive=updateEvent.IsActive));*/

                _eventsService.CreateEvent(updateEvent);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

    }
}