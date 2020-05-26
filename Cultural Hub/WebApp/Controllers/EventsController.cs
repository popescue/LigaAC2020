using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;
using Services;
using Services.User;
using Services.Client;
using Microsoft.AspNetCore.Authorization;
using System.Threading;
using System.Security.Claims;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using Newtonsoft.Json;

namespace WebApp.Controllers
{

    public class EventsController : Controller
    {
        private UserEventsService _userEventsService;
        private ClientEventsService _clientEventsService;
        private IWebHostEnvironment _environment;

        public EventsController(UserEventsService userEventsService, ClientEventsService clientEventsService, IWebHostEnvironment environment)
        {
            _userEventsService = userEventsService;
            _clientEventsService = clientEventsService;
            _environment = environment;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44323/");

            var result = await httpClient.GetAsync($"/events/usereventdetails/{id}");
            var content = await result.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<EventDetailsViewModel>(content);

            return View(deserialized);
        }

        [HttpGet]
        public IActionResult AddEvent()
        {
            List<Audience> audience = Enum.GetValues(typeof(Audience)).Cast<Audience>().ToList();
            List<EventType> eventType = Enum.GetValues(typeof(EventType)).Cast<EventType>().ToList();
            List<LocationType> locationType = Enum.GetValues(typeof(LocationType)).Cast<LocationType>().ToList();

            ViewBag.RequiredAudience = new SelectList(audience);
            ViewBag.RequiredEventType = new SelectList(eventType);
            ViewBag.RequiredLocationType = new SelectList(locationType);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(CrudEventViewModel crudEventViewModel)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44323/");

            var request = new HttpRequestMessage(HttpMethod.Post, "/events");
            request.Content = new StringContent(JsonConvert.SerializeObject(crudEventViewModel));

            await httpClient.SendAsync(request);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> EditEvent(string id)
        {
            List<Audience> audience = Enum.GetValues(typeof(Audience)).Cast<Audience>().ToList();
            List<EventType> eventType = Enum.GetValues(typeof(EventType)).Cast<EventType>().ToList();
            List<LocationType> locationType = Enum.GetValues(typeof(LocationType)).Cast<LocationType>().ToList();

            ViewBag.RequiredAudience = new SelectList(audience);
            ViewBag.RequiredEventType = new SelectList(eventType);
            ViewBag.RequiredLocationType = new SelectList(locationType);

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44323/");

            var result = await httpClient.GetAsync($"/events/clientevent/{id}");
            var content = await result.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<CrudEventViewModel>(content);

            return View(deserialized);
        }

        public async Task<IActionResult> EditEvent(CrudEventViewModel crudEventViewModel)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44323/");

            var request = new HttpRequestMessage(HttpMethod.Put, $"/events/{crudEventViewModel.Id}");
            request.Content = new StringContent(JsonConvert.SerializeObject(crudEventViewModel));

            await httpClient.SendAsync(request);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEvent(string id)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44323/");

            var result = await httpClient.GetAsync($"/events/clientevent/{id}");
            var content = await result.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<CrudEventViewModel>(content);

            return View(deserialized);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44323/");

            var request = new HttpRequestMessage(HttpMethod.Delete, $"/events/{id}");
            await httpClient.SendAsync(request);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        //[Authorize(Roles = "User")]
        public IActionResult FavoriteEvents()
        {
            return View();
        }


        [HttpPost]
        public IActionResult UploadFiles()
        {

            long size = 0;
            var files = Request.Form.Files;

            foreach (var file in files)
            {
                size += file.Length;
                string filename = _environment.ContentRootPath + $@"\UploadedFiles\{file.FileName}";  // Folder-ul UploadedFiles trebuie creat manual, la acelasi nivel cu folder-ele Controllers/Models/Views
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            string message = $"{files.Count} file(s) / {size} bytes uploaded successfully!";
            return Json(message);
        }
    }
}