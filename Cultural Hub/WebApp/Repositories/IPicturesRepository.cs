using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.StorageModels;

namespace WebApp.Repositories
{
    public interface IPicturesRepository
    {
        List<Picture> GetPicturesForEvent(string eventId);

        List<PictureStorageModel> AddPicturesToEvent(List<PictureStorageModel> pictures);

        void DeleteAllPicturesFromEvent(string eventId);

    }
}
