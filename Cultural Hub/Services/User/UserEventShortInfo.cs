using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class UserEventShortInfo
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string LocationAddress { get; set; }

        public DateTime StartsAt { get; set; }

        public IEnumerable<Uri> Pictures { get; set; }

        public Guid ClientId { get; set; }
    }
}