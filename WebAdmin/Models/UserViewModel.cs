using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Enum;

namespace WebAdmin.Models
{
    public class UserViewModel
    {
        [Display(Name = "FullName")]
        public string FullName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Display(Name = "Avatar")]
        public string AvatarPath { get; set; }
        [Display(Name = "Gender")]
        public MyEnum.Gender Gender { get; set; }
        [Display(Name ="Birthday")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Birthday { get; set; }
        public List<SelectListItem> Genders { get; set; }
    }
    public class UpdateUserRequestViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string AvatarPath { get; set; }
        public MyEnum.Gender Gender { get; set; }
        public string Birthday { get; set; }
    }
    public class ChangePasswordViewModel
    {
        [StringLength(100, MinimumLength = 6, ErrorMessage = "The {0} characters must between {2} and {1} characters.")]
        [Required]
        public string OldPassword { get; set; }
        [StringLength(100, MinimumLength = 6, ErrorMessage = "The {0} characters must between {2} and {1} characters.")]
        [Required]
        public string NewPassword { get; set; }
        [JsonIgnore]
        public TokenViewModel User { get; set; }
    }
    public class IndexUserVewModel
    {
        public ICollection<UserViewModel> Users { get; set; }
        public TokenViewModel User { get; set; }
    }

}
