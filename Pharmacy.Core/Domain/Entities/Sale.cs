using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Domain.Entities.IdentityEntities;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.Domain.Entities
{

    public class Sale
    {
        public int Id { get; set; }

        public DateTime SaleDate { get; set; }

        public int EmployeeId { get; set; }
         
        public  Employee? Employee { get; set; }

        [Precision(18, 2)]
        public decimal ? TotalAmount { set; get; }

        public ICollection<SaleItem>? SaleItems { get; set; } = new List<SaleItem>();
    }


}