using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApp.Repositories
{
    public interface IEventsRepository
    {
        Event GetEventById(string eventId);

        Event AddEvent(Event e);

        void EditEvent(Event e);

        void DeleteEvent(string eventId);

        void AddToFavorites(Guid userId, string id);
    }
}
