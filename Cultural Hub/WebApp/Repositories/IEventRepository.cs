using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repositories
{
    public interface IEventRepository
    {
        List<Event> GetEvents();
        Event Add(Event e);
        Event Update(Event e);
        Event Delete(string Id);
        Event GetEventDetailsById(string Id);

    }
}
