using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO.MedicineDTO;
using Pharmacy.Core.DTO.ProductDTO;

public class MedicineTypeDetailResponse
{
    public int Id { get; set; }
   
    public string Name { get; set; }

    public List<MedicineResponse>? Medicine{ set; get; } 
}

public static class MedicineTypeDetailExtension
{
    public static MedicineTypeDetailResponse ToMedicineTypeDetailResponse(this MedicineType medicineType)
    {
        return new MedicineTypeDetailResponse()
        {
            Id = medicineType.Id,
            Name = medicineType.Name  ,
            Medicine=medicineType.Medicines.Select(m => m.ToMedicineResponse()).ToList()

        };
    }
}