using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdmin.Enum
{
    public class MyEnum
    {
        public enum Gender
        {
            [Display(Name = "Unknown")]
            Unknown = 0,
            [Display(Name = "Male")]
            Male = 1,
            [Display(Name = "Female")]
            Female = 2,
        }
    }
}
