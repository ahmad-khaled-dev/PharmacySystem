
using Pharmacy.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.DTO.BatchDTO
{
    public class BatchUpdateRequest
    {
        [Required]
        public int BatchID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string? Barcode { get; set; }

        public string? BatchNumber { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Batch ToBatch(int ProductId)
        {
            return new Batch
            {
                BatchID = this.BatchID,
                ProductId = ProductID,
                Quantity = this.Quantity,
                Barcode = this.Barcode,
                BatchNumber = this.BatchNumber,
                ExpirationDate = this.ExpirationDate
            };
        }
    }

}
