using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Context;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly CulturalHubContext _culturalHubContext;

        public EventsRepository(CulturalHubContext culturalHubContext)
        {
            _culturalHubContext = culturalHubContext;
        }

        public List<Event> GetEvents()
        {
            return _culturalHubContext.Events
                .Select(e => 
                
                        new Event(e.Id, 
                        e.Title, e.Description, 
                        new Location(e.LocationAddress, (LocationType)e.LocationType), 
                        e.StartsAt, e.Duration, (EventType)e.Type, (Audience)e.Audience, 
                        e.PublishDate, e.IsActive)
                )
                .ToList();
        }

        public Event AddEvent(Event e)
        {
            //_culturalHubContext.Events.Add(e);

            //_culturalHubContext.SaveChanges();

            return e;
        }

        public void EditEvent(Event e)
        {
            //_culturalHubContext.Events.Update(e);

            //_culturalHubContext.SaveChanges();
        }

        public void DeleteEvent(Event e)
        {
            //_culturalHubContext.Events.Remove(e);

            //_culturalHubContext.SaveChanges();
        }

        public Event GetEventById(string eventId)
        {
            //var e = _culturalHubContext.Events.Find(eventId);

            return null;
        }
    }
}
