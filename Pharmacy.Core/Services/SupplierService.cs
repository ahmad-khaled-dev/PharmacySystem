using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Domain.IRepositoriesContracts;
using Pharmacy.Core.IServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepositroy _supplierRepositroy;

        public SupplierService(ISupplierRepositroy supplierRepositroy)
        {
            _supplierRepositroy = supplierRepositroy;
        }

        public async Task<Supplier> Add(Supplier supplier)
        {
           return await _supplierRepositroy.Add(supplier);
        }

        public async Task<bool> Delete(int id)
        {
            return await _supplierRepositroy.Delete(id);
        }

        public async Task<IEnumerable<Supplier>> GetAll()
        {
            return await _supplierRepositroy.GetAll();
        }

        public async Task<Supplier> GetById(int id)
        {
            return await _supplierRepositroy.GetById(id);
        }
        public async Task<bool> Update(Supplier supplier)
        {
            var existingSupplier = await _supplierRepositroy.GetById(supplier.SupplierID);
            if (existingSupplier is null) return false;

 
            if (!string.IsNullOrWhiteSpace(supplier.Name))
                existingSupplier.Name = supplier.Name;

            if (!string.IsNullOrWhiteSpace(supplier.Email))
                existingSupplier.Email = supplier.Email;

            if (!string.IsNullOrWhiteSpace(supplier.Phone))
                existingSupplier.Phone = supplier.Phone;

            return await _supplierRepositroy.Update(existingSupplier);
        }

    }
}
