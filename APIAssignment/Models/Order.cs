using System;
using System.Collections.Generic;

namespace APIAssignment.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderCode { get; set; }
        public int CustomerCodeId { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalMoney { get; set; }
        public string? Note { get; set; }
        public bool Status { get; set; }

        public virtual Customer CustomerCode { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
