using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.StorageModels
{
    public class PictureStorageModel
    {
        public string Id { get; set; }

        public string EventId { get; set; }

        public string Description { get; set; }

        public Uri Link { get; set; }

        public DateTime? Deleted { get; set; }

    }
}
