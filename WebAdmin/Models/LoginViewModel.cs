using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdmin.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
    public class TokenViewModel
    {
        public string[] Roles { get; set; }
        public string Fullname { get; set; }
        public string AvatarPath { get; set; }
        public string Email { get; set; }
        public string Access_token { get; set; }
        public DateTime Expires_in { get; set; }
    }
}
