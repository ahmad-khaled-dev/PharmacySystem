using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Domain.Entities;

namespace Pharmacy.Core.DTO.SaleItemDTO
{
    public class SaleItemAddRequest
    {
        public int Quantity { get; set; }

        [Precision(18, 2)]
        public decimal? Price { get; set; }

        public int ProductId { get; set; }

        public IFormFile? Image { set; get; }

        public string? ImagePath { set; get; }


        public SaleItem ToSaleItem()
        {
            return new SaleItem
            {
                ProductId = this.ProductId,
                Quantity = this.Quantity,
                Price = this.Price,
                ImagPrescription = ImagePath

            };
        }
    }

}


