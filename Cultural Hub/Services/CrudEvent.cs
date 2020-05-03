using Domain;
using System;
using System.Collections.Generic;

namespace Services.Client
{
    public class CrudEvent
    {
        public EventId Id { get; set; }

        public EventTitle Title { get; set; }

        public EventDescription Description { get; set; }

        public string Address { get; set; }

        public LocationType LocationType { get; set; }

        public Audience Audience { get; set; }

        public EventStartDate StartsAt { get; set; }

        public EventPublishDate PublishDate { get; set; }

        public EventDuration Duration { get; set; }

        public EventType Type { get; set; }

        public bool IsActive { get; set; }

        public bool IsPublished { get; set; }

        public List<Uri> Pictures { get; set; }
    }
}