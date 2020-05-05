using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Client
{
    public class ClientEventShortInfo
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string LocationAddress { get; set; }

        public DateTime StartsAt { get; set; }

        public DateTime PublishDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsPublished { get; set; }

        public List<Uri> Pictures { get; set; }
    }
}
