using Microsoft.AspNetCore.Mvc.Rendering;
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
        [Required(ErrorMessage = "* Introduceti un ID valid")]
        public string Id { get; set; }

        [Display(Name = "Titlu")]
        [Required(ErrorMessage = "* Introduceti un titlu valid")]
        public string Title { get; set; }

        [Display(Name = "Descriere")]
        public string Description { get; set; }

        [Display(Name = "Locație")]
        [Required(ErrorMessage = "* Introduceti o locatie valida")]
        public string LocationAddress { get; set; }

        [Display(Name = "Tipul locației")]
        [Required(ErrorMessage = "* Selectati tipul locatiei")]
        public string LocationType { get; set; }

        [Display(Name = "Incepe la")]
        [Required(ErrorMessage = "* Selectati data si ora")]
        public DateTime StartsAt { get; set; }

        [Display(Name = "Se termina la")]
        [Required(ErrorMessage = "* Introduceti o durata valida")]
        public DateTime EndsAt { get; set; }

        [Display(Name = "Tipul evenimentului")]
        [Required(ErrorMessage = "* Introduceti tipul evenimetului")]
        public string Type { get; set; }

        [Display(Name = "Audiență")]
        public string Audience { get; set; }

        [Display(Name = "Poze")]
        public List<Uri> Pictures { get; set; }
    }
}
