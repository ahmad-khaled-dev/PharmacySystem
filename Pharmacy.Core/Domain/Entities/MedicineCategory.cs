using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Core.Domain.Entities
{
    public class MedicineCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MedicineCategoryID { set; get; }

        public string ? Name { set; get; }

        public string? Image { set; get; }

        public ICollection<Medicine>? Medicines { set; get; } = new List<Medicine>() ;
    }
}