using Pharmacy.Core.Domain.Entities;

namespace Pharmacy.Core.DTO.ProductDTO
{
    public class ProductResponseSummary
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? CategoryName { get; set; }

        public decimal? SellPrice { get; set; }

        public int? MinimumStockLevel { get; set; }
    }

    public static class ProductSummaryExtension
    {
        public static ProductResponseSummary ToSummaryDto(this Product product)
        {
            return new ProductResponseSummary
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                CategoryName = product.ProductCategory?.Name,
                SellPrice = product.SellPrice,
                MinimumStockLevel = product.MinimumStockLevel
            };
        }
    }
}
