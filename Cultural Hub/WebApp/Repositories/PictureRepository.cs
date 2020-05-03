using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repositories
{
    public static class PictureRepository
    {
        private static List<Picture> _pictures;

        static PictureRepository()
        {
            _pictures = new List<Picture>
            {
                new Picture
                {
                    EventId = "123abc",
                    Description = "description pic1",
                    Link = new Uri("https://image.stirileprotv.ro/media/images/680xX/Apr2010/60415012.jpg")
                },
                new Picture
                {
                    EventId = "123abc",
                    Description = "description pic2",
                    Link = new Uri("https://s3.publi24.ro/vertical-ro-f646bd5a/extralarge/20190106/1426/aa23d5ae00e464c48f9d6bb01a05c3fd.jpg")
                },
                new Picture
                {
                    EventId = "123abc",
                    Description = "description pic3",
                    Link = new Uri("https://i.ytimg.com/vi/Wz01h5lRFHM/maxresdefault.jpg")
                },
                new Picture
                {
                    EventId = "345dfr",
                    Description = "description pic1",
                    Link = new Uri("https://www.animalepierdute.ro/wp-content/uploads/2019/07/pisica-scottish-fold-699x414.jpg")
                },
                new Picture
                {
                    EventId = "345dfr",
                    Description = "description pic2",
                    Link = new Uri("https://i.ytimg.com/vi/Wz01h5lRFHM/maxresdefault.jpg")
                },
                new Picture
                {
                    EventId = "345dfr",
                    Description = "description pic3",
                    Link = new Uri("https://storage0.dms.mpinteractiv.ro/media/401/721/10072/16015503/1/pisici.jpg")
                },
                new Picture
                {
                    EventId = "987thu",
                    Description = "description pic2",
                    Link = new Uri("https://agrobiznes.md/wp-content/uploads/2013/02/capre.jpg")
                },
                new Picture
                {
                    EventId = "987thu",
                    Description = "description pic3",
                    Link = new Uri("https://evz.ro/wp-content/uploads/2016/01/a-apasat-pe-sarpe-si-i-au-iesit-din-stomac-doua-capre-intregi-vi.jpg")
                }
            };
        }

        internal static List<Picture> GetPicturesForEvent(string id)
        {
            return _pictures.Where(e => e.EventId == id).ToList();
        }
    }
}
