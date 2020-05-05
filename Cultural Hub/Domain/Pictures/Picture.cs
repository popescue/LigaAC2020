using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Picture
    {
        public string EventId { get; }
        public string Description { get; }
        public Uri Link { get; }

        public Picture(string eventId, string description, Uri link)
        {
            EventId = eventId;
            Description = description;
            Link = link;
        }
    }
}
