using System.ComponentModel.DataAnnotations.Schema;

namespace Website_BanHang.Models
{
    public class Orders
    {
        public int OrderCode { get; set; }
        public int CustomerCode { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalMoney { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }
        public Customers Customers { get; set; }
        public IList<OrderDetails> OrderDetails { get; set; }
    }
}
