using Pharmacy.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.DTO.MedicineDTO

{

    public class MedicineAddRequest
    {
        public string? Manufacturer { get; set; }

        public string? ActiveIngredient { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        public int MedicineTypeId { get; set; }

        public bool IsRequiredDescription { set; get; }

        public Medicine ToMedicine()
        {
            return new Medicine
            {
                Manufacturer = this.Manufacturer,
                ActiveIngredient = this.ActiveIngredient,
                CategoryID = this.CategoryID,
                MedicineTypeId = this.MedicineTypeId,
                IsRequiredDescription = this.IsRequiredDescription

            };
        }
    }
}