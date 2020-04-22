using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IEventsService
    {
        List<EventDetailsViewModel> GetEventDetails();
        List<EventListViewModel> GetEventList();
        EventDetailsViewModel GetEventDetailsById(string id);
        EventViewModel GetEvent(string eventId);
        EventViewModel AddEvent(EventViewModel eventViewModel);
        EventViewModel EditEvent(EventViewModel eventViewModel);
        void DeleteEvent(EventViewModel eventViewModel);
    }
}
