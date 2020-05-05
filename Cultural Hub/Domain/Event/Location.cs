using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Location
    {
        public string EventId { get; }
        public string Address { get; }
        public LocationType Type { get; }

        public Location(string Address, LocationType Type)
        {
            if (string.IsNullOrWhiteSpace(Address))
                throw new ArgumentException("Address cannot be null or blank", "Address");

            if (Address.Length > 100)
                throw new ArgumentException("Address must not be longer than 100 characters", "Address");

            if (!Enum.IsDefined(typeof(LocationType), Type))
                throw new ArgumentException("Location Type is not a valid one", "Type");

            this.Address = Address;
            this.Type = Type;
        }

    }

    public enum LocationType
    {
        Indoor,
        Outdoor
    }
}
