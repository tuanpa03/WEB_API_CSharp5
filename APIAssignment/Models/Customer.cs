using System;
using System.Collections.Generic;

namespace APIAssignment.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerCode { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string? Image { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = null!;
    }
}
