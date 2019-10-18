using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdmin.Models
{
    public class LocationVewModel
    {
        public string Id { get; set; }
        public string LocationName { get; set; }
    }
    public class CreateLocationRequestViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The {0} characters must between {2} and {1} characters.")]
        public string LocationName { get; set; }
    }
    public class UpdateLocationViewModel
    {
        [Display(Name = "ID")]
        public string Id { get; set; }
        [Display(Name ="Location name *")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The {0} characters must between {2} and {1} characters.")]
        public string LocationName { get; set; }
    }
    public class IndexLocationVewModel
    {
        public ICollection<LocationVewModel> Locations { get; set; }
        public TokenViewModel User { get; set; }
    }

    public class EditLocationVewModel
    {
        public UpdateLocationViewModel Location { get; set; }
        public TokenViewModel User { get; set; }
    }
    public class CreateLocationVewModel : EditLocationVewModel
    {
    }

}
