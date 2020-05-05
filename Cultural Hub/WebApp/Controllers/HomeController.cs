using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Services.User;
using WebApp.Models;


namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserEventsService _userEventsService;

        public HomeController(
            ILogger<HomeController> logger,
            UserEventsService userEventsService
            )
        {
            _logger = logger;
            _userEventsService = userEventsService;
        }

        public IActionResult Index()
        {
            return View(_userEventsService
                .GetUserEventShortInfoList()
                .Select(e => new EventShortInfoViewModel()
                {
                    Id = e.Id,
                    LocationAddress = e.LocationAddress,
                    Pictures = e.Pictures,
                    StartsAt = e.StartsAt,
                    Title = e.Title
                })
                .ToList()
                );
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
