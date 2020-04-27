using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.StorageModels;

namespace WebApp.Repositories
{
    public interface IEventsRepository
    {
        Event GetEventById(string eventId);

        List<Event> GetEvents();

        Event AddEvent(EventStorageModel e);

        void EditEvent(EventStorageModel e);

        void DeleteEvent(string eventId);
    }
}
