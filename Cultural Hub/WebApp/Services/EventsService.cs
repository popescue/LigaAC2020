using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Factories;
using WebApp.Models;

namespace WebApp.Services
{
    public class EventsService
    {
        public List<EventDetailsViewModel> GetEventDetails()
        {
            return EventsStore.GetEventDetails();
        }

        public List<EventListViewModel> GetEventList()
        {
            return EventsStore.GetEventList();
        }

        public EventDetailsViewModel GetEventDetailsById(string id)
        {
            return EventsStore.GetEventDetails().Find(e => e.Id == id);
        }
    }
}
