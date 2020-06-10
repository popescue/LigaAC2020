using System;
using System.Collections.Generic;
using Domain;

namespace Services
{
    public class CrudEvent
    {
        //public EventId Id { get; set; }
        //public ClientId ClientId { get; set; }
        //public EventTitle Title { get; set; }
        //public EventDescription Description { get; set; }
        //public Location Location { get; set; }
        //public EventStartDate StartsAt { get; set; }
        //public EventDuration Duration { get; set; }
        //public EventType Type { get; set; }
        //public Audience Audience { get; set; }
        //public EventPublishDate PublishDate { get; set; }
        //public bool IsActive { get; set; }
        //public bool IsPublished { get; set; }
        //public List<Uri> Pictures { get; set; }

        public string Id { get; set; }
        public Guid ClientId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public LocationType LocationType { get; set; }
        public Audience Audience { get; set; }
        public DateTime EndsAt { get; set; }
        public EventType Type { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartsAt { get; set; }
        public List<Uri> Pictures { get; set; }
    }
}