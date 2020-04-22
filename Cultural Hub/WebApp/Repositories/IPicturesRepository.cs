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

        Picture AddPicture(Picture p);

        Picture DeletePicture(Picture p);

    }
}
