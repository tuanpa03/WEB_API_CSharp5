using System.ComponentModel.DataAnnotations.Schema;

namespace Website_BanHang.Models
{
    public class Customers
    {
        public int CustomerCode { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool Gender { get; set; } //true: Nam, false: Nữ
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
        public bool Status { get; set; } //true: Hoạt động, False: Không hoạt động
        public ICollection<Orders> orders { get; set; }
    }
}
