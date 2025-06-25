using Pharmacy.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.Domain.IRepositoriesContracts
{
    public interface IPurchaseRepository
    {
         
        Task<IEnumerable<Purchase>> GetAllPurchasesAsync();
          
        Task<Purchase?> GetPurchaseByIdAsync(int purchaseId);
         
        Task<Purchase> AddPurchaseAsync(Purchase purchase);
         
        Task<bool> UpdatePurchaseAsync(Purchase purchase);
         
        Task<bool> DeletePurchaseAsync(int purchaseId);

        
    }
}
