using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class EventDetailsViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Titlu")]
        public string Title { get; set; }

        [Display(Name = "Descriere")]
        public string Description { get; set; }

        [Display(Name = "Locație")]
        public string LocationAddress { get; set; }

        [Display(Name = "Tipul locației")]
        public string LocationType { get; set; }

        [Display(Name = "Incepe la")]
        public DateTime StartsAt { get; set; }

        [Display(Name = "Durata")]
        public TimeSpan Duration { get; set; }

        [Display(Name = "Tipul evenimentului")]
        public string Type { get; set; }

        [Display(Name = "Audiență")]
        public string Audience { get; set; }

        [Display(Name = "Poze")]
        public List<Uri> Pictures { get; set; }
    }
}
