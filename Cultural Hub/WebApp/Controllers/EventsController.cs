using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
   
    public class EventsController : Controller
    {
        [HttpGet("{id}")]
        public IActionResult Details(string id)
        {
            var ev = new EventViewModel
            {
                Id = "123abc",
                Audience = "AG",
                Duration = TimeSpan.FromHours(2),
                LocationAddress = "strada principala",
                LocationType = "interior",
                StartsAt = new DateTime(2021, 1, 20, 12, 0, 0),
                Title = "catei",
                Type = "usturoi"
            };
            return View(ev);
        }
    }
}