using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repositories
{
    interface IEventRepository
    {
        List<Event> GetEvents();
        void Add(Event e);
        void Update(Event e);
        void Delete(string Id);
        Event GetEventDetailsById(string Id);

    }
}
