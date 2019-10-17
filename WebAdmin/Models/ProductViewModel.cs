using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Attribute;

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
        //public double NoNullPriceSale
        //{
        //    get { return PriceSale ?? 0; }
        //    set { PriceSale = value; }
        //}
        //public bool NoNullIsSale
        //{
        //    get { return IsSale ?? false; }
        //    set { IsSale = true; }
        //}
        //public bool NoNullIsNew
        //{
        //    get { return IsNew ?? false; }
        //    set { IsNew = true; }
        //}
        //public bool NoNullIsBest
        //{
        //    get { return IsBest ?? false; }
        //    set { IsBest = true; }
        //}
    }

    public class ProductIndexViewModel
    {
        public ICollection<ProductViewModel> Products { get; set; }
        public TokenViewModel User { get; set; }
    }

    public class ProductCategoryViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Category")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class UpdateProductViewModel
    {
        public string Id { get; set; }
        [StringLength(100, MinimumLength = 6, ErrorMessage = "The {0} characters must between {2} and {1} characters.")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Quantity { get; set; }
        [Display(Name = "Price Sale")]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public double? PriceSale { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Description")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "The {0} characters must between {2} and {1} characters.")]
        public string Description { get; set; }
        [Display(Name = "Is Sale")]
        public bool? IsSale { get; set; }
        [Display(Name = "Is Best")]
        public bool? IsBest { get; set; }
        public string CateId { get; set; }
        [Display(Name = "Image")]
        public string ImagePath { get; set; }
        public ProductCategoryViewModel Category { get; set; }



        public bool NoNullIsSale
        {
            get { return IsSale ?? false; }
            set { IsSale = value; }
        }
        public bool NoNullIsBest
        {
            get { return IsBest ?? false; }
            set { IsBest = value; }
        }
        public double NoNullPriceSale
        {
            get { return PriceSale ?? 0; }
            set { PriceSale = value; }
        }
    }
    public class ProductEditViewModel
    {
        public UpdateProductViewModel Product { get; set; }
        public TokenViewModel User { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
    public class ProductCreateViewModel : ProductEditViewModel
    {
    }

}
