using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Core.Domain.Entities
{
    public class SaleItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Sale")]
        public int SaleId { get; set; }

        public  Sale Sale { get; set; }

        public int Quantity { get; set; }

        [Precision(18, 2)]
        public decimal? Price { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public  Product Product { set; get; }

        public string ? ImagPrescription { set; get; }

        public ICollection<InventoryItem>? InventoryItems { get; set; } = new List<InventoryItem>();
    }

}

