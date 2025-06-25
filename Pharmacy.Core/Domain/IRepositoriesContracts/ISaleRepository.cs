using Pharmacy.Core.Domain.Entities;

public interface ISaleRepository
{
    Task<IEnumerable<Sale>> GetAllSalesAsync();

    Task<Sale?> GetSaleByIdAsync(int id);

    Task<Sale> AddSaleAsync(Sale sale);

    Task<bool> UpdateSaleAsync(Sale sale);

    Task<bool> DeleteSaleAsync(int id);

    public   Task<SaleItem> GetSaleItemByIdAsync(int saleItemId);
}
