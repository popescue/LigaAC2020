using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Stores
{
    public static class PicturesStore
    {
        private static List<Picture> _pictures;

        static PicturesStore()
        {
            _pictures = new List<Picture>
            {
                new Picture
                {
                    EventId = "123abc",
                    Description = "description pic1",
                    Link = new Uri("https://agrobiznes.md/wp-content/uploads/2013/02/capre.jpg")
                },
                new Picture
                {
                    EventId = "123abc",
                    Description = "description pic2",
                    Link = new Uri("http://www.zooland.ro//data/articles/40/1407/Clipboard011-0n.jpg")
                },
                new Picture
                {
                    EventId = "123abc",
                    Description = "description pic2",
                    Link = new Uri("https://i.ytimg.com/vi/Wz01h5lRFHM/maxresdefault.jpg")
                },
                new Picture
                {
                    EventId = "345dfr",
                    Description = "description pic1",
                    Link = new Uri("https://agrobiznes.md/wp-content/uploads/2013/02/capre.jpg")
                },
                new Picture
                {
                    EventId = "345dfr",
                    Description = "description pic2",
                    Link = new Uri("http://www.zooland.ro//data/articles/40/1407/Clipboard011-0n.jpg")
                },
                new Picture
                {
                    EventId = "345dfr",
                    Description = "description pic2",
                    Link = new Uri("https://i.ytimg.com/vi/Wz01h5lRFHM/maxresdefault.jpg")
                },
                new Picture
                {
                    EventId = "987thu",
                    Description = "description pic1",
                    Link = new Uri("https://agrobiznes.md/wp-content/uploads/2013/02/capre.jpg")
                },
                new Picture
                {
                    EventId = "987thu",
                    Description = "description pic2",
                    Link = new Uri("http://www.zooland.ro//data/articles/40/1407/Clipboard011-0n.jpg")
                },
                new Picture
                {
                    EventId = "987thu",
                    Description = "description pic2",
                    Link = new Uri("https://i.ytimg.com/vi/Wz01h5lRFHM/maxresdefault.jpg")
                }
            };
        }

        internal static List<Picture> GetPicturesForEvent(string id)
        {
            return _pictures.Where(e => e.EventId == id).ToList();
        }
    }
}
