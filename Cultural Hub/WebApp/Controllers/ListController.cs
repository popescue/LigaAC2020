using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ListController : Controller
    {
        public IActionResult Items()
        {
           
            var lstEvents = new List<EventList>();


            lstEvents.Add(new EventList
            {
                eventType = "Piesa de teatru",
                location = "Timisoara",
                availableSeats = 100,
                
            });

            lstEvents.Add(new EventList
            {
                eventType = "Opereta",
                location = "Arad",
                availableSeats = 140,

            });

            lstEvents.Add(new EventList
            {
                eventType = "Concert simfonic",
                location = "Timisoara",
                availableSeats = 300,

            });


            return View(lstEvents);
        }
    }
}