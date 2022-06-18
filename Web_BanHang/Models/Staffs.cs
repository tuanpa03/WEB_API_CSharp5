using System.ComponentModel.DataAnnotations.Schema;
namespace Website_BanHang.Models
{
    public class Staffs
    {
        public Staffs()
        {
            Roles = new Roles();
        }
        public int StaffCode { get; set; }
        public int RoleCode { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string StaffName { get; set; }
        public bool Gender { get; set; } 
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }
        public Roles Roles { get; set; }
    }
}
