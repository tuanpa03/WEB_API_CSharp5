using System;
using System.Collections.Generic;

namespace APIAssignment.Models
{
    public partial class Categroie
    {
        public Categroie()
        {
            Products = new HashSet<Product>();
        }

        public int CatCode { get; set; }
        public string CatName { get; set; } = null!;
        public string? Image { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
