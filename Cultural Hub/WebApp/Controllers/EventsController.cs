using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;
using WebApp.Services;

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

        [HttpGet]
        public IActionResult AddEvent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEvent(EventDetailsViewModel eventDetails)
        {
            /*if (!ModelState.IsValid)
            {
                return View(eventDetails);
            }*/
            //EventDetailsViewModel.Save();
            return Redirect("/");
        }
    }
}