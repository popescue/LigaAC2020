using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppClient.Models;

namespace WebAppClient.Controllers
{
    [Authorize("AllowAll")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            var u = User;

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