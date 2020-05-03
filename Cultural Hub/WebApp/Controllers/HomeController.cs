using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Client;
using WebApp.Models;


namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ClientEventsService _eventsService;

        public HomeController(
            ILogger<HomeController> logger,
            ClientEventsService eventsService
            )
        {
            _logger = logger;
            _eventsService = eventsService;
        }

        public IActionResult Index()
        {
            return View(_eventsService.GetClientEventShortInfoList());
        }

        public IActionResult Index2()
        {
            return View(_eventsService.GetClientEventShortInfoList());
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
