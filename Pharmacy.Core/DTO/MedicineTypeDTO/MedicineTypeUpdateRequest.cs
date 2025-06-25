// MedicineTypeUpdateRequest.cs
using Pharmacy.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;
// MedicineTypeUpdateRequest.cs
public class MedicineTypeUpdateRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }


    public MedicineType ToMedicineType()
    {
        return new MedicineType()
        {
            Id=this.Id,
            Name = this.Name
        };
    }
}
