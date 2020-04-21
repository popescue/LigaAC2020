using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Factories
{
    public static class EventsStore
    {
        private static List<Event> _events;

        static EventsStore()
        {
            InitializeEvents();
        }

        private static void InitializeEvents()
        {
            _events = new List<Event>()
            {
                new Event(
                    "123abc",
                    "catei",
                    "descriere catei",
                    "strada principala",
                    LocationType.Indoor,
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
                    "strada secundara",
                    LocationType.Indoor,
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
                    "strada lalelelor",
                    LocationType.Outdoor,
                    new DateTime(2021, 1, 20, 12, 0, 0),
                    TimeSpan.FromHours(2),
                    EventType.Theatre,
                    Audience.Children,
                    new DateTime(2021, 1, 20, 12, 0, 0),
                    true
                )

            };
        }

        public static List<Event> GetEvents()
        {
            return _events;
        }
    }
}
