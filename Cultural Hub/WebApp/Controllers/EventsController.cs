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

namespace WebApp.Controllers
{

    public class EventsController : Controller
    {
        private UserEventsService _userEventsService;
        private ClientEventsService _clientEventsService;

        public EventsController(UserEventsService userEventsService, ClientEventsService clientEventsService)
        {
            _userEventsService = userEventsService;
            _clientEventsService = clientEventsService;
        }

        [HttpGet("{id}")]
        public IActionResult Details(string id)
        {
            var eventDetails = _userEventsService.GetUserEventDetailsById(id);
      
            return View(new EventDetailsViewModel()
            {
                Id = eventDetails.Id,
                Title = eventDetails.Title,
                Audience = eventDetails.Audience,
                Description = eventDetails.Description,
                EndsAt = eventDetails.EndsAt,
                LocationType = eventDetails.LocationType,
                LocationAddress = eventDetails.LocationAddress,
                Type = eventDetails.Type,
                StartsAt = eventDetails.StartsAt,
                Pictures = eventDetails.Pictures
            });
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
        public IActionResult AddEvent(CrudEventViewModel crudEventViewModel)
        {          
            var crudEvent = new CrudEvent()           
            {
                ClientId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Title = crudEventViewModel.Title,
                Description = crudEventViewModel.Description,
                Address = crudEventViewModel.Address,
                LocationType = crudEventViewModel.LocationType,
                Audience = crudEventViewModel.Audience,
                EndsAt = crudEventViewModel.EndsAt,
                Type = crudEventViewModel.Type,
                PublishDate = crudEventViewModel.PublishDate,
                IsActive = crudEventViewModel.IsActive,
                StartsAt = crudEventViewModel.StartsAt,
                Pictures = crudEventViewModel.Pictures
            };

            _clientEventsService.AddEvent(crudEvent);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult EditEvent(string eventId)
        {
            List<Audience> audience = Enum.GetValues(typeof(Audience)).Cast<Audience>().ToList();
            List<EventType> eventType = Enum.GetValues(typeof(EventType)).Cast<EventType>().ToList();
            List<LocationType> locationType = Enum.GetValues(typeof(LocationType)).Cast<LocationType>().ToList();

            ViewBag.RequiredAudience = new SelectList(audience);
            ViewBag.RequiredEventType = new SelectList(eventType);
            ViewBag.RequiredLocationType = new SelectList(locationType);

            var crudEvent = _clientEventsService.GetCrudEventViewModelById(eventId);

            return View(new CrudEventViewModel()
            {
                Id = crudEvent.Id,
                Title = crudEvent.Title,
                Audience = crudEvent.Audience,
                Description = crudEvent.Description,
                EndsAt = crudEvent.EndsAt,
                LocationType = crudEvent.LocationType,
                Address = crudEvent.Address,
                Type = crudEvent.Type,
                StartsAt = crudEvent.StartsAt,
                Pictures = crudEvent.Pictures,
                IsActive = crudEvent.IsActive,
                PublishDate = crudEvent.PublishDate
            });
        }

        public IActionResult EditEvent(CrudEventViewModel crudEventViewModel)
        {
            var crudEvent = new CrudEvent()
            {
                Id = crudEventViewModel.Id,
                Title = crudEventViewModel.Title,
                Description = crudEventViewModel.Description,
                Address = crudEventViewModel.Address,
                LocationType = crudEventViewModel.LocationType,
                Audience = crudEventViewModel.Audience,
                EndsAt = crudEventViewModel.EndsAt,
                Type = crudEventViewModel.Type,
                PublishDate = crudEventViewModel.PublishDate,
                IsActive = crudEventViewModel.IsActive,
                StartsAt = crudEventViewModel.StartsAt,
                Pictures = crudEventViewModel.Pictures
            };

            _clientEventsService.EditEvent(crudEvent);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult DeleteEvent(string eventId)
        {
            var crudEvent = _clientEventsService.GetCrudEventViewModelById(eventId);

            return View(new CrudEventViewModel()
            {
                Id = crudEvent.Id,
                Title = crudEvent.Title,
                Audience = crudEvent.Audience,
                Description = crudEvent.Description,
                EndsAt = crudEvent.EndsAt,
                LocationType = crudEvent.LocationType,
                Address = crudEvent.Address,
                Type = crudEvent.Type,
                StartsAt = crudEvent.StartsAt,
                Pictures = crudEvent.Pictures,
                IsActive = crudEvent.IsActive,
                PublishDate = crudEvent.PublishDate
            });
        }

        public IActionResult Delete(string eventId)
        {
            _clientEventsService.DeleteEvent(eventId);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        //[Authorize(Roles = "User")]
        public IActionResult FavoriteEvents()
        {
            return View();
        }
    }
}