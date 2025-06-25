using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Domain.IRepositoriesContracts;
using Pharmacy.Core.Enum;
using Pharmacy.Infrastructure.DbContext;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<Product> _products;
    

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _products = dbContext.Products;
    }

    public async Task<Product> AddProduct(Product product)
    {
        var category = await _dbContext.ProductCategories.FindAsync(product.CategoryProductID);
         
        if (category is null)
            return null;

        await _products.AddAsync(product);
        await _dbContext.SaveChangesAsync();

        product.ProductCategory = category;

        return product;
    }

    public async Task<bool> DeleteProduct(int productId)
    {
        var product = await _products.Include(p => p.Medicine).FirstOrDefaultAsync(p => p.ProductId == productId);
        if (product == null) return false;

        if (product.Medicine != null && product.ProductType != enProductType.Medicine)
        {
            _dbContext.Medicines.Remove(product.Medicine);
        }

        _products.Remove(product);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Product?> FindProductById(int productId)
    {
        return await _products
           .Include(p => p.ProductCategory)
            .Include(p => p.Medicine)
            .ThenInclude(m => m.Category)
            .Include(p => p.Medicine)
            .ThenInclude(m => m.MedicineType)
            .FirstOrDefaultAsync(p => p.ProductId == productId);
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _products
            .Include(p => p.ProductCategory)
            .Include(p => p.Medicine)
            .ThenInclude(m => m.Category)
            .Include(p => p.Medicine)
            .ThenInclude(m => m.MedicineType)
            .ToListAsync();
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var existingProduct = await _products
            .Include(p => p.Medicine)
            .FirstOrDefaultAsync(p => p.ProductId == product.ProductId);

        if (existingProduct == null) return false;

        var existingcategory = await _dbContext.ProductCategories.FindAsync(existingProduct.CategoryProductID);

        if (existingcategory is null) return false;

        // حذف الدواء إذا لم يعد المنتج من نوع دواء
        if (product.ProductType != enProductType.Medicine && existingProduct.Medicine != null)
        {
            _dbContext.Medicines.Remove(existingProduct.Medicine);
            existingProduct.Medicine = null;
        }

        // تحديث أو إضافة الدواء
        if (product.Medicine != null)
        {
            var existingMedicine = await _dbContext.Medicines.FindAsync(product.ProductId);
            if (existingMedicine != null)
            {
                _dbContext.Entry(existingMedicine).CurrentValues.SetValues(product.Medicine);
            }
            else
            {
                await _dbContext.Medicines.AddAsync(product.Medicine);
            }
        }

        _dbContext.Entry(existingProduct).CurrentValues.SetValues(product);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<Product>> GetExistingProductsByIdsAsync(List<int> productIds)
    {
        return await _products
            .Include(p => p.Medicine)
            .Where(p => productIds.Contains(p.ProductId))
            .ToListAsync();
    }

    public async Task UpdateRange(List<Product> products)
    {
        _products.UpdateRange(products);
        //await _dbContext.SaveChangesAsync();
    }

}
