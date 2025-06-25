using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO.PurchaseDTO;
 
namespace Pharmacy.Core.DTO.PurchaseDTO
{
    public class PurchaseResponse
    {
        public int PurchaseID { get; set; }

        public int SupplierID { get; set; }
        
        public DateTime? PurchaseDate { get; set; }
        
        public decimal? TotalAmount { get; set; }
        
        public float? Discount { get; set; }
        
        public List<PurchaseItemResponse>? PurchaseItems { get; set; } = new();
    }

}

public static class PurchaseExtensions
{
    public static PurchaseResponse ToPurchaseResponse(this Purchase purchase)
    {
        return new PurchaseResponse
        {
            PurchaseID = purchase.PurchaseID,
            SupplierID = purchase.SupplierID,
            PurchaseDate = purchase.PurchaseDate,
            TotalAmount = purchase.TotalAmount,
            Discount = purchase.discount,
            PurchaseItems = purchase.PurchaseItems?.Select(i => i.ToPurchaseItemResponse()).ToList()
        };
    }

}