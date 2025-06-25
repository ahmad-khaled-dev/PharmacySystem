using Microsoft.AspNetCore.Http;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Domain.IRepositoriesContracts;
using Pharmacy.Core.IServiceContracts;




namespace Pharmacy.Core.Services
{
    public class CategoryProductService : ICategoryProductService
    {
        private readonly ICategoryProductsRepositroy _categoryProductsRepositroy;
        private readonly IFileStorageService _fileService;

        public CategoryProductService(ICategoryProductsRepositroy categoryProductsRepositroy, IFileStorageService fileService)
        {
            _categoryProductsRepositroy = categoryProductsRepositroy;
            _fileService = fileService;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync()
        {
            var categories = await _categoryProductsRepositroy.GetAllCategoriesAsync();


            categories = categories.Select(ca => new ProductCategory()
            {
                CategoryId = ca.CategoryId,
                Name = ca.Name,
                Products = ca.Products,
                Image = _fileService.GetFullImageUrl(ca.Image)
            });

            return categories;
        }

        public async Task<ProductCategory> AddCategoryAsync(ProductCategory productCategory, IFormFile? Image)
        {

            if (Image is not null)
            {
                productCategory.Image = await _fileService.SaveFileAsync(Image, "ProductCategory");
            }


            var categoryProduct = await _categoryProductsRepositroy.AddCategoryAsync(productCategory);


            categoryProduct.Image = _fileService.GetFullImageUrl(categoryProduct.Image);

            return categoryProduct;
        }


        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await _categoryProductsRepositroy.FindCategoryByIdAsync(categoryId);


            if (category?.Products?.Count > 0) return false;


            var result = await _categoryProductsRepositroy.DeleteCategoryAsync(categoryId);


            if (result && !string.IsNullOrEmpty(category.Image))
            {
                _fileService.DeleteFile(category.Image);
            }


            return result;
        }

        public async Task<ProductCategory?> FindCategoryByIdAsync(int categoryId)
        {
            var category = await _categoryProductsRepositroy.FindCategoryByIdAsync(categoryId);

            if (category == null)
                return null;


            category.Image = _fileService.GetFullImageUrl(category.Image);

            return category;
        }

        public async Task<bool> UpdateCategoryAsync(ProductCategory productCategory, IFormFile? Image = null)
        {

            var existingCategory = await _categoryProductsRepositroy.FindCategoryByIdAsync(productCategory.CategoryId);

            if (existingCategory == null)
            {
                return false;
            }

            string? imagePath = existingCategory.Image;

            if (Image != null && Image.Length > 0)
            {
                _fileService.DeleteFile(existingCategory.Image!); // حذف القديمة
                productCategory.Image = await _fileService.SaveFileAsync(Image, "ProductCategory");
            }

            await _categoryProductsRepositroy.UpdateCategoryAsync(productCategory);

            return true;
        }
    }
}
