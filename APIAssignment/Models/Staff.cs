using System;
using System.Collections.Generic;

namespace APIAssignment.Models
{
    public partial class Staff
    {
        public int StaffCode { get; set; }
        public int RoleCodeId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string StaffName { get; set; } = null!;
        public bool Gender { get; set; }
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public string? Note { get; set; }
        public bool Status { get; set; }

        public virtual Role RoleCode { get; set; } = null!;
    }
}
