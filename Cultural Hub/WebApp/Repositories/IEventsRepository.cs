using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repositories
{
    public interface IEventsRepository
    {
        List<Event> GetEvents();
        Event AddEvent(Event e);
        void EditEvent(Event e);
        void DeleteEvent(Event e);
        Event GetEventById(string eventId);

    }
}
