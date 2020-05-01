using Domain;
using System;
using System.Collections.Generic;

namespace Services
{
    public class CrudEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public LocationType LocationType { get; set; }
        public DateTime StartsAt { get; set; }
        public int Duration { get; set; }
        public EventType Type { get; set; }
        public Audience Audience { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsActive { get; set; }
        public List<Uri> Pictures { get; set; }
    }
}