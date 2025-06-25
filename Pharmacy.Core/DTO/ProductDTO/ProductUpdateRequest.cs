using Microsoft.AspNetCore.Http;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO.MedicineDTO;
using Pharmacy.Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.DTO.ProductDTO
{
    public class ProductUpdateRequest
    {
        [Required]
        public int ProductId { set; get; }

        [Required]
        public string Name { set; get; }

        public string? description { set; get; }

        public int? categoryId { set; get; }

        public IFormFile? Image { set; get; }

        public decimal? SellPrice { set; get; }

        public int? MinimumStockLevel { get; set; }

        public enProductType ProductType { set; get; }

        public MedicineUpdateRequest ? MedicineUpdateRequest { set; get; }

        public Product ToProduct(string ? ImagePath=null)
        {
           var product= new Product()
            {
                CategoryProductID = this.categoryId,
                Description = this.description,
                Name = this.Name,
                MinimumStockLevel = this.MinimumStockLevel,
                ProductId = this.ProductId,
                Image = ImagePath,
                SellPrice = this.SellPrice
 

            };

             

            return product;
        }

    }

}
