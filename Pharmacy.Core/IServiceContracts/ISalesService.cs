using Pharmacy.Core.DTO.SaleDTO;

public interface ISaleService
{
    Task<IEnumerable<SaleResponse>> GetAllSalesAsync();

    Task<SaleResponse?> GetSaleByIdAsync(int id);

    Task<SaleResponse> AddSaleAsync(SaleAddRequest request);

    Task<bool> UpdateSaleAsync(SaleUpdateRequest request);

    Task<bool> DeleteSaleAsync(int id);
}
