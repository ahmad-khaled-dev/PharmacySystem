using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Infrastructure.DbContext;

public class SaleRepository : ISaleRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Sale> _sales;

    public SaleRepository(ApplicationDbContext context)
    {
        _context = context;
        _sales = _context.Sales;
    }

    public async Task<IEnumerable<Sale>> GetAllSalesAsync()
    {


        return await _sales
            .Include(s => s.Employee)
            .Include(s => s.SaleItems)
            .ToListAsync();
    }

    public async Task<Sale?> GetSaleByIdAsync(int id)
    {


        return await _sales
            .Include(s => s.Employee)
            .Include(s => s.SaleItems)
            .ThenInclude(si => si.InventoryItems)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Sale> AddSaleAsync(Sale sale)
    { 

        await _sales.AddAsync(sale);
       
        return sale;
    }

    public async Task<bool> UpdateSaleAsync(Sale updatedSale)
    {
        var existingSale = await _sales
            .Include(s => s.SaleItems)
            .FirstOrDefaultAsync(s => s.Id == updatedSale.Id);

        if (existingSale == null)
            return false;

        var incomingItemIds = updatedSale.SaleItems.Select(item => item.Id).ToList();

        // حذف العناصر التي لم تعد موجودة
        var itemsToRemove = existingSale.SaleItems
            .Where(item => !incomingItemIds.Contains(item.Id))
            .ToList();

        _context.SaleItems.RemoveRange(itemsToRemove);

        // تحديث أو إضافة العناصر
        foreach (var incomingItem in updatedSale.SaleItems)
        {
            var dbItem = existingSale.SaleItems.FirstOrDefault(item => item.Id == incomingItem.Id);

            if (dbItem != null)
            {
                dbItem.ProductId = incomingItem.ProductId;
                dbItem.Quantity = incomingItem.Quantity;
                dbItem.Price = incomingItem.Price;
                dbItem.ImagPrescription = incomingItem.ImagPrescription;
            }
            else
            {
                existingSale.SaleItems.Add(new SaleItem
                {
                    ProductId = incomingItem.ProductId,
                    Quantity = incomingItem.Quantity,
                    Price = incomingItem.Price,
                    ImagPrescription = incomingItem.ImagPrescription
                });
            }
        }

        existingSale.TotalAmount = updatedSale.TotalAmount;
        existingSale.SaleDate = updatedSale.SaleDate;
        existingSale.EmployeeId = updatedSale.EmployeeId;

       
        return true;
    }


    public async Task<bool> DeleteSaleAsync(int id)
    {
        var existing = await _context.Sales.FindAsync(id);
        if (existing == null)
            return false;

        _context.Sales.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<SaleItem> GetSaleItemByIdAsync(int saleItemId)
    {
        return  await _context.SaleItems.Include(si => si.InventoryItems)
            .FirstOrDefaultAsync(si => si.Id == saleItemId);
    } 

     


}
