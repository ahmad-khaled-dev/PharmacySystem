using Microsoft.AspNetCore.Http;
using Pharmacy.Core.Domain.Entities;

namespace Pharmacy.Core.IServiceContracts
{
    public interface ICategoryProductService
    {
        Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync();

        Task<ProductCategory> AddCategoryAsync(ProductCategory productCategory, IFormFile? Image);

        Task<bool> DeleteCategoryAsync(int categoryId);

        Task<ProductCategory?> FindCategoryByIdAsync(int categoryId);

        Task<bool> UpdateCategoryAsync(ProductCategory productCategory, IFormFile? Image = null);


    }
}
