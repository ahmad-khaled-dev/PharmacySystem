using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Domain.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.DTO.SaleDTO
{
   public class SaleUpdateRequest
    {
        public int Id { get; set; }

        public DateTime SaleDate { get; set; } = DateTime.Now;

        public int EmployeeId { get; set; }
         
        [Precision(18, 2)]
        public decimal? TotalAmount { set; get; }
         
        [Required]
        public List<SaleItemUpdateRequest> SaleItems { get; set; } = new();

        public Sale ToSale()
        {

            return new Sale()
            {
                Id = this.Id,
                EmployeeId = this.EmployeeId,
                SaleDate = this.SaleDate,
                SaleItems = this.SaleItems.Select(i => i.ToSaleItem()).ToList()
            };
        }
    }

}
