using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdmin.Models
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double? PriceSale { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool? IsSale { get; set; }
        public bool? IsNew { get; set; }
        public bool? IsBest { get; set; }
        public string CateId { get; set; }
        //public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        // public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string ImagePath { get; set; }

        public virtual ProductCategoryViewModel Category { get; set; }
    }

    public class ProductIndexViewModel
    {
        public ProductViewModel Product { get; set; }
        public TokenViewModel User { get; set; }
    }

    public class ProductCategoryViewModel
    {
        public ProductViewModel Product { get; set; }
        public TokenViewModel User { get; set; }
    }

}
