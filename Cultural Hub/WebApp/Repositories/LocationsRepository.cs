using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Context;

namespace WebApp.Repositories
{
    public class LocationsRepository : ILocationsRepository
    {
        private readonly CulturalHubContext _culturalHubContext;

        public LocationsRepository(CulturalHubContext culturalHubContext)
        {
            _culturalHubContext = culturalHubContext;
        }

        public Location AddLocation(Location l)
        {
            _culturalHubContext.Locations.Add(l);

            _culturalHubContext.SaveChanges();

            return l;
        }

        public Location GetLocationByEventId(string eventId)
        {
            var location = _culturalHubContext.Locations.Single(l => l.EventId == eventId);

            return location;
        }

        public void EditLocation(Location l)
        {
            _culturalHubContext.Locations.Update(l);

            _culturalHubContext.SaveChanges();
        }

        public void DeleteLocation(Location l)
        {
            _culturalHubContext.Locations.Remove(l);

            _culturalHubContext.SaveChanges();
        }
    }
}
