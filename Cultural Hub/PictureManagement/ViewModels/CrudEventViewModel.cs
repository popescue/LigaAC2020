using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Domain;

namespace PictureManagement.ViewModels
{
    public class CrudEventViewModel
    {
        public string Id { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        [Display(Name = "Titlu")]
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Display(Name = "Descriere")]
        [Required]
        [StringLength(5000)]
        public string Description { get; set; }

        [Display(Name = "Adresa")]
        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Display(Name = "Tip Locatie")]
        [Required]
        public LocationType LocationType { get; set; }

        [Display(Name = "Incepe la")]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartsAt { get; set; }

        [Display(Name = "Se termina la")]
        [Required]
        public DateTime EndsAt { get; set; }

        [Display(Name = "Tipul evenimentului")]
        [Required]
        public EventType Type { get; set; }

        [Display(Name = "Audiență")]
        [Required]
        public Audience Audience { get; set; }

        [Display(Name = "Data publicarii")]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PublishDate { get; set; }

        [Display(Name = "Activ")]
        [Required]
        public bool IsActive { get; set; }
        [Display(Name = "Poze")]
        [Required]
        public List<Uri> Pictures { get; set; }
    }
}
