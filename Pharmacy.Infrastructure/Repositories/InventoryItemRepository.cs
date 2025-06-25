

using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Domain.IRepositoriesContracts;
using Pharmacy.Infrastructure.DbContext;

namespace Pharmacy.Infrastructure.Repositories
{


    public class InventoryItemRepository : IInventoryItemRepository
    {
        private  readonly ApplicationDbContext _applicationDbContext;

        public InventoryItemRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(InventoryItem item)
        {
            await _applicationDbContext.InventoryItems.AddAsync(item);
        }
    }
}
