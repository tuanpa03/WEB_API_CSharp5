using System.ComponentModel.DataAnnotations.Schema;

namespace Website_BanHang.Models
{
    public class Categroies
    {
        public Categroies()
        {
            this.products = new List<Products>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CatCode { get; set; }
        public string CatName { get; set; }
        public byte[] Image { get; set; }//hihihih
        public string Description { get; set; }
        public ICollection<Products> products { get; set; }
    }
}
