
using Pharmacy.Core.Domain.Entities;

namespace Pharmacy.Core.IServiceContracts.IValidatorContract
{
    public interface IPurchaseValidator
    {
        Task<bool> ValidatePurchaseAsync(Purchase purchase);
    }
}
