using System.ComponentModel.DataAnnotations.Schema;
namespace Website_BanHang.Models
{
    public class Roles
    {
        public int RoleCode { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public ICollection<Staffs> staffs { get; set; }
    }
}
