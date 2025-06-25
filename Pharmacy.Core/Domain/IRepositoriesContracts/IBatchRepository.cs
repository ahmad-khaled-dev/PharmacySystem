


using Pharmacy.Core.Domain.Entities;

namespace Pharmacy.Core.Domain.IRepositoriesContracts
{
   public interface IBatchRepository
    {
        Task<List<Batch>> GetBatchesByProductIdAsync(int productId);
    }
}
