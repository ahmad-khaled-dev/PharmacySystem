using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Core.Domain.Entities
{
    public class InventoryItem
    {
        public int InventoryItemID { get; set; }

        [ForeignKey("Batch")]
        public int BatchId { get; set; }
        public Batch ?Batch { get; set; }

        [ForeignKey("SaleItem")]
        public int SaleItemId { get; set; }

        public SaleItem? SaleItem { get; set; }

        public int QuantitySoldFromBatch { get; set; }
    }

}
