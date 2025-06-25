using Pharmacy.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

public class MedicineTypeAddRequest
{
    [Required]
    public string Name { get; set; }
     
    public MedicineType ToMedicineType()
    {
        return new MedicineType()
        {
            Name = this.Name
        };
    }
}
