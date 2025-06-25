using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Domain.IRepositoriesContracts;
using Pharmacy.Infrastructure.DbContext;

namespace Pharmacy.Infrastructure.Repositories
{
    public class PurchasesRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<Purchase> _purchases;

        public PurchasesRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _purchases = _applicationDbContext.Purchases;
        }

        public async Task<Purchase> AddPurchaseAsync(Purchase purchase)
        {
            if (purchase == null || purchase.PurchaseItems == null || !purchase.PurchaseItems.Any())
                return null;

            var supplier = await _applicationDbContext.Suppliers.FindAsync(purchase.SupplierID);
            if (supplier == null)
                return null;
              
            await _applicationDbContext.Purchases.AddAsync(purchase);
            //await _applicationDbContext.SaveChangesAsync();

            return purchase;
        }



        public async Task<bool> DeletePurchaseAsync(int purchaseId)
        {
            var purchase = await _purchases
                .Include(p => p.PurchaseItems)
                    .ThenInclude(pi => pi.Batches)
                .FirstOrDefaultAsync(p => p.PurchaseID == purchaseId);

            if (purchase == null)
                return false;

            DeletePurchaseItemsAndBatches(purchase);

            _purchases.Remove(purchase);
            await _applicationDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesAsync()
        {

            return await _purchases.ToListAsync();
        }
         
        public async Task<Purchase?> GetPurchaseByIdAsync(int purchaseId)
        {

            return await _purchases.Include(p => p.PurchaseItems)
                .ThenInclude(pi => pi.Batches)
                .FirstOrDefaultAsync(p => p.PurchaseID == purchaseId);
        }
         
        public async Task<bool> UpdatePurchaseAsync(Purchase updatedPurchase)
        {
            var supplier = await _applicationDbContext.Suppliers.FindAsync(updatedPurchase.SupplierID);
            if (supplier == null)
                return false;

            var existingPurchase = await _purchases
                .Include(p => p.PurchaseItems)
                    .ThenInclude(pi => pi.Batches)
                .FirstOrDefaultAsync(p => p.PurchaseID == updatedPurchase.PurchaseID);

            if (existingPurchase == null)
                return false;

            
            UpdatePurchaseBasicFields(existingPurchase, updatedPurchase);

            // تحديث عناصر الشراء
            UpdatePurchaseItems(existingPurchase, updatedPurchase);
             
            return true;
        }
         
        private void UpdatePurchaseBasicFields(Purchase existing, Purchase updated)
        {
            existing.PurchaseDate = updated.PurchaseDate ?? existing.PurchaseDate;
            existing.TotalAmount = updated.TotalAmount ?? existing.TotalAmount;
            existing.discount = updated.discount ?? existing.discount;
            existing.SupplierID = updated.SupplierID;
        }

        private void UpdatePurchaseItems(Purchase existingPurchase, Purchase updatedPurchase)
        {
            var existingItems = existingPurchase.PurchaseItems.ToList();
            var updatedItems = updatedPurchase.PurchaseItems.ToList();

           
            foreach (var existingItem in existingItems)
            {
                if (!updatedItems.Any(i => i.PurchaseItemID == existingItem.PurchaseItemID))
                {
                    _applicationDbContext.Batches.RemoveRange(existingItem.Batches);
                    _applicationDbContext.PurchaseItems.Remove(existingItem);
                }
            }

            foreach (var updatedItem in updatedItems)
            {
                var existingItem = existingItems.FirstOrDefault(i => i.PurchaseItemID == updatedItem.PurchaseItemID);

                if (existingItem != null)
                {
                    UpdatePurchaseItemFields(existingItem, updatedItem);
                    UpdateBatches(existingItem, updatedItem.Batches.ToList());
                }
                else
                {
                    existingPurchase.PurchaseItems.Add(updatedItem); // مضاف حديثًا
                }
            }
        }

        private void UpdatePurchaseItemFields(PurchaseItem existingItem, PurchaseItem updatedItem)
        { 
            existingItem.ProductID = updatedItem.ProductID;
            existingItem.Quantity = updatedItem.Quantity  ;
            existingItem.PurchasePrice = updatedItem.PurchasePrice;
        }
        
        private void UpdateBatches(PurchaseItem existingItem, List<Batch> updatedBatches)
        {
            var existingBatches = existingItem.Batches.ToList();

            // حذف الدُفعات التي لم تعد موجودة
            foreach (var existingBatch in existingBatches)
            {
                if (!updatedBatches.Any(b => b.BatchID == existingBatch.BatchID))
                {
                    _applicationDbContext.Batches.Remove(existingBatch);
                }
            }

            // تحديث أو إضافة دُفعات
            foreach (var batch in updatedBatches)
            {
                var existingBatch = existingBatches.FirstOrDefault(b => b.BatchID == batch.BatchID);

                if (existingBatch != null)
                {
                    existingBatch.Barcode = batch.Barcode;
                    existingBatch.BatchNumber = batch.BatchNumber;
                    existingBatch.ExpirationDate = batch.ExpirationDate;
                    existingBatch.Quantity = batch.Quantity;
                }
                else
                {
                    existingItem.Batches.Add(batch); // Batch جديد
                }
            }
        }

        private void DeletePurchaseItemsAndBatches(Purchase purchase)
        {
            foreach (var item in purchase.PurchaseItems)
            {
                _applicationDbContext.Batches.RemoveRange(item.Batches);
            }

            _applicationDbContext.PurchaseItems.RemoveRange(purchase.PurchaseItems);
        }

    }

}
