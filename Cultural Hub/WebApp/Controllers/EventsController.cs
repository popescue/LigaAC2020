using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Services;
using Services.User;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private UserEventsService _userEventsService;

        public EventsController(UserEventsService userEventsService, IWebHostEnvironment environment)
        {
            _userEventsService = userEventsService;
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

        //[HttpGet]
        //public IActionResult AddEvent()
        //{
        //    var audience = Enum.GetValues(typeof(Audience)).Cast<Audience>().ToList();
        //    var eventType = Enum.GetValues(typeof(EventType)).Cast<EventType>().ToList();
        //    var locationType = Enum.GetValues(typeof(LocationType)).Cast<LocationType>().ToList();

        //    ViewBag.RequiredAudience = new SelectList(audience);
        //    ViewBag.RequiredEventType = new SelectList(eventType);
        //    ViewBag.RequiredLocationType = new SelectList(locationType);

        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddEvent(CrudEventViewModel crudEventViewModel)
        //{
        //    // Translate input model into lower layer request (model)
        //    var serviceModel = FromViewModel(crudEventViewModel);

        //    // Call service responsible with required use case (creating events)
        //    ResponseModel responseModel = _clientEventsServiceMvc.AddEvent(serviceModel);

        //    // Translate lower layer response (model)
        //    if (responseModel.IsSuccess)
        //        return RedirectToAction("Index", "Home");
        //    //return Ok(FromServiceModel(responseModel));
        //    return Problem();

        //    //var httpClient = new HttpClient();
        //    //httpClient.BaseAddress = new Uri("https://localhost:44323/");

        //    ////crudEventViewModel.Id = new EventId(Guid.NewGuid().ToString().Substring(31));
        //    //crudEventViewModel.ClientId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    //var request = new HttpRequestMessage(HttpMethod.Post, "/events");
        //    //var content = JsonConvert.SerializeObject(crudEventViewModel);
        //    //request.Content = new StringContent(content, Encoding.UTF8, "application/json");

        //    //var response = await httpClient.SendAsync(request);

        //    //return RedirectToAction("Index", "Home");
        //}

        //[HttpGet]
        //public async Task<IActionResult> EditEvent(string id)
        //{
        //    var audience = Enum.GetValues(typeof(Audience)).Cast<Audience>().ToList();
        //    var eventType = Enum.GetValues(typeof(EventType)).Cast<EventType>().ToList();
        //    var locationType = Enum.GetValues(typeof(LocationType)).Cast<LocationType>().ToList();

        //    ViewBag.RequiredAudience = new SelectList(audience);
        //    ViewBag.RequiredEventType = new SelectList(eventType);
        //    ViewBag.RequiredLocationType = new SelectList(locationType);

        //    var httpClient = new HttpClient();
        //    httpClient.BaseAddress = new Uri("https://localhost:44323/");

        //    var result = await httpClient.GetAsync($"/events/clientevent/{id}");
        //    var content = await result.Content.ReadAsStringAsync();
        //    var deserialized = JsonConvert.DeserializeObject<CrudEventViewModel>(content);

        //    return View(deserialized);
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditEvent(CrudEventViewModel crudEventViewModel)
        //{
        //    var httpClient = new HttpClient();
        //    httpClient.BaseAddress = new Uri("https://localhost:44323/");

        //    var request = new HttpRequestMessage(HttpMethod.Put, $"/events/{crudEventViewModel.Id}");
        //    var content = JsonConvert.SerializeObject(crudEventViewModel);
        //    request.Content = new StringContent(content, Encoding.UTF8, "application/json");

        //    await httpClient.SendAsync(request);

        //    return RedirectToAction("Index", "Home");
        //}

        //[HttpGet]
        //public async Task<IActionResult> DeleteEvent(string id)
        //{
        //    var httpClient = new HttpClient();
        //    httpClient.BaseAddress = new Uri("https://localhost:44323/");

        //    var result = await httpClient.GetAsync($"/events/clientevent/{id}");
        //    var content = await result.Content.ReadAsStringAsync();
        //    var deserialized = JsonConvert.DeserializeObject<CrudEventViewModel>(content);

        //    return View(deserialized);
        //}

        //public async Task<IActionResult> Delete(string id)
        //{
        //    var httpClient = new HttpClient();
        //    httpClient.BaseAddress = new Uri("https://localhost:44323/");

        //    var request = new HttpRequestMessage(HttpMethod.Delete, $"/events/{id}");
        //    await httpClient.SendAsync(request);

        //    return RedirectToAction("Index", "Home");
        //}

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        //[Authorize(Roles = "User")]
        public IActionResult FavoriteEvents()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult UploadFiles()
        //{
        //    long size = 0;
        //    var files = Request.Form.Files;

        //    foreach (var file in files)
        //    {
        //        size += file.Length;
        //        var filename =
        //            _environment.ContentRootPath +
        //            $@"\UploadedFiles\{file.FileName}"; // Folder-ul UploadedFiles trebuie creat manual, la acelasi nivel cu folder-ele Controllers/Models/Views
        //        using (var fs = System.IO.File.Create(filename))
        //        {
        //            file.CopyTo(fs);
        //            fs.Flush();
        //        }
        //    }

        //    var message = $"{files.Count} file(s) / {size} bytes uploaded successfully!";
        //    return Json(message);
        //}
    }
}