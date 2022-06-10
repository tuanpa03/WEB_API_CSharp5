using System.ComponentModel.DataAnnotations.Schema;

namespace Website_BanHang.Models
{
    public class Categroies
    {
        public int CatCode { get; set; }
        public string CatName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public ICollection<Products> products { get; set; }
    }
}
