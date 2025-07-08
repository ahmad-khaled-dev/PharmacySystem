using Bogus;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Enum;

namespace Pharmacy.Infrastructure.Data.Seeders.DummyDataGenerator
{
    
public static class MedicineCategoryGenerator
    { 
        public static List<MedicineCategory> GenerateMedicineCategories(int count)
        {
            var faker = new Faker<MedicineCategory>()
               // .RuleFor(mc => mc.MedicineCategoryID, f => f.IndexFaker + 1)
                .RuleFor(mc => mc.Name, f => f.Commerce.Categories(1)[0]);
             //   .RuleFor(mc => mc.Image, f => f.Image.PicsumUrl(200, 200));

            return faker.Generate(count);
        }
         

    }

}
