using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdmin.Models
{
    public class OrderViewModel
    {
        public string Id { get; set; }

        public string CreatedUserId { get; set; }

        public virtual UserViewModel CreatedUser { get; set; }

        public decimal TotalPrice { get; set; }

        public string LocationId { get; set; }

        public virtual LocationViewModel Location { get; set; }

        public virtual bool? IsCash { get; set; }

        public string PaymentId { get; set; }

        public int? TotalQuantity { get; set; }

        public string Status { get; set; }

        public string DeliveryUserId { get; set; }

        public virtual UserViewModel DeliveryUser { get; set; }

        public virtual ICollection<OrderDetailViewModel> OrderDetail { get; set; }
    }
    public class OrderDetailViewModel
    {
        public string Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal? TotalPrice { get; set; }

        public decimal? Price { get; set; }
    }
    public class IndexOrderVewModel
    {
        public ICollection<OrderViewModel> Orders { get; set; }
        public TokenViewModel User { get; set; }
    }

}
