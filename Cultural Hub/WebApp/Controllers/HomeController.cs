using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services;
using WebApp.Models;


namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ILogger<HomeController> logger
            )
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44323/");

            var result = await httpClient.GetAsync("/events/usereventsshortinfo");
            var content = await result.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<List<EventShortInfoViewModel>>(content);

            return View(deserialized);
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
