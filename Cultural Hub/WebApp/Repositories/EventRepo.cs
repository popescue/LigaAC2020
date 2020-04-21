using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Context;

namespace WebApp.Repositories
{
    public class EventRepo : IEventRepository
    {
        private CulturalHubContext context;

        public EventRepo(CulturalHubContext context)
        {
            this.context = context;
        }
        public void Add(Event e)
        {
            throw new NotImplementedException();
        }

        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public Event GetEventDetailsById(string Id)
        {
            throw new NotImplementedException();
        }

        public List<Event> GetEvents()
        {
            throw new NotImplementedException();
        }

        public void Update(Event e)
        {
            throw new NotImplementedException();
        }
    }
}
