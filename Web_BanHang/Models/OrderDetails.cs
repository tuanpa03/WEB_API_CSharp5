using System.ComponentModel.DataAnnotations.Schema;
namespace Website_BanHang.Models
{
    public class OrderDetails
    {
        public Orders Orders { get; set; }
        public int OrderCode { get; set; }

        public Products Products { get; set; }
        public int ProductCode { get; set; }
        public int Quantity { get; set; }
    }
}
