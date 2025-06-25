using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO.BatchDTO;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.DTO.PurchaseDTO
{


    public class PurchaseItemAddRequest
    {
        [Required]
        public int ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        public List<BatchAddRequest> Batches { get; set; } = new();

        public PurchaseItem ToPurchaseItem()
        {
            return new PurchaseItem
            {
                ProductID = this.ProductID,
                Quantity = this.Quantity,
                PurchasePrice = this.Price,
                Batches = this.Batches.Select(b => b.ToBatch(this.ProductID)).ToList()
            };
        }
    }
}