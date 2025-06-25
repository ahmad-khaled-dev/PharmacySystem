using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Core.Domain.Entities
{
    public class PurchaseItem
    {

        [Key]
        public int PurchaseItemID { set; get; }

        [ForeignKey("Purchase")]
        public int PurchaseID { set; get; }

        public Purchase? Purchase { set; get; }

        [ForeignKey("Product")]
        public int ProductID { set; get; }

        public Product? Product { set; get; }

        [Precision(18, 2)]
        public decimal? PurchasePrice { set; get; }

        public int Quantity { set; get; }

        public ICollection<Batch> Batches { get; set; } = new List<Batch>();

    }
}
