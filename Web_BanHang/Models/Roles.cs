using System.ComponentModel.DataAnnotations.Schema;
namespace Website_BanHang.Models
{
    public class Roles
    {
        public Roles()
        {
            staffs = new List<Staffs>();
        }
        public int RoleCode { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public ICollection<Staffs> staffs { get; set; }
    }
}
