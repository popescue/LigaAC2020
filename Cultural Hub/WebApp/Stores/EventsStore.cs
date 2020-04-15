using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Factories
{
    public static class EventsStore
    {
        private static List<EventDetailsViewModel> _eventDetailsViewModels;
        private static List<EventListViewModel> _eventListViewModels;

        static EventsStore()
        {
            InitializeEventDetails();
            InitializeEventList();
        }

        private static void InitializeEventDetails()
        {
            _eventDetailsViewModels = new List<EventDetailsViewModel> {

                new EventDetailsViewModel
                {
                    Id = "123abc",
                    Audience = "AG",
                    Duration = TimeSpan.FromHours(2),
                    LocationAddress = "strada principala",
                    LocationType = "interior",
                    StartsAt = new DateTime(2021, 1, 20, 12, 0, 0),
                    Title = "catei",
                    Type = "usturoi",
                    Description = "descriere catei",
                    Pictures = new List<Uri>()
                    {
                        new Uri("https://image.stirileprotv.ro/media/images/680xX/Apr2010/60415012.jpg"),
                        new Uri("https://s3.publi24.ro/vertical-ro-f646bd5a/extralarge/20190106/1426/aa23d5ae00e464c48f9d6bb01a05c3fd.jpg"),
                        new Uri("http://www.zooland.ro//data/articles/40/1407/Clipboard011-0n.jpg")
                    }
                },
                new EventDetailsViewModel
                {
                    Id = "456dbf",
                    Audience = "AG",
                    Duration = TimeSpan.FromHours(6),
                    LocationAddress = "strada secundara",
                    LocationType = "exterior",
                    StartsAt = new DateTime(2021, 1, 20, 16, 0, 0),
                    Title = "pisici",
                    Type = "usturoi",
                    Description = "descriere pisici",
                    Pictures = new List<Uri>()
                    {
                        new Uri("https://www.animalepierdute.ro/wp-content/uploads/2019/07/pisica-scottish-fold-699x414.jpg"),
                        new Uri("https://i.ytimg.com/vi/Wz01h5lRFHM/maxresdefault.jpg"),
                        new Uri("https://storage0.dms.mpinteractiv.ro/media/401/721/10072/16015503/1/pisici.jpg")
                    }
                },
                new EventDetailsViewModel
                {
                    Id = "678ght",
                    Audience = "AG",
                    Duration = TimeSpan.FromHours(3),
                    LocationAddress = "strada lalelelor",
                    LocationType = "exterior",
                    StartsAt = new DateTime(2021, 1, 20, 1, 0, 0),
                    Title = "capre",
                    Type = "concert",
                    Description = "descriere capre",
                    Pictures = new List<Uri>()
                    {
                        new Uri("https://evz.ro/wp-content/uploads/2016/01/a-apasat-pe-sarpe-si-i-au-iesit-din-stomac-doua-capre-intregi-vi.jpg"),
                        new Uri("https://agrobiznes.md/wp-content/uploads/2013/02/capre.jpg"),
                        new Uri("https://agrobiznes.md/wp-content/uploads/2013/02/capre.jpg")
                    }
                }
            };
        }

        private static void InitializeEventList()
        {
            _eventListViewModels = _eventDetailsViewModels.Select(e =>
            {
                var eventListViewModel = new EventListViewModel()
                {
                    Id = e.Id,
                    Title = e.Title,
                    StartsAt = e.StartsAt,
                    LocationAddress = e.LocationAddress,
                    Pictures = e.Pictures
                };

                return eventListViewModel;
            }).ToList();
        }

        public static List<EventDetailsViewModel> GetEventDetails()
        {
            return _eventDetailsViewModels;
        }

        public static List<EventListViewModel> GetEventList()
        {
            return _eventListViewModels;
        }
    }
}
