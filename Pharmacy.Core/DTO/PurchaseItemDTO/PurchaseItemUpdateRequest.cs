using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO.BatchDTO;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.DTO
{
    public class PurchaseItemUpdateRequest
    {
        
            public int PurchaseItemID { set; get; }

            [Required]
            public int ProductID { get; set; }

            [Required]
            public int Quantity { get; set; }
         
            [Required]
            public decimal Price { get; set; }

        public List<BatchUpdateRequest> Batches { get; set; } = new(); // نفس الشيء

        public PurchaseItem ToPurchaseItem()
        {
            return new PurchaseItem
            {
                PurchaseItemID = this.PurchaseItemID,
                ProductID = this.ProductID,
                Quantity = this.Quantity,
                PurchasePrice = this.Price,
                  Batches=this.Batches.Select(b => b.ToBatch(this.ProductID)).ToList()
            };
        }

    }
}