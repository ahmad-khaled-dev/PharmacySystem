using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO.MedicineCategoryDTO;
using Pharmacy.Core.DTO.MedicineDTO;

namespace Pharmacy.Core.DTO.MedicineCategoryDTO
{
     
        public class MedicineCategoryResponseForPharmacist
        {
            public int CateogryID { set; get; }

            public string Name { set; get; }

        public List<MedicineResponse>? MedicineResponses { set; get; } = new List<MedicineResponse>();
        }
    }

    public static class MedicineCategoryExtension
    {
        public static MedicineCategoryResponseForPharmacist ToCategoryResponsePharmacist(this MedicineCategory MedicineCategory)
        {
            return new MedicineCategoryResponseForPharmacist()
            {
                CateogryID = MedicineCategory.MedicineCategoryID,
                Name = MedicineCategory.Name,
                MedicineResponses=MedicineCategory?.Medicines.Select(m => m.ToMedicineResponse()).ToList()
                 
            };
        }
    }

 