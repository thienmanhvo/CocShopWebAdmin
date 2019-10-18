using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdmin.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Display(Name ="Role Name")]
        public string Name { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }
        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }
        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }
    }
    public class CreateRoleRequestViewModel
    {
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The {0} characters must between {2} and {1} characters.")]
        [Required]
        public string Name { get; set; }
    }
    public class IndexRoleVewModel
    {
        public ICollection<RoleViewModel> Locations { get; set; }
        public TokenViewModel User { get; set; }
    }

}
