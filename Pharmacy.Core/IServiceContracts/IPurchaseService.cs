using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO;
using Pharmacy.Core.DTO.PurchaseDTO;


namespace Pharmacy.Core.IServiceContracts
{
    public interface IPurchaseService
    {
        Task<IEnumerable<Purchase>> GetAllPurchasesAsync();

        Task<Purchase?> GetPurchaseByIdAsync(int id);
        
        Task<Purchase> AddPurchaseAsync(Purchase purchase);
        
        Task<bool> UpdatePurchaseAsync(Purchase purchase);
        
        Task<bool> DeletePurchaseAsync(int id);
    }
}
