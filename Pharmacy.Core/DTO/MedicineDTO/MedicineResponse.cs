
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO.ProductDTO;

namespace Pharmacy.Core.DTO.MedicineDTO
{

    public class MedicineResponse
    {
      //  public int ProductId { set; get; }

        public string? Manufacturer { get; set; }

        public string? ActiveIngredient { get; set; }

        public string? CategoryName { get; set; }

        public string? MedicineTypeName { get; set; }

        public bool? IsRequiredDescription { set; get; }


        public ProductResponseSummary ProductResponse { set; get; } = default!;

    }


    public static class MedicineExtensions
    {
        public static MedicineResponse ToMedicineResponse(this Medicine medicine)
        {
            return new MedicineResponse
            {
             //   ProductId = medicine.ProductID,
                Manufacturer = medicine.Manufacturer,
                ActiveIngredient = medicine.ActiveIngredient,
                CategoryName = medicine.Category?.Name,
                MedicineTypeName = medicine.MedicineType?.Name,
                IsRequiredDescription = medicine.IsRequiredDescription,
                ProductResponse = medicine?.Product?.ToSummaryDto()!
            };

        }
    }
}