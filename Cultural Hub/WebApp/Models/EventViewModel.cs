using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class EventViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Titlu")]
        public string Title { get; set; }
        public string LocationAddress { get; set; }
        public string LocationType { get; set; }
        public DateTime StartsAt { get; set; }
        public TimeSpan Duration { get; set; }
        public string Type { get; set; }
        public string Audience { get; set; }
        public List<Uri> Picture { get; set; }
    }
}
