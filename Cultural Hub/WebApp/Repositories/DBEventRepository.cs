using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Context;

namespace WebApp.Repositories
{
    public class DBEventRepository : IEventRepository
    {
        private CulturalHubContext context;

        public DBEventRepository()
        {

        }

        public DBEventRepository(CulturalHubContext context)
        {
            this.context = context;
        }
        public Event Add(Event e)
        {
            context.Events.Add(e);
            context.SaveChanges();
            return e;
        }

        public Event Delete(string Id)
        {
            Event toDelete = context.Events.Find(Id);
            if (toDelete != null)
            {
                context.Events.Remove(toDelete);
                context.SaveChanges();
            }
            return toDelete;
        }

        public Event GetEventDetailsById(string Id)
        {
            return context.Events.Find(Id);
        }

        public List<Event> GetEvents()
        {
            return context.Events.ToList();
        }

        public Event Update(Event e)
        {
            var eventChange = context.Events.Attach(e);
            eventChange.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return e;
        }
    }
}
