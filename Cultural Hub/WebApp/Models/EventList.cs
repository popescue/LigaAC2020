using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
     public class EventList
    {
        public string eventType { get; set; }
        public string location { get; set; }
        public int availableSeats { get; set; }
    }
}
