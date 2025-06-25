using Pharmacy.Core.Domain.Entities;


namespace Pharmacy.Core.Domain.IRepositoriesContracts
{
    public interface ICategoryProductsRepositroy
    {
         
            Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync();

            Task<ProductCategory> AddCategoryAsync(ProductCategory category);

            Task<bool> DeleteCategoryAsync(int categoryId);

            Task<ProductCategory?> FindCategoryByIdAsync(int categoryId);

            Task<bool> UpdateCategoryAsync(ProductCategory category);
         
        }
}
