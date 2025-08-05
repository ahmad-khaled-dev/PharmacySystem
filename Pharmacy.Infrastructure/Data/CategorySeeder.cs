using Microsoft.Extensions.DependencyInjection;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Infrastructure.DbContext;

public static class CategorySeeder
{
    public static async Task SeedCategoryAsync(IServiceProvider provider)
    {
        var db = provider.GetRequiredService<ApplicationDbContext>();

        if (!db.ProductCategories.Any(c => c.Name == "Medicine"))
        {
            db.ProductCategories.Add(new ProductCategory
            {
                Name = "Medicine"
            });

            await db.SaveChangesAsync();
        }
    }
}
