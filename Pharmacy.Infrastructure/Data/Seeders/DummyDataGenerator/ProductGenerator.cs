using Bogus;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Enum;

namespace Pharmacy.Infrastructure.Data.Seeders.DummyDataGenerator
{

    public static class ProductGenerator
    {
        public static List<Product> GenerateProducts(int count, int[] categoryIds)
        {
            var faker = new Faker<Product>()
                //  .RuleFor(p => p.ProductId, f => f.IndexFaker + 1)
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                //   .RuleFor(p => p.Image, f => f.Image.PicsumUrl(200, 200))
                .RuleFor(p => p.SellPrice, f => f.Random.Decimal(5, 500))
                .RuleFor(p => p.MinimumStockLevel, f => f.Random.Int(0, 20))
                .RuleFor(p => p.ProductType, f => f.PickRandom<enProductType>())
                .RuleFor(p => p.CategoryProductID, f => f.PickRandom(categoryIds))

                ;

            return faker.Generate(count);
        }

    }
}
