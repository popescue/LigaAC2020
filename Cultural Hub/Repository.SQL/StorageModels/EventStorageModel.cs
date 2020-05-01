using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.StorageModels
{
    public class EventStorageModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LocationAddress { get; set; }
        public int LocationType { get; set; }
        public DateTime StartsAt { get; set; }
        public TimeSpan Duration { get; set; }
        public int Type { get; set; }
        public int Audience { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
