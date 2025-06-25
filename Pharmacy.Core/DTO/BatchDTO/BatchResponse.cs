
using Pharmacy.Core.Domain.Entities;

namespace Pharmacy.Core.DTO.BatchDTO
{
    public class BatchResponse
    {
        public int BatchId { set; get; }

        public int ?RemainQuantity { set; get; }

        public string? Barcode { get; set; }

        public string? BatchNumber { get; set; }

        public DateTime ExpirationDate { get; set; }
         
    }

    public static class BatchExtension
        {
             
        public static BatchResponse ToBathcResponse(this Batch batch)
        {
            return new BatchResponse()
            {
                BatchId=batch.BatchID,
                RemainQuantity=batch.Quantity,
                Barcode = batch.Barcode,
                BatchNumber = batch.BatchNumber,
                ExpirationDate = batch.ExpirationDate
            };
        }
        }

}
