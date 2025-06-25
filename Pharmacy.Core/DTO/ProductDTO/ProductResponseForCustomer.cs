using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO.ProductDTO;
using System.ComponentModel.DataAnnotations;




namespace Pharmacy.Core.DTO.ProductDTO
{
    public class ProductResponseForCustomer : ProductResponseForPharmacist
    { 

        public string ? Image { set; get; }
         

    }
}

public static class ProductForCustomerExtenion
{
    public static ProductResponseForCustomer ToProductResponseCustomer(this Product product,string?ImagePath=null)
    {

        return new ProductResponseForCustomer()
        {
           
            ProductId = product.ProductId,
            CategoryId = product.ProductCategory?.CategoryId,
            CategoryName = product.ProductCategory?.Name,
            Description = product.Description,
            MinimumStockLevel = product.MinimumStockLevel,
            Name = product.Name,
            Image=ImagePath,
            SellPrice=product.SellPrice
        };
    }
}
