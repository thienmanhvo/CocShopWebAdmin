using Microsoft.AspNetCore.Mvc.Rendering;
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
}
