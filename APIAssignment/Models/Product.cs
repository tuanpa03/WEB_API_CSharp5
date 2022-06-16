using System;
using System.Collections.Generic;

namespace APIAssignment.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductCode { get; set; }
        public int CatCodeId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public string? Image { get; set; }
        public float Price { get; set; }
        public string? Note { get; set; }

        public virtual Categroie CatCode { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
