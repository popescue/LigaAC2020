using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repositories
{
    public interface ILocationsRepository
    {
        Location AddLocation(Location l);

        Location GetLocationByEventId(string eventId);

        void EditLocation(Location l);

        void DeleteLocation(Location l);

    }
}
