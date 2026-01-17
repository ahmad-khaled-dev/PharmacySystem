using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Domain.IRepositoriesContracts;
using Pharmacy.Core.IServiceContracts;
using Pharmacy.Core.IServiceContracts.IValidatorContract;


namespace Pharmacy.Core.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IProductRepository _productRepository;

        private readonly IUnitOfWorkService _unitOfWorkService;

        private readonly IPurchaseValidator _purchaseValidator;

        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IProductRepository productRepository, IUnitOfWorkService unitOfWorkService, IPurchaseValidator purchaseValidator, IPurchaseRepository purchaseRepository)
        {
            _productRepository = productRepository;
            _unitOfWorkService = unitOfWorkService;
            _purchaseValidator = purchaseValidator;
            _purchaseRepository = purchaseRepository;
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesAsync()
        {
            var purchases = await _purchaseRepository.GetAllPurchasesAsync();

            return purchases;
        }

        public async Task<Purchase> AddPurchaseAsync(Purchase purchase)
        {
            if (purchase == null)
                throw new ArgumentNullException(nameof(purchase));

            var isValid = await _purchaseValidator.ValidatePurchaseAsync(purchase);
            if (!isValid)
                throw new Exception("Validation failed: Some products do not exist or invalid purchase.");

            purchase.TotalAmount = CalculateTotalAmount(purchase.PurchaseItems.ToList(), purchase.discount);

            // هذه هي النقطة المهمة
            var strategy = _unitOfWorkService.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                await _unitOfWorkService.BeginTransactionAsync(); // هذا مسموح الآن لأنه داخل ExecuteAsync

                try
                {
                    var result = await _purchaseRepository.AddPurchaseAsync(purchase);
                   
                    await _unitOfWorkService.SaveChangesAsync();
                    await _unitOfWorkService.CommitTransactionAsync();
                    return result;
                }
                catch
                {
                    await _unitOfWorkService.RollbackTransactionAsync();
                    throw;
                }
            });
        }

        public async Task<bool> DeletePurchaseAsync(int id)
        {

            return await _purchaseRepository.DeletePurchaseAsync(id);
        }
         
        public async Task<Purchase?> GetPurchaseByIdAsync(int id)
        {

            var result = await _purchaseRepository.GetPurchaseByIdAsync(id);

            if(result is null)
            {
                return null;
            }

         return result;
        }

        public async Task<bool> UpdatePurchaseAsync(Purchase purchase)
        {

            if (purchase == null)
                throw new ArgumentNullException(nameof(purchase));

            var isValid = await _purchaseValidator.ValidatePurchaseAsync(purchase);
            if (!isValid)
                return false;

             //var productIds = purchase.PurchaseItems.Select(pi => pi.ProductID).ToList();
             //var products = await _productRepository.GetExistingProductsByIdsAsync(productIds);

             
             

            var correctTotal = CalculateTotalAmount(purchase.PurchaseItems.ToList(), purchase.discount);

            if (purchase.TotalAmount != correctTotal)
                purchase.TotalAmount = correctTotal;



            var strategy = _unitOfWorkService.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {

                await _unitOfWorkService.BeginTransactionAsync();

                try
                {
                    var result = await _purchaseRepository.UpdatePurchaseAsync(purchase);

                    await _unitOfWorkService.SaveChangesAsync();

                    await _unitOfWorkService.CommitTransactionAsync();
                    return result;
                }
                catch
                {
                    await _unitOfWorkService.RollbackTransactionAsync();
                    throw;
                }
            });
        }

        private decimal? CalculateTotalAmount(List<PurchaseItem> items, float? discount)
        {

            decimal? total = items.Sum(item => item.Quantity * item.PurchasePrice);

            if (discount.HasValue)
                total -= total * (decimal)(discount.Value / 100);

            return total;
        }



    }
}
