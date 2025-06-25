using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Pharmacy.Core.Domain.Entities
{
    public class Batch
    { 

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BatchID { set; get; }

        public int Quantity { set; get; }
          
        public string ?Barcode { get; set; }

        public string? BatchNumber { get; set; }

        public DateTime ExpirationDate { set; get; }

        // Denormalized field
        public int ProductId { get; set; }

        [ForeignKey("PurchaseItem")]
        public int PurchaseItemId { get; set; }

        public PurchaseItem? PurchaseItem { get; set; } 
          
        public DamagedProduct ? DamagedProduct { set; get; }

        public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();

        public ICollection<Notification> Notifications { get; set; } =new List<Notification>() ;
    }

}
