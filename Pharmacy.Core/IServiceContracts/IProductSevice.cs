using Pharmacy.Core.DTO.ProductDTO;



namespace Pharmacy.Core.IServiceContracts
{
    public interface IProductSevice
    {
        Task<IEnumerable<ProductResponseForPharmacist?>> GetAllProductAsync();
        
        Task<ProductResponseForPharmacist?> AddProductAsync(ProductAddRequest productAddRequest);
         
        Task<ProductResponseForPharmacist?> FindProductByIdAsync(int productId);

        Task<bool> DeleteProductByIdAsync(int productId);

        Task<bool> UpdateProductByIdAsync(ProductUpdateRequest productUpdateRequest);
    }
}
