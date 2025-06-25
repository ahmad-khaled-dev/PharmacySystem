

using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Domain.IRepositoriesContracts;
using Pharmacy.Infrastructure.DbContext;

namespace Pharmacy.Infrastructure.Repositories
{
    public class SupplierRepository : ISupplierRepositroy
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<Supplier> _suppliers;

        public SupplierRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
            _suppliers = context.Set<Supplier>();
        }

        public async Task<Supplier> Add(Supplier supplier)
        {
            await _suppliers.AddAsync(supplier);
            await _applicationDbContext.SaveChangesAsync();
            return supplier;
        }

        public async Task<bool> Delete(int id)
        {
            var supplier = await _suppliers.FindAsync(id);
            if (supplier is null) return false;

            _suppliers.Remove(supplier);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Supplier>> GetAll()
        {
            return await _suppliers.ToListAsync();
        }

        public async Task<Supplier?> GetById(int id)
        {
            return await _suppliers.Include(s => s.Purchases)
                                   .FirstOrDefaultAsync(s => s.SupplierID == id);
        }

        public async Task<bool> Update(Supplier supplier)
        {
            var existingSupplier = await _suppliers.FindAsync(supplier.SupplierID);
            if (existingSupplier is null) return false;

            _applicationDbContext.Entry(existingSupplier).CurrentValues.SetValues(supplier);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }

}
