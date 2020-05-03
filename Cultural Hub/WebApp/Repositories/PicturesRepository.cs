using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Context;
using WebApp.StorageModels;

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
                .Select(x => new Picture(x.EventId, x.Description, x.Link))
                .ToList();
        }

        public List<PictureStorageModel> AddPicturesToEvent(List<PictureStorageModel> pictures)
        {
            pictures.ForEach(p => p.Id = Guid.NewGuid().ToString());

            _culturalHubContext.Pictures.AddRange(pictures);

            _culturalHubContext.SaveChanges();

            return pictures;
        }

        public void DeleteAllPicturesFromEvent(string eventId)
        {
            var picturesDB = _culturalHubContext.Pictures.Where(p => p.EventId == eventId).ToList();

            _culturalHubContext.Pictures.RemoveRange(picturesDB);

            _culturalHubContext.SaveChanges();
        }

        public void SoftDeleteAllPicturesFromEvent(string eventId)
        {
            var picturesDB = _culturalHubContext.Pictures.Where(p => p.EventId == eventId).ToList();

            picturesDB.ForEach(p => p.Deleted = DateTime.Now);

            _culturalHubContext.Pictures.UpdateRange(picturesDB);

            _culturalHubContext.SaveChanges();
        }
    }
}
