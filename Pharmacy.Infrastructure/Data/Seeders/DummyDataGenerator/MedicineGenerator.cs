using Bogus;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Enum;

namespace Pharmacy.Infrastructure.Data.Seeders.DummyDataGenerator
{
    
public static class MedicineGenerator
    {
         
         
      public static List<Medicine> GenerateMedicines(
    int[] productIds,
    int[] medicineCategoryIds,
    int[] medicineTypeIds)
{
    var faker = new Faker<Medicine>();

    return productIds.Select(productId => faker
        .RuleFor(m => m.ProductID, _ => productId)
        .RuleFor(m => m.Manufacturer, f => f.Company.CompanyName())
        .RuleFor(m => m.ActiveIngredient, f => f.Commerce.ProductMaterial())
        .RuleFor(m => m.CategoryID, f => f.PickRandom(medicineCategoryIds))
        .RuleFor(m => m.MedicineTypeId, f => f.PickRandom(medicineTypeIds))
        .RuleFor(m => m.IsRequiredDescription, f => f.Random.Bool())
        .Generate()).ToList();
}

    }

}
