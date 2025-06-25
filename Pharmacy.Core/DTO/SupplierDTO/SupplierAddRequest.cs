

using Pharmacy.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.DTO
{
   public class SupplierAddRequest
    {
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
                Phone = this.Phone

            };
        }
    }
}
