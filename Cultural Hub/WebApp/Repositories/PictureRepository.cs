using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

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
                    Link = new Uri("https://www.rfi.ro/sites/default/files/articol/foto_caini_mutanti_obtinuti_savantii_chinezi_cum_arata_cateii_culturisti_0.jpg")
                },
                new Picture
                {
                    EventId = "123abc",
                    Description = "description pic2",
                    Link = new Uri("https://images.unsplash.com/photo-1507146426996-ef05306b995a?ixlib=rb-1.2.1&auto=format&fit=crop&w=1950&q=80")
                },
                new Picture
                {
                    EventId = "123abc",
                    Description = "description pic2",
                    Link = new Uri("https://mcdn.wallpapersafari.com/medium/67/54/i4SqIP.jpg")
                },
                new Picture
                {
                    EventId = "345dfr",
                    Description = "description pic1",
                    Link = new Uri("https://www.ecopetit.cat/wpic/mpic/4-47690_funny-cat-desktop-wallpapers-1366768-sen-sus-desktop.jpg")
                },
                new Picture
                {
                    EventId = "345dfr",
                    Description = "description pic2",
                    Link = new Uri("http://wallpaperget.com/images/cat-hd-wallpaper-1.jpg")
                },
                new Picture
                {
                    EventId = "345dfr",
                    Description = "description pic2",
                    Link = new Uri("https://wallpaperplay.com/walls/full/2/d/4/206304.jpg")
                },
                new Picture
                {
                    EventId = "987thu",
                    Description = "description pic1",
                    Link = new Uri("https://img1.goodfon.com/wallpaper/nbig/4/7b/oboi-ot-lolita777-kozy-gornye.jpg")
                },
                new Picture
                {
                    EventId = "987thu",
                    Description = "description pic2",
                    Link = new Uri("https://mcdn.wallpapersafari.com/medium/61/86/1036f5.jpg")
                },
                new Picture
                {
                    EventId = "987thu",
                    Description = "description pic2",
                    Link = new Uri("https://img5.goodfon.com/wallpaper/nbig/f/32/koziol-morda-roga-boke.jpg")
                }
            };
        }

        internal static List<Picture> GetPicturesForEvent(string id)
        {
            return _pictures.Where(e => e.EventId == id).ToList();
        }

        public static void CreateEvent(EventCreateModel _event)
        {

            foreach (var item in _event.Pictures)
            {
                _pictures.Add(new Picture
                {
                    EventId = _event.Id,
                    Description = _event.Description,
                    Link = item
                });
            }
        }
        public static Picture GetPictureById(string id)
        {
            return _pictures.FirstOrDefault(_ => _.EventId == id);
        }
        public static void DeletePicture(Picture _pic)
        {
            _pictures.Remove(_pic);
        }
    }
}

