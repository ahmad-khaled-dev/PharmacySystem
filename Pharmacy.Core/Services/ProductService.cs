using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Domain.IRepositoriesContracts;
using Pharmacy.Core.DTO.ProductDTO;
using Pharmacy.Core.IServiceContracts;

public class ProductService : IProductSevice
{
    private readonly IProductRepository _productRepository;
    private readonly IFileStorageService _fileStorageService;
   
    public ProductService(IProductRepository productRepository, IFileStorageService fileStorageService)
    {
        _productRepository = productRepository;
        _fileStorageService = fileStorageService;
        
    }

    public async Task<IEnumerable<ProductResponseForPharmacist?>> GetAllProductAsync()
    {
        var products = await _productRepository.GetAllProducts();
        return products.Select(p => p.ToProductResponsePharmacist());
    }

    public async Task<ProductResponseForPharmacist?> AddProductAsync(ProductAddRequest productAddRequest)
    {
        if (productAddRequest == null)
            return null;

        string imagePath = string.Empty;


         
         
        if (productAddRequest.Image?.Length > 0)
        {
            imagePath = await _fileStorageService.SaveFileAsync(productAddRequest.Image, "Product");
        }

        var product = productAddRequest.ToProduct(imagePath);
        
        product= await _productRepository.AddProduct(product);
         
        if(product is null)
        {
            return null;
        }


        product.Image = _fileStorageService.GetFullImageUrl(product.Image);

        return product.ToProductResponsePharmacist();
    }

    public async Task<bool> DeleteProductByIdAsync(int productId)
    {
        var existingProduct = await _productRepository.FindProductById(productId);
        if (existingProduct == null)
            return false;

        var isDeleted = await _productRepository.DeleteProduct(productId);

        if (isDeleted && !string.IsNullOrWhiteSpace(existingProduct.Image))
        {
            _fileStorageService.DeleteFile(existingProduct.Image);
        }

        return isDeleted;
    }

    public async Task<ProductResponseForPharmacist?> FindProductByIdAsync(int productId)
    {
        var product = await _productRepository.FindProductById(productId);

        if (product is null)
            return null;

        return product.ToProductResponsePharmacist();
    }

    public async Task<bool> UpdateProductByIdAsync(ProductUpdateRequest request)
    {
        var existingProduct = await _productRepository.FindProductById(request.ProductId);
        if (existingProduct == null)
            return false;

        if (!string.IsNullOrWhiteSpace(request.Name))
            existingProduct.Name = request.Name;

        if (!string.IsNullOrWhiteSpace(request.description))
            existingProduct.Description = request.description;

        if (request.categoryId.HasValue)
            existingProduct.CategoryProductID = request.categoryId;

        if (request.MinimumStockLevel.HasValue)
            existingProduct.MinimumStockLevel = request.MinimumStockLevel;

        if (request.SellPrice.HasValue)
            existingProduct.SellPrice = request.SellPrice;

        if (request.Image != null)
        {
            if (!string.IsNullOrWhiteSpace(existingProduct.Image))
            {
                _fileStorageService.DeleteFile(existingProduct.Image);
            }

            existingProduct.Image = await _fileStorageService.SaveFileAsync(request.Image, "Product");
        }

         
        if (request.MedicineUpdateRequest != null)
        {
            if (existingProduct.Medicine == null)
            {
                existingProduct.Medicine = new Medicine { ProductID = existingProduct.ProductId };
            }

            request.MedicineUpdateRequest.UpdateMedicine(existingProduct.Medicine);
        }

        return await _productRepository.UpdateProduct(existingProduct);
    }
}
