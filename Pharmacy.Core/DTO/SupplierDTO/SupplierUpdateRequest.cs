using Pharmacy.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.DTO
{
   public class SupplierUpdateRequest
    {
        [Required]
        public int SupplierId { set; get; }
        
        public string? Name { set; get; }

        public string? Phone { set; get; }

        [EmailAddress]
        public string? Email { set; get; }

        public Supplier ToSupplier()
        {
            return new Supplier()
            {
                Email = this.Email,
                Name = this.Name,
                Phone = this.Phone,
                SupplierID=this.SupplierId

            };
        }
    }
}
