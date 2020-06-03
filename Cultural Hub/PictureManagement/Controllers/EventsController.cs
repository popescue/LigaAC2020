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
using PictureManagement.ViewModels;

namespace PictureManagement.Controllers
{
    [ApiController]
    [Route("events")]
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

        [HttpGet("usereventsshortinfo")]
        public List<EventShortInfoViewModel> GetUserEventsShortInfoList()
        {
            return _userEventsService
                .GetUserEventShortInfoList()
                .Select(e => new EventShortInfoViewModel()
                {
                    Id = e.Id,
                    LocationAddress = e.LocationAddress,
                    Pictures = e.Pictures,
                    StartsAt = e.StartsAt,
                    Title = e.Title
                })
                .ToList();
        }

        [HttpGet("usereventdetails/{id}")]
        public EventDetailsViewModel Details(string id)
        {
            var eventDetails = _userEventsService.GetUserEventDetailsById(id);
      
            return new EventDetailsViewModel()
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
            };
        }

        [HttpGet("clientevent/{id}")]
        public CrudEventViewModel GetCrudEventViewModelById(string id)
        {
            var crudEvent = _clientEventsService.GetCrudEventViewModelById(id);

            return new CrudEventViewModel()
            {
                Id = crudEvent.Id,
                ClientId = crudEvent.ClientId,
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
            };
        }

        [HttpPost]
        public string Create(CrudEventViewModel crudEventViewModel)
        {          
            var crudEvent = new CrudEvent()           
            {
                //ClientId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                ClientId = crudEventViewModel.ClientId,
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

            var result = _clientEventsService.AddEvent(crudEvent);

            return result.Id; 
        }

        [HttpPut("{id}")]
        public void Update(string id, CrudEventViewModel crudEventViewModel)
        {
            var crudEvent = new CrudEvent()
            {
                Id = id,
                ClientId = crudEventViewModel.ClientId,
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

        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _clientEventsService.DeleteEvent(id);
        }
    }
}