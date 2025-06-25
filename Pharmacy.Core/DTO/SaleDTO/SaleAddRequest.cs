using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO.SaleItemDTO;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.DTO.SaleDTO
{
  public class SaleAddRequest
    {

        public int EmployeeId { get; set; }

        public DateTime SaleDate { get; set; } = DateTime.Now;
         
        [Precision(18, 2)]
        public decimal? TotalAmount { set; get; }

        [Required]
        public List<SaleItemAddRequest> SaleItems { get; set; } = new();

        public  Sale ToSale()
        {
            return new Sale
            {
                EmployeeId = this.EmployeeId,
                SaleDate = this.SaleDate,
                SaleItems = this.SaleItems.Select(i => i.ToSaleItem()).ToList()
            };
        }
    }

}
