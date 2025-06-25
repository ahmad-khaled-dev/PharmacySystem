using Microsoft.AspNetCore.Http;
using Pharmacy.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.DTO.MedicineCategoryDTO
{
  public  class MedicineCategoryAddRequest
    {
        [Required]
        public string Name { set; get; }

        public IFormFile ?Image { set; get; }


        public MedicineCategory ToCategoryMedicine(string? ImagePath=null)
        {
            return new MedicineCategory()
            {
                Name = this.Name,
                Image = ImagePath

            };
        }
    }
}
