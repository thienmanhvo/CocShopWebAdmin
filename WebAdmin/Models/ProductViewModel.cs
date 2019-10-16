using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdmin.Models
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Price Sale")]
        public double? PriceSale { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Is Sale")]
        public bool? IsSale { get; set; }
        [Display(Name = "Is New")]
        public bool? IsNew { get; set; }
        [Display(Name = "Is Best")]
        public bool? IsBest { get; set; }

        public string CateId { get; set; }
        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }
        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }
        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        public virtual ProductCategoryViewModel Category { get; set; }
    }

    public class ProductIndexViewModel
    {
        public ICollection<ProductViewModel> Products { get; set; }
        public TokenViewModel User { get; set; }
    }

    public class ProductCategoryViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
