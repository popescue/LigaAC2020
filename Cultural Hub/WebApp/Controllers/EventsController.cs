using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        [HttpPost]
        public IActionResult AddEvent(EventViewModel eventViewModel)
        {
            eventViewModel = _eventsService.AddEvent(eventViewModel);

            return View(eventViewModel);
        }

        [HttpDelete]
        public IActionResult DeleteEvent(string eventId)
        {
            var e = _eventsService.GetEvent(eventId);

            _eventsService.DeleteEvent(e);

            return View();
        }
    }
}