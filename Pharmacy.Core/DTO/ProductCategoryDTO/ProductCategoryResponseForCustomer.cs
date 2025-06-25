using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO.ProductCategoryDTO;


namespace Pharmacy.Core.DTO.ProductCategoryDTO
{

    public class ProductCategoryResponseForCustomer
    {
        public int CateogryID { set; get; }

        public string Name { set; get; }

        public string? ImageUrl { set; get; }
    }
}

public static class ProductCategoryForCustomerExtension
{
    public static ProductCategoryResponseForCustomer ToCategoryResponseCustomer(this ProductCategory productCategory
        )
    {
        return new ProductCategoryResponseForCustomer()
        {
            CateogryID = productCategory.CategoryId,
            Name = productCategory.Name,
            ImageUrl = productCategory.Image ?? null
        };
    }
}
