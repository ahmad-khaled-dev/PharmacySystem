using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO.MedicineCategoryDTO;

namespace Pharmacy.Core.DTO.MedicineCategoryDTO
{

    public class MedicineCategoryResponseForCustomer
    {
        public int CateogryID { set; get; }

        public string Name { set; get; }

        public string? Image { set; get; }
    }
}

public static class MedicineCategoryForCustomerExtension
{
    public static MedicineCategoryResponseForCustomer ToCategoryResponseCustomer(this MedicineCategory MedicineCategory)
    {
        return new MedicineCategoryResponseForCustomer()
        {
            CateogryID = MedicineCategory.MedicineCategoryID,
            Name = MedicineCategory.Name,
            Image = MedicineCategory.Image
        };
    }
}

