using Microsoft.AspNetCore.Http;
using Pharmacy.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.DTO.ProductCategoryDTO
{
    public class ProductCategoryAddRequest
    {
        [Required]
        public string Name { set; get; }

        public IFormFile? Image { set; get; }



        public ProductCategory ToCategoryProduct(string? ImagePath = null)
        {
            return new ProductCategory()
            {
                Name = this.Name,
                Image = ImagePath
            };
        }
    }
}
