using Pharmacy.Core.Domain.Entities;
 

namespace Pharmacy.Core.Domain.IRepositoriesContracts
{
    public interface IInventoryItemRepository
    {
        Task AddAsync(InventoryItem item);
    }

}
