using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Context;

namespace WebApp.Repositories
{
    public class PicturesRepository : IPicturesRepository
    {
        private readonly CulturalHubContext _culturalHubContext;

        public PicturesRepository(CulturalHubContext culturalHubContext)
        {
            _culturalHubContext = culturalHubContext;
        }

        public List<Picture> GetPicturesForEvent(string eventId)
        {
            return _culturalHubContext.Pictures
                .Where(p => p.EventId == eventId)
                .ToList();
        }

        public Picture AddPicture(Picture p)
        {
            _culturalHubContext.Pictures.Add(p);

            _culturalHubContext.SaveChanges();

            return p;
        }

        public Picture DeletePicture(Picture p)
        {
            _culturalHubContext.Pictures.Remove(p);

            _culturalHubContext.SaveChanges();

            return p;
        }
    }
}
