using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WebApp.StorageModels
{
    public class PictureStorageModel
    {
        public int Id { get; set; }

        public string EventId { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public DateTime? Deleted { get; set; }

        [ForeignKey("EventId")] public EventStorageModel Event { get; set; }
    }
}