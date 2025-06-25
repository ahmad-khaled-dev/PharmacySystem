using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Domain.IRepositoriesContracts;
using Pharmacy.Core.IServiceContracts.IValidatorContract;


namespace Pharmacy.Core.Services.ValidatorServices
{
    public class PurchaseValidator : IPurchaseValidator
    {
        private readonly IProductRepository _productRepository;

        public PurchaseValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> ValidatePurchaseAsync(Purchase purchase)
        {
            var productIds = purchase.PurchaseItems.Select(pi => pi.ProductID).ToList();
            var products = await _productRepository.GetExistingProductsByIdsAsync(productIds);

            if (products.Any(p => p == null))
                return false;
             
            return true;
        }
    }
}
