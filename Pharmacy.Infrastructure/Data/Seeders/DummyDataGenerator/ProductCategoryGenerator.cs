using Bogus;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Enum;

namespace Pharmacy.Infrastructure.Data.Seeders.DummyDataGenerator
{
    
public static class ProductCategoryGenerator
    {
    
        public static List<ProductCategory> GenerateProductCategories(int count)
        {
            var faker = new Faker<ProductCategory>()
            //    .RuleFor(pc => pc.CategoryId, f => f.IndexFaker + 1)
                .RuleFor(pc => pc.Name, f => f.Commerce.Categories(1)[0]);
                //.RuleFor(pc => pc.Image, f => f.Image.PicsumUrl(200, 200));

            return faker.Generate(count);
        }


    }

}
