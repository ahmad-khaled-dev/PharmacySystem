using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO.MedicineDTO;

public class MedicineTypeResponse
{
    public int Id { get; set; }
   
    public string Name { get; set; }

    public List <MedicineResponse> MedicineResponse { set; get; }   = new List<MedicineResponse>() ;
}

public static class MedicineTypeExtension
{
    public static MedicineTypeResponse ToMedicineTypeResponse(this MedicineType medicineType)
    {
        return new MedicineTypeResponse()
        {
            Id = medicineType.Id,
            Name = medicineType.Name,
            MedicineResponse =medicineType?.Medicines.Select(m => m.ToMedicineResponse()).ToList()
        };
    }
}