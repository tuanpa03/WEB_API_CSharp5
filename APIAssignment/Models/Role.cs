using System;
using System.Collections.Generic;

namespace APIAssignment.Models
{
    public partial class Role
    {
        public Role()
        {
            staff = new HashSet<Staff>();
        }

        public int RoleCode { get; set; }
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Staff> staff { get; set; }
    }
}
