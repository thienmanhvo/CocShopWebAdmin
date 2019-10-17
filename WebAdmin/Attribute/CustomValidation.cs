using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdmin.Attribute
{
    public class CheckUrlAttribute : ValidationAttribute
    {
        public string Property { get; set; }
        protected override ValidationResult IsValid(object uriName, ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(uriName as string))
                return Uri.TryCreate(uriName as string, UriKind.Absolute, out Uri uriResult)
                     && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps) == true
                     ? ValidationResult.Success
                     : new ValidationResult($"Invalid URL");
            return ValidationResult.Success;
        }
    }
}
