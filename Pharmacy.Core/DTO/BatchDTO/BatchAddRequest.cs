using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO.PurchaseDTO;

namespace Pharmacy.Core.DTO.BatchDTO
{
    public class BatchAddRequest
    {
        public int Quantity { get; set; }  // ← الكمية الخاصة بهذا الـ Batch

        public string? Barcode { get; set; }

        public string? BatchNumber { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Batch ToBatch(int productId)
        {
            return new Batch
            {
                Quantity = this.Quantity,
                Barcode = this.Barcode,
                BatchNumber = this.BatchNumber,
                ExpirationDate = this.ExpirationDate,
                ProductId = productId
            };
        }
    }
}

 
