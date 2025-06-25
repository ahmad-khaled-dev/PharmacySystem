using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Domain.Entities;


namespace Pharmacy.Core.DTO.SaleDTO
{
    public class SaleResponse
    {
        public int Id { get; set; }

        public DateTime SaleDate { get; set; }
         
        [Precision(18, 2)]
        public decimal? TotalAmount { set; get; }
         
        public string? EmployeeName { set; get; }

        public List<SaleItemResponse> SaleItems { get; set; } = new();


    }

    public static class SaleExtension
    {
        public static SaleResponse ToSaleResponse(this Sale sale)
        {
            return new SaleResponse
            {
                Id = sale.Id,
                SaleDate = sale.SaleDate,
                EmployeeName = sale.Employee?.User?.UserName,
                TotalAmount = sale.TotalAmount ?? 0,
                SaleItems = sale.SaleItems?.Select(i => i.ToSaleItemResponse()).ToList() ?? new List<SaleItemResponse>()
            };
        }


    }
}



