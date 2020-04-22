using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repositories
{
    public class MockEventRepository : IEventRepository
    {
        private List<Event> _events;

        public MockEventRepository()
        {
            _events = new List<Event>()
            {
                new Event(
                    "123abc",
                    "catei",
                    "descriere catei",
                    new Location("strada principala", LocationType.Indoor),
                    new DateTime(2021, 1, 20, 12, 0, 0),
                    TimeSpan.FromHours(2),
                    EventType.Concert,
                    Audience.GeneralAudience,
                    new DateTime(2021, 1, 20, 12, 0, 0),
                    true
                    ),
                new Event(
                    "345dfr",
                    "pisici",
                    "descriere pisici",
                    new Location("strada secundara", LocationType.Indoor),
                    new DateTime(2021, 1, 20, 12, 0, 0),
                    TimeSpan.FromHours(4),
                    EventType.Concert,
                    Audience.GeneralAudience,
                    new DateTime(2021, 1, 20, 12, 0, 0),
                    true
                ),
                new Event(
                    "987thu",
                    "capre",
                    "descriere capre",
                    new Location("strada lalelelor", LocationType.Outdoor),
                    new DateTime(2021, 1, 20, 12, 0, 0),
                    TimeSpan.FromHours(2),
                    EventType.Theatre,
                    Audience.Children,
                    new DateTime(2021, 1, 20, 12, 0, 0),
                    true
                )

            };
        }

        public Event Add(Event e)
        {
            _events.Add(e);
            return e;
        }

        public Event Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public Event GetEventDetailsById(string Id)
        {
            return _events.ElementAt(0);
        }

        public List<Event> GetEvents()
        {
            return _events;
        }

        public Event Update(Event e)
        {
            throw new NotImplementedException();
        }
    }
}
