using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO.BatchDTO;
using Pharmacy.Core.DTO.PurchaseDTO;

namespace Pharmacy.Core.DTO.PurchaseDTO
{
    public class PurchaseItemResponse
    {
        public int PurchaseItemID { get; set; }

        public int ProductID { get; set; }
        
        public int Quantity { get; set; }
        
        public decimal Price { get; set; }

        public List<BatchResponse> batchResponses { set; get; } = new();
    }

}
public static class PurchaseItemExtensions
{
    public static PurchaseItemResponse ToPurchaseItemResponse(this PurchaseItem item)
    {
        return new PurchaseItemResponse
        {
            PurchaseItemID = item.PurchaseItemID,
            ProductID = item.ProductID,
            Quantity = item.Quantity,
            Price = item.PurchasePrice ?? 0,
            batchResponses = item.Batches.Select(b => b.ToBathcResponse()).ToList()
        };
    }
}
