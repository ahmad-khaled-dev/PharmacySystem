

using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Domain.IRepositoriesContracts;
using Pharmacy.Infrastructure.DbContext;

namespace Pharmacy.Infrastructure.Repositories
{


    public class InventoryItemRepository : IInventoryItemRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public InventoryItemRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(InventoryItem item)
        {
            await _applicationDbContext.InventoryItems.AddAsync(item);
        }

        public async Task<List<InventoryItem>> GetBySaleItemIdAsync(int saleItemId)
        {
            return await _applicationDbContext.InventoryItems
                .Where(ii => ii.SaleItemId == saleItemId)
                .ToListAsync();
        }


        public void RemoveRange(List<InventoryItem> items)
        {
            _applicationDbContext.InventoryItems.RemoveRange(items);
        }

        // يمكنك أيضًا توفير طريقة للحذف الفردي لو أحببت
        public void Remove(InventoryItem item)
        {
            _applicationDbContext.InventoryItems.Remove(item);
        }
    }
}
