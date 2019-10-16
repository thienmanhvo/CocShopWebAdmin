using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAdmin.Models
{
    public class BaseViewModel<T>
    {

        public int StatusCode { get; set; }

        public T Data { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        ///// <summary>
        /////     Will be de-serialize as list property 
        ///// </summary>
        //[JsonProperty(Order = 8)]
        //[JsonExtensionData]
        //public virtual Dictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>();
    }
}
