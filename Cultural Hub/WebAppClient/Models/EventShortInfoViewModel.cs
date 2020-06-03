using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebAppClient.Models
{
    public class EventShortInfoViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Titlu")]
        public string Title { get; set; }

        [Display(Name = "Locație")]
        public string LocationAddress { get; set; }

        [Display(Name = "Incepe la")]
        public DateTime StartsAt { get; set; }

        [Display(Name = "Poze")]
        public List<Uri> Pictures { get; set; }
    }
}
