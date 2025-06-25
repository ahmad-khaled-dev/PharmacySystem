using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

public class SaleItemUpdateRequest
{
    [Required]
    public int Id { get; set; }  // ID of existing SaleItem


    public int SaleId { get; set; }

    public int Quantity { get; set; }
      
    public int ProductID { get; set; }
     
    [Precision(18, 2)]
    public decimal? Price { get; set; }

    public IFormFile? Image { set; get; }

    public string? ImagePath { set; get; }


    public SaleItem ToSaleItem()
    {
        return new SaleItem
        {
            ProductId = this.ProductID,
            Quantity = this.Quantity,
            Id = this.Id,
            SaleId = this.SaleId,
            Price=this.Price ,
            ImagPrescription = this.ImagePath
        };
    }
}


