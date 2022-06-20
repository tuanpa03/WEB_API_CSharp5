using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website_BanHang.Models
{
    public class Categroies
    {
        public Categroies()
        {
            this.products = new HashSet<Products>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CatCode { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9]", ErrorMessage = @"Vui lòng nhập đúng định dạng danh mục")]
        public string CatName { get; set; }
        public byte[] Image { get; set; }//hihihih
        public string Description { get; set; }
        public ICollection<Products> products { get; set; }
    }
}
