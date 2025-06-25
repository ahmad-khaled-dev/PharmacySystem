using Pharmacy.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.DTO.MedicineDTO{
    public class MedicineUpdateRequest
    {

        public string? Manufacturer { get; set; }

        public string? ActiveIngredient { get; set; }

        public int? CategoryID { get; set; }

        public int? MedicineTypeId { get; set; }

        public bool? IsRequiredDescription { set; get; }



        public void UpdateMedicine(Medicine medicine)
        {
            if (Manufacturer != null) medicine.Manufacturer = Manufacturer;
            if (ActiveIngredient != null) medicine.ActiveIngredient = ActiveIngredient;
            if (CategoryID.HasValue) medicine.CategoryID = CategoryID.Value;
            if (MedicineTypeId.HasValue) medicine.MedicineTypeId = MedicineTypeId.Value;
            if (IsRequiredDescription.HasValue) medicine.IsRequiredDescription = IsRequiredDescription.Value;
        }



    }
}
