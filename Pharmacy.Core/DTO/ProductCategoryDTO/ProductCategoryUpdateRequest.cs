using Microsoft.AspNetCore.Http;
using Pharmacy.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.DTO.ProductCategoryDTO
{
    public class ProductCategoryUpdateRequest
    {
        [Required]
        public int CategoryID { set; get; }

        [Required]
        public string Name { set; get; }

        public IFormFile? Image { set; get; }


        public ProductCategory ToCategoryProduct(string? imagePath = null)
        {
            return new ProductCategory()
            {
                Name = this.Name,
                Image = imagePath,
                CategoryId = this.CategoryID
            };
        }
    }
}
