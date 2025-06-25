
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Core.Domain.Entities
{
  public  class ProductCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { set; get; }

        public string  Name { set; get; }

        public string? Image { set; get; }

        public ICollection<Product>? Products { set; get; } = new List<Product>();
    }
}
