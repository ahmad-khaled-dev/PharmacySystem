using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Core.Domain.Entities
{
    public class DamagedProduct
    {
        [Key]
        public int DamagedID { set; get; }

        [ForeignKey("Product")]
        public int? ProductId { set; get; }

        public Product? Product { set; get; }

        [ForeignKey("Batch")]
        public int BatchID { set; get; }

        public Batch ?Batch { set; get; }

        public int Quauntity { set; get; }
      
        [Precision(18, 2)]
        public decimal? Price { set; get; }

        public DateTime DamageDate { set; get; }

        public string? Notes { set; get; }

    }
}