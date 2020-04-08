using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Location
    { 
        public string Address { get; set; }
        public LocationType Type { get; set; }
    }

    public enum LocationType
    {
        Indoor,
        Outdoor
    }
}
