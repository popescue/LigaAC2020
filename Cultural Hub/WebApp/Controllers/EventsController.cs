using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{

    public class EventsController : Controller
    {
        private IEventsService _eventsService;

        public EventsController(
            IEventsService eventsService
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
        public IActionResult AddEvent(CrudEventViewModel crudEventViewModel)
        {
            crudEventViewModel = _eventsService.AddEvent(crudEventViewModel);

            return RedirectToAction("Index", "Home");
        }

        //[HttpDelete]
        //public IActionResult DeleteEvent(string eventId)
        //{
        //    //var e = _eventsService.GetEvent(eventId);

        //    //_eventsService.DeleteEvent(e);

        //    //return View();
        //}
    }
}