using Microsoft.VisualBasic;
using Pharmacy.Core.Domain.Entities.IdentityEntities;
using Pharmacy.Core.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Core.Domain.Entities
{
    public class StockMovement
    {
        public int StockMovementID { set; get; }

        [ForeignKey("Product")]
        public int? ProducIDP { set; get; }

        public Product ? Product { set; get; }

        [ForeignKey("Employee")]
        public int? EmployeeID { set; get; }

        public Employee ? Employee { set; get; }

        [Required]
        public MovementType MovementType { set; get; }

        public string? ReferenceNumber { set; get; }

        [Required]
        public int QuantityChanged { set; get; } // +10 -15 Damaged,sale,purchase

        public string ? Note { set; get; }

        public DateTime? MovementDate { set; get; } = DateTime.Now;
    }
}