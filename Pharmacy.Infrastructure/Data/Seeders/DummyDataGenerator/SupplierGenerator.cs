using Bogus;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Enum;

namespace Pharmacy.Infrastructure.Data.Seeders.DummyDataGenerator
{
    
public static class SupplierGenerator
    {
        public static List<Supplier> GenerateSuppliers(int count)
        {
            var faker = new Faker<Supplier>()
              //  .RuleFor(s => s.SupplierID, f => f.IndexFaker + 1)
                .RuleFor(s => s.Name, f => f.Company.CompanyName())
                .RuleFor(s => s.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(s => s.Email, f => f.Internet.Email());

            return faker.Generate(count);
        }
          
    }

}
