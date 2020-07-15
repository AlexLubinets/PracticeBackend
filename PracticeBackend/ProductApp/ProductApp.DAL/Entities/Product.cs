using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApp.DAL.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
