using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Domain.IRepositoriesContracts;
using Pharmacy.Infrastructure.DbContext;



namespace Pharmacy.Infrastructure.Repositories
{
    public class CategoryMedicineRepository : ICategoryMedicineRepositroy
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<MedicineCategory> _medicineCategories;

        public CategoryMedicineRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _medicineCategories = _dbContext.MedicineCategories;
        }

        public async Task<MedicineCategory?> AddCategoryAsync(MedicineCategory category)
        {

            await _medicineCategories.AddAsync(category);
            await _dbContext.SaveChangesAsync();


            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var cateogry = await _medicineCategories.FindAsync(categoryId);

            if (cateogry is null)
            {
                return false;
            }

            _medicineCategories.Remove(cateogry);
            await _dbContext.SaveChangesAsync();


            return true;
        }

        public async Task<MedicineCategory?> FindCategoryByIdAsync(int categoryId)
        {


            return await _medicineCategories.Include(c => c.Medicines)

                .ThenInclude(p => p.Product)
                            .Include(c => c.Medicines)
                            .ThenInclude(m => m.MedicineType)
                .FirstOrDefaultAsync(c => c.MedicineCategoryID == categoryId);
        }

        public async Task<IEnumerable<MedicineCategory>> GetAllCategoriesAsync()
        {

            return await _medicineCategories.ToListAsync();
        }

        public async Task<bool> UpdateCategoryAsync(MedicineCategory category)
        {
            var existingCategory = await _medicineCategories.FindAsync(category.MedicineCategoryID);
            if (existingCategory == null)
                return false;

            _dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
