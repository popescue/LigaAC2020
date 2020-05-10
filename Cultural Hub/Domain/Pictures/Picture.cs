using System;
using System.Collections.Generic;

namespace Domain
{
    public class Picture
    {
        public EventId EventId { get; }
        public string Description { get; }
        public Uri Link { get; }

        public Picture(EventId eventId, string description, Uri link)
        {
            EventId = eventId;
            Description = description;
            Link = link;
        }
    }
}
