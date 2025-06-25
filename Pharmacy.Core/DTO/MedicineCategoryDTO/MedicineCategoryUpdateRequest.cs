using Microsoft.AspNetCore.Http;
using Pharmacy.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;



namespace Pharmacy.Core.DTO.MedicineCategoryDTO
{
   public class MedicineCategoryUpdateRequest
    {

        [Required]
        public int CategoryID { set; get; }

        public string Name { set; get; }

        public IFormFile? Image { set; get; }


        public MedicineCategory ToCategoryMedicine(string ?ImagePath=null)
        {
            return new MedicineCategory()
            {
                      Name=this.Name,
                MedicineCategoryID = this.CategoryID,
                Image=ImagePath
            };
        }
    }
}
