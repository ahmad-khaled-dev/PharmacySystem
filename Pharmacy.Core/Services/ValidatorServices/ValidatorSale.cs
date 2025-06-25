
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Domain.IRepositoriesContracts;
using Pharmacy.Core.IServiceContracts;

namespace Pharmacy.Core.Services.ValidatorServices
{
    public class ValidatorSale : IValidatorSale
    {
        public IProductRepository _productRepository;

        public ValidatorSale(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<bool> ValidateSaleAsync(List<int> ProductsId)
        {
            throw new NotImplementedException();
        }
    }
}
