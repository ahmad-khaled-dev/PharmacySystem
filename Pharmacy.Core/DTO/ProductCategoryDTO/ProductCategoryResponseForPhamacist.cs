using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO.ProductCategoryDTO;
using Pharmacy.Core.DTO.ProductDTO;


namespace Pharmacy.Core.DTO.ProductCategoryDTO
{

    public class ProductCategoryResponseForPhamacist
    {
        public int CateogryID { set; get; }

        public string Name { set; get; }

        public List<ProductResponseForPharmacist>? Products { set; get; }
    }
}

public static class ProductCategoryForPharmacistExtension
{
    public static ProductCategoryResponseForPhamacist ToCategoryResponsePharmacist(this ProductCategory productCategory
        )
    {
        return new ProductCategoryResponseForPhamacist()
        {
            CateogryID = productCategory.CategoryId,
            Name = productCategory.Name,
            Products = productCategory.Products?.Select(p => p.ToProductResponsePharmacist()).ToList()
           ?? new List<ProductResponseForPharmacist>()

        };
    }
}
