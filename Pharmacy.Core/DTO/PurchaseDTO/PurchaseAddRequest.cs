using Azure.Core;
using Pharmacy.Core.Domain.Entities;

namespace Pharmacy.Core.DTO.PurchaseDTO
{
    public class PurchaseAddRequest
    {
 
        public int SupplierID { get; set; }

        public DateTime? PurchaseDate { get; set; } = DateTime.Now;

        public float? Discount { get; set; } = 0;

        public decimal? TotalAmount { get; set; } = 0;

        public List<PurchaseItemAddRequest> PurchaseItems { get; set; } = new ();


        public Purchase ToPurchase()
        {
            return new Purchase
            {
                SupplierID = this.SupplierID,
                PurchaseDate = this.PurchaseDate,
                discount = this.Discount ?? 0,
                TotalAmount = this.TotalAmount ?? 0,
                PurchaseItems = this.PurchaseItems?.Select(i => i.ToPurchaseItem()).ToList()
                            
            };


        }
        }

 }
