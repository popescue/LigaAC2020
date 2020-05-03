using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repositories
{
    public interface IPicturesRepository
    {
        List<Picture> GetPicturesForEvent(string eventId);

        List<Picture> AddPicturesToEvent(List<Picture> pictures);

        void DeleteAllPicturesFromEvent(string eventId);

        void SoftDeleteAllPicturesFromEvent(string eventId);
    }
}
