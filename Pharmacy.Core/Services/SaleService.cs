using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Domain.Entities.IdentityEntities;
using Pharmacy.Core.Domain.IRepositoriesContracts;
using Pharmacy.Core.DTO.SaleDTO;
using Pharmacy.Core.DTO.SaleItemDTO;
using Pharmacy.Core.IServiceContracts;

namespace Pharmacy.Core.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IEmployeesRepository _employeeRepository;
    private readonly IBatchRepository _batchRepository;
    private readonly IUnitOfWorkService _unitOfWorkService;
    private readonly IProductRepository _productRepository;
    private readonly IInventoryItemRepository _inventoryItemRepository;
    private readonly IFileStorageService _fileStorageService;

    public SaleService(ISaleRepository saleRepository, ICurrentUserService currentUserService, IEmployeesRepository employeeRepository, IBatchRepository batchRepository, IUnitOfWorkService unitOfWorkService, IProductRepository productRepository, IInventoryItemRepository inventoryItemRepository, IFileStorageService fileStorageService)
    {
        _saleRepository = saleRepository;
        _currentUserService = currentUserService;
        _employeeRepository = employeeRepository;
        _batchRepository = batchRepository;
        _unitOfWorkService = unitOfWorkService;
        _productRepository = productRepository;
        _inventoryItemRepository = inventoryItemRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<IEnumerable<SaleResponse>> GetAllSalesAsync()
    {
        var sales = await _saleRepository.GetAllSalesAsync();
        return sales.Select(s => s.ToSaleResponse());
    }

    public async Task<SaleResponse?> GetSaleByIdAsync(int id)
    {
        var sale = await _saleRepository.GetSaleByIdAsync(id);
        return sale?.ToSaleResponse();
    }

    public async Task<SaleResponse?> AddSaleAsync(SaleAddRequest saleRequest)
    {
        var employee = await GetCurrentEmployeeAsync();
        saleRequest.EmployeeId = employee.EmployeeID;

        if (saleRequest.SaleItems == null || !saleRequest.SaleItems.Any())
            return null;

        var productIds = saleRequest.SaleItems.Select(si => si.ProductId).ToList();
        var products = await ValidateAndFetchProductsAsync(productIds);

        await ValidateSaleItemsAsync(saleRequest.SaleItems, products);

        var saleEntity = saleRequest.ToSale();
        saleEntity.TotalAmount = CalculateTotalAmount(saleEntity.SaleItems.ToList());

        
        var strategy =  _unitOfWorkService.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await _unitOfWorkService.BeginTransactionAsync();

            try
            {
                var savedSale = await _saleRepository.AddSaleAsync(saleEntity);

                await _unitOfWorkService.SaveChangesAsync();

                savedSale = await _saleRepository.GetSaleByIdAsync(savedSale.Id);

                foreach (var saleItem in savedSale.SaleItems)
                {
                    await DeductFromBatchesAsync(saleItem.ProductId, saleItem.Quantity, saleItem.Id);
                }

                await _unitOfWorkService.SaveChangesAsync();
                await _unitOfWorkService.CommitTransactionAsync();

                return savedSale.ToSaleResponse();
            }
            catch
            {
                await _unitOfWorkService.RollbackTransactionAsync();
                throw;
            }
        });
    }

    public async Task<bool> DeleteSaleAsync(int id)
    {
        return await _saleRepository.DeleteSaleAsync(id);
    }

    public async Task<bool> UpdateSaleAsync(SaleUpdateRequest saleUpdate)
    {
        var employee = await _employeeRepository.GetByUserIdAsync(_currentUserService.GetUserId());

        if (employee == null)
            throw new Exception("لم يتم العثور على موظف مرتبط بالمستخدم الحالي.");

        saleUpdate.EmployeeId = employee.EmployeeID;

        if (saleUpdate.SaleItems == null || !saleUpdate.SaleItems.Any())
            return false;

        var productIds = saleUpdate.SaleItems.Select(si => si.ProductID).ToList();

        var products = await _productRepository.GetExistingProductsByIdsAsync(productIds);

        if (products.Count != productIds.Count)
            return false;

        var productMap = products.ToDictionary(p => p.ProductId);

        var existingSale = await _saleRepository.GetSaleByIdAsync(saleUpdate.Id);

        if (existingSale == null)
            return false;
         

        foreach (var item in saleUpdate.SaleItems)
        {
            if (!productMap.TryGetValue(item.ProductID, out var product))
                return false;

            item.Price = product.SellPrice;

            // معالجة صورة الوصفة إن لزم
            var existItem = existingSale.SaleItems.FirstOrDefault(si => si.Id == item.Id);
            if (product.Medicine != null && product.Medicine.IsRequiredDescription)
            {
                item.ImagePath = await HandlePrescriptionImage(item, existItem?.ImagPrescription);
            }
        }


        var strategy = _unitOfWorkService.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await _unitOfWorkService.BeginTransactionAsync();
            try
            {
               
                foreach (var oldItem in existingSale.SaleItems)
                {
                    await ReturnQuantityToBatchAsync(oldItem);
                    await DeleteInventoryItemsForSaleItemAsync(oldItem.Id);
                     
                }

                
                existingSale.SaleItems = saleUpdate.SaleItems.Select(si => si.ToSaleItem()).ToList();
                existingSale.TotalAmount = CalculateTotalAmount(existingSale.SaleItems.ToList());
                existingSale.EmployeeId = saleUpdate.EmployeeId;

                var updated = await _saleRepository.UpdateSaleAsync(existingSale);
                if (!updated)
                    throw new Exception("فشل في تحديث البيع.");

                // خصم الكميات من المخزون
                foreach (var saleItem in existingSale.SaleItems)
                {
                    await DeductFromBatchesAsync(saleItem.ProductId, saleItem.Quantity, saleItem.Id);
                    await _unitOfWorkService.SaveChangesAsync();
                }

                await _unitOfWorkService.SaveChangesAsync();
                await _unitOfWorkService.CommitTransactionAsync();

                return true;
            }
            catch
            {
                await _unitOfWorkService.RollbackTransactionAsync();
                throw;
            }
        });
    }

    private async Task<string?> HandlePrescriptionImage(SaleItemUpdateRequest item, string? existingImagePath)
    {
        if (item.Image == null || item.Image.Length == 0)
            throw new ArgumentException("يجب إضافة صورة للوصفة الطبية لهذا الدواء.");

        if (!string.IsNullOrWhiteSpace(existingImagePath))
            _fileStorageService.DeleteFile(existingImagePath);

        return await _fileStorageService.SaveFileAsync(item.Image, "Medicine");
    }

    private async Task ReturnQuantityToBatchAsync(SaleItem saleItem)
    {
        var batches = await _batchRepository.GetBatchesByProductIdAsync(saleItem.ProductId);

        foreach (var invItem in saleItem.InventoryItems)
        {
            var batch = batches.FirstOrDefault(b => b.BatchID == invItem.BatchId);
            if (batch != null)
            {
                batch.Quantity += invItem.QuantitySoldFromBatch;
                invItem.QuantitySoldFromBatch = 0;
            }
        }
    }

    public async Task DeductFromBatchesAsync(int productId, int quantityToSell, int saleItemId)
    {
        var batches = await _batchRepository.GetBatchesByProductIdAsync(productId);

        foreach (var batch in batches)
        {
            if (quantityToSell == 0)
                break;

            int availableQty = batch.Quantity;
            if (availableQty == 0)
                continue;

            int deductQty = Math.Min(quantityToSell, availableQty);

            batch.Quantity -= deductQty;
            quantityToSell -= deductQty;

            var inventoryItem = new InventoryItem
            {
                BatchId = batch.BatchID,
                SaleItemId = saleItemId,
                QuantitySoldFromBatch = deductQty
            };

            await _inventoryItemRepository.AddAsync(inventoryItem);
        }

        if (quantityToSell > 0)
        {
            throw new Exception("الكمية المطلوبة غير متوفرة في المخزون.");
        }
    }
      
    private async Task<Employee> GetCurrentEmployeeAsync()
    {

        var employee = await _employeeRepository.GetByUserIdAsync(_currentUserService.GetUserId());
        if (employee == null)
            throw new Exception("لم يتم العثور على موظف مرتبط بالمستخدم الحالي.");
        return employee;
    }

    private async Task<List<Product>> ValidateAndFetchProductsAsync(List<int> ids)
    {
        var products = await _productRepository.GetExistingProductsByIdsAsync(ids);
        if (products.Count != ids.Count)
            throw new ArgumentException("بعض المنتجات غير موجودة.");
        return products;
    }

    private async Task ValidateSaleItemsAsync(List<SaleItemAddRequest> items, List<Product> products)
    {
        foreach (var item in items)
        {
            var product = products.First(p => p.ProductId == item.ProductId);
            item.Price = product.SellPrice;

            if (product.Medicine?.IsRequiredDescription == true)
            {
                if (item.Image == null || item.Image.Length == 0)
                    throw new ArgumentException($"يجب إضافة صورة للوصفة الطبية للدواء: {product.Name}");

                item.ImagePath = await SavePrescriptionImageAsync(item.Image);
            }
        }
    }

    private async Task<string> SavePrescriptionImageAsync(IFormFile image)
    {
        return await _fileStorageService.SaveFileAsync(image, "Medicine");
    }

    private decimal CalculateTotalAmount(List<SaleItem> saleItems)
    {
        return saleItems.Sum(i => i.Price * i.Quantity ?? 0);
    }

    private async Task DeleteInventoryItemsForSaleItemAsync(int saleItemId)
    {
        var inventoryItems = await _inventoryItemRepository.GetBySaleItemIdAsync(saleItemId);

        if (inventoryItems.Any())
        {
            _inventoryItemRepository.RemoveRange(inventoryItems);
        }
    }



}
