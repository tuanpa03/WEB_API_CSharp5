using System;
using System.Collections.Generic;

namespace APIAssignment.Models
{
    public partial class OrderDetail
    {
        public int OrderCodeId { get; set; }
        public int ProductCodeId { get; set; }
        public int Quantity { get; set; }

        public virtual Order OrderCode { get; set; } = null!;
        public virtual Product ProductCode { get; set; } = null!;
    }
}
