

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Domain.IRepositoriesContracts;
using Pharmacy.Infrastructure.DbContext;

namespace Pharmacy.Infrastructure.Repositories
{
    public class BatcheRepository : IBatchRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BatcheRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Batch>> GetBatchesByProductIdAsync(int productId)
        {
            return await _applicationDbContext.Batches
               .Where(b => b.ProductId == productId )
               .OrderBy(b => b.ExpirationDate)
               .ToListAsync();
                
        }
    }
}
