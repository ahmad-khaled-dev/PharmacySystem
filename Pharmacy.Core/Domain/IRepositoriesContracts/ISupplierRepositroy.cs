using Pharmacy.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.Domain.IRepositoriesContracts
{
   public interface ISupplierRepositroy
    {

        Task<IEnumerable<Supplier>> GetAll();

        Task<Supplier> Add(Supplier supplier);

        Task<bool> Delete(int id);

        Task<bool> Update(Supplier supplier);

        Task<Supplier> GetById(int id);
    }
}
