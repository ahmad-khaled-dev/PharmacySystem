using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.IServiceContracts
{
    public interface IValidatorSale
    {
        Task<bool> ValidateSaleAsync(List<int> ProductsId);
    }
}
