using Pharmacy.Core.Domain.Entities;

namespace Pharmacy.Core.DTO
{
    public class SupplierResponse
    {

        public int SupplierId { set; get; }

        public string? Name { set; get; }

        public string? Phone { set; get; }

        public string? Email { set; get; }

    
}

    public static class SupplierExtension
        {

      public static SupplierResponse ToSupplierResponse(this Supplier supplier)
        {

            return new SupplierResponse()
            {
                Email = supplier.Email,
                Name = supplier.Name,
                Phone = supplier.Phone,
                SupplierId = supplier.SupplierID

            };
        }

    }

}

