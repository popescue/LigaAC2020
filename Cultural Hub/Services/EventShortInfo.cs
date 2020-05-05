using System;
using System.Collections.Generic;

namespace Services
{
    public class EventShortInfo
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string LocationAddress { get; set; }

        public DateTime StartsAt { get; set; }

        public List<Uri> Pictures { get; set; }
    }
}