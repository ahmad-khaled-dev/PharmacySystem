using Pharmacy.Core.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Core.Domain.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Image { get; set; }

        public decimal? SellPrice { get; set; }


        public int? MinimumStockLevel { get; set; }

        public enProductType ProductType { get; set; }  

        [ForeignKey(nameof(ProductCategory))]
        public int? CategoryProductID { get; set; }

        public ProductCategory? ProductCategory { get; set; }

        public Medicine? Medicine { get; set; }

        public ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();

        public ICollection<DamagedProduct> DamagedProducts { get; set; } = new List<DamagedProduct>();

        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }


}
