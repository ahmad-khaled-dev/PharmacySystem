

using Pharmacy.Core.Domain.Entities;

namespace Pharmacy.Core.IServiceContracts
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetAll();

        Task<Supplier> Add(Supplier supplier);

        Task<bool> Delete(int id);

        Task<bool> Update(Supplier supplier);

        Task<Supplier> GetById(int id);
    }
}
