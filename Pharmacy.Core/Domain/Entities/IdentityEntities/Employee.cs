using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Pharmacy.Core.Domain.Entities.IdentityEntities
{
  public  class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { set; get; }
        
        public string ? PersonName { set; get; }

        [Required]
        public DateTime? HireDate { set; get; }

        public float ? Salary { set; get; }

        [Required]
        public string  NumberOfCertificate { set; get; }
         
        public string Role { set; get; }
         
        // الربط مع المستخدم من Identity
        public Guid? UserId { get; set; }

        public ApplicationUser? User { get; set; }

        public ICollection<Sale>? Sales { get; set; } = new List<Sale>();

        public ICollection<StockMovement>? StockMovements { get; set; } = new List<StockMovement>();

    }
}
