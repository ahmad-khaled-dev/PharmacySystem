using Bogus;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Enum;

namespace Pharmacy.Infrastructure.Data.Seeders.DummyDataGenerator
{
    
public static class MedicineTypeGenerator
    { 
        public static List<MedicineType> GenerateMedicineTypes(int count)
        {
            var faker = new Faker<MedicineType>()
              //  .RuleFor(mt => mt.Id, f => f.IndexFaker + 1)
                .RuleFor(mt => mt.Name, f => f.Commerce.ProductAdjective());

            return faker.Generate(count);
        }
         
    }

}
