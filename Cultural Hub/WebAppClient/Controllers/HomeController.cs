using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.User;
using WebAppClient.Models;

namespace WebAppClient.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> IndexAsync()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44323/");

            var clientId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var result = await httpClient.GetAsync($"/events/clienteventsshortinfo/{clientId}");
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