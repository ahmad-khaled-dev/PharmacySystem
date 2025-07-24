using Pharmacy.Core.Domain.Entities;
 

namespace Pharmacy.Core.Domain.IRepositoriesContracts
{
    public interface IInventoryItemRepository
    {
        Task AddAsync(InventoryItem item);

        Task<List<InventoryItem>> GetBySaleItemIdAsync(int saleItemId);

         void RemoveRange(List<InventoryItem> items);

         void Remove(InventoryItem item);
    }

}
