using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Domain.Entities;

namespace Pharmacy.Core.Domain.IRepositoriesContracts
{
   public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();

        Task<Product> AddProduct(Product product);

        Task<Product?> FindProductById(int ProductId);

        Task<bool> DeleteProduct(int ProductId);
        
        Task<bool> UpdateProduct(Product product);

        Task<List<Product>> GetExistingProductsByIdsAsync(List<int> productIds);

        public Task UpdateRange(List<Product> products);

    }
}
