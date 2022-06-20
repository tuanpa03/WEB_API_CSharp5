using System.ComponentModel.DataAnnotations.Schema;
namespace Website_BanHang.Models
{
    public class Products
    {
        public Products()
        {
            OrderDetails = new List<OrderDetails>();
        }
        public int ProductCode { get; set; }
        public int CatCode { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public byte[] Image { get; set; }
        public float Price { get; set; }
        public string Note { get; set; }
        public IList<OrderDetails> OrderDetails { get; set; }
        public Categroies Categroies { get; set; }
    }
}
