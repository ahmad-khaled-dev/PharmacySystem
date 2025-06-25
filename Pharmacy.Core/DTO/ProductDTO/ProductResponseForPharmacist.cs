using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO.MedicineDTO;
using Pharmacy.Core.DTO.ProductDTO;
using System.ComponentModel.DataAnnotations;


namespace Pharmacy.Core.DTO.ProductDTO
{
    public class ProductResponseForPharmacist
    {
        public int ProductId { set; get; }

        [Required]
        public string Name { set; get; }

        public string? Description { set; get; }

        public int? CategoryId { set; get; }

        public string? CategoryName { set; get; }

        public decimal? SellPrice { set; get; }

        public int? MinimumStockLevel { get; set; }

        public MedicineResponse? MedicineResponse { set; get; }
    }

    public static class ProductForPharmacistExtenion
    {
        public static ProductResponseForPharmacist ToProductResponsePharmacist(this Product product)
        {

            return new ProductResponseForPharmacist()
            {
                ProductId = product.ProductId,
                CategoryId = product.ProductCategory?.CategoryId,
                CategoryName = product.ProductCategory?.Name,
                Description = product.Description,
                MinimumStockLevel = product.MinimumStockLevel,
                Name = product.Name,
                SellPrice = product.SellPrice,
                MedicineResponse = product?.Medicine?.ToMedicineResponse()
            };
        }
    }
}