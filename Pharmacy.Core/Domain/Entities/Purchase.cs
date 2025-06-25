using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Pharmacy.Core.Domain.Entities
{
   public class Purchase
    {
          
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseID { set; get; }
        
        public DateTime ? PurchaseDate { set; get; }

        [Precision(18, 2)]
        public decimal ? TotalAmount { set; get; }
         
         
        public float? discount { set; get; }

        [ForeignKey("Supplier")]
        public int SupplierID { set; get; }

        public Supplier? Supplier { set; get; }
    
        public ICollection<PurchaseItem>? PurchaseItems { set; get; } =new List <PurchaseItem>();
    }
}
