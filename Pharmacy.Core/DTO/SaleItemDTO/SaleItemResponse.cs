using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO.SaleItemDTO;

public class SaleItemResponse
{
    public int Id { get; set; }

    public int ProductID { get; set; }

    public int Quantity { get; set; }

    public decimal? Price { get; set; }
     
}


public static class SaleItemExtension
{
    public static SaleItemResponse ToSaleItemResponse(this SaleItem saleItem)
    {
        return new SaleItemResponse()
        {   
            Id=saleItem.Id,
            Quantity=saleItem.Quantity,
            Price=saleItem.Price,
            ProductID=saleItem.ProductId,
             
            
         };
    }
}