using System.ComponentModel.DataAnnotations;
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
        [RegularExpression(@"^[a-z0-9](\.?[a-z0-9]){5,}@g(oogle)?mail\.com$", ErrorMessage = "Vui lòng nhập đúng định dạng email")]
        public string Email { get; set; }
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Hãy nhập MK đúng định dạng, có ít nhất 1 chữ số, 1 chữ cái hoa, thường, có 1 ký tự đặc biệt và ít nhất 8 chữ cái")]
        public string Password { get; set; }
        public string StaffName { get; set; }
        public bool Gender { get; set; } 
        public string Address { get; set; }
        [RegularExpression(@"(84|0[9])+([0-9]{8})\b", ErrorMessage = @"Vui lòng nhập đúng định dạng SĐT")]
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }
        public Roles Roles { get; set; }
    }
}
