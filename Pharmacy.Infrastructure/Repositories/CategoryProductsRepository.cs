using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Domain.IRepositoriesContracts;
using Pharmacy.Infrastructure.DbContext;



namespace Pharmacy.Infrastructure.Repositories
{
    public class CategoryProductsRepository : ICategoryProductsRepositroy
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<ProductCategory> _productCategories;

        public CategoryProductsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _productCategories = _dbContext.ProductCategories;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync()
        { 
            return await _productCategories.ToListAsync();
        }



        public async Task<ProductCategory> AddCategoryAsync(ProductCategory category)
        {

            await _productCategories.AddAsync(category);
            await _dbContext.SaveChangesAsync();


            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var cateogry = await _productCategories.FindAsync(categoryId);

            if (cateogry is null)
            {
                return false;
            }

            _productCategories.Remove(cateogry);
            await _dbContext.SaveChangesAsync();


            return true;
        }

        public async Task<ProductCategory?> FindCategoryByIdAsync(int categoryId)
        { 

            return await _productCategories.Include(pc => pc.Products)
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

        
        public async Task<bool> UpdateCategoryAsync(ProductCategory category)
        {
            var existingCategory = await _productCategories.FindAsync(category.CategoryId);
            if (existingCategory == null)
                return false;

            _dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }


}
