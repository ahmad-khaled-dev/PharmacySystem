using Pharmacy.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;
 

namespace Pharmacy.Core.DTO
{
   public class PurchaseUpdateRequest
    {
        [Required]
        public int PurchaseID { set; get; }
       
        public int SupplierID { get; set; }

        public DateTime? PurchaseDate { get; set; } = DateTime.Now;

        public decimal? Discount { get; set; } 

        public decimal TotalAmount { get; set; }

        // List of PurchaseItem
        public List<PurchaseItemUpdateRequest> PurchaseItems { get; set; } = new();


        public Purchase ToPurchase()
        {
            return new Purchase
            {
                PurchaseID = this.PurchaseID,
                SupplierID = this.SupplierID,
                PurchaseDate = this.PurchaseDate,
                discount = this.Discount.HasValue ? (float?)this.Discount : null,
                TotalAmount = this.TotalAmount,
                PurchaseItems = this.PurchaseItems?.Select(i => i.ToPurchaseItem()).ToList()
            };
        }

    }
}
