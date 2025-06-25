using Microsoft.AspNetCore.Http;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO.MedicineDTO;
using Pharmacy.Core.Enum;
using System.ComponentModel.DataAnnotations;


namespace Pharmacy.Core.DTO.ProductDTO
{
  public  class ProductAddRequest
    {
        [Required]
        public  string Name { set; get; }

        public string? Description { set; get; }
        
        public int CategoryId { set; get; }

        [Required]
        public int? MinimumStockLevel { get; set; }

        [Required]
        public decimal? SellPrice { set; get; }

        public IFormFile ? Image { set; get; }

        public enProductType ProductType { set; get; }

        public MedicineAddRequest? MedicineAddRequest { set; get; }

        public Product ToProduct(string ?ImagePath =null)
        {
            var product= new Product()
            {
                CategoryProductID = this.CategoryId,
                Description = this.Description,
                Name = this.Name,
                MinimumStockLevel = this.MinimumStockLevel,
                SellPrice = this.SellPrice,
                Image = ImagePath,
                ProductType=this.ProductType
                
            };


             
            if (this.ProductType == enProductType.Medicine && MedicineAddRequest is not null)
            {
                product.Medicine = this.MedicineAddRequest.ToMedicine();
            }

            return product;
        } 
    }

}
