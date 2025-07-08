using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pharmacy.Infrastructure.Data.Seeders.DummyDataGenerator;
using Pharmacy.Infrastructure.DbContext;


namespace Pharmacy.Infrastructure.Data.Seeders
{
    public class DbSeederService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public DbSeederService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
 
            if (db.Products.Any() || db.Medicines.Any())
                return;

             
        
            var productCategories = ProductCategoryGenerator.GenerateProductCategories(5);
            db.ProductCategories.AddRange(productCategories);
            await db.SaveChangesAsync();

            var medicineCategories = MedicineCategoryGenerator.GenerateMedicineCategories(4);
            db.MedicineCategories.AddRange(medicineCategories);
            await db.SaveChangesAsync();

            var medicineTypes = MedicineTypeGenerator.GenerateMedicineTypes(3);
            db.MedicineTypes.AddRange(medicineTypes);
            await db.SaveChangesAsync();

           
            var products = ProductGenerator.GenerateProducts(
                20,
                productCategories.Select(c => c.CategoryId).ToArray()
            );
            db.Products.AddRange(products);
            await db.SaveChangesAsync();

           
            var medicines = MedicineGenerator.GenerateMedicines(
                 
                productIds: products.Select(p => p.ProductId).ToArray(),
                medicineCategoryIds: medicineCategories.Select(m => m.MedicineCategoryID).ToArray(),
                medicineTypeIds: medicineTypes.Select(m => m.Id).ToArray()
            );
            db.Medicines.AddRange(medicines);
            await db.SaveChangesAsync();

 
            var suppliers = SupplierGenerator.GenerateSuppliers(8);
            db.Suppliers.AddRange(suppliers);
            await db.SaveChangesAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
