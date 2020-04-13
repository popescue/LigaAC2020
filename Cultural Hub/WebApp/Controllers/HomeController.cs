using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<EventViewModel> events = new List<EventViewModel> {

             new EventViewModel
            {
                Id = "123abc",
                Audience = "AG",
                Duration = TimeSpan.FromHours(2),
                LocationAddress = "strada principala",
                LocationType = "interior",
                StartsAt = new DateTime(2021, 1, 20, 12, 0, 0),
                Title = "catei",
                Type = "usturoi"
            },
             new EventViewModel
            {
                Id = "456dfr",
                Audience = "AG",
                Duration = new TimeSpan(1, 30, 0),
                LocationAddress = "strada secundara",
                LocationType = "exterior",
                StartsAt = new DateTime(2021, 3, 10, 2, 0, 0),
                Title = "pisici",
                Type = "ciocolata"
            }
            };

            return View(events);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
