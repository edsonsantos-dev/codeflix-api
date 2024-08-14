using Codeflix.Catalog.Domain.Entities;

namespace Codeflix.Catalog.UnitTests.Domain.Fixturies;

public class CategoryTestFixture : BaseFixture
{
    public Category GetValidCategory() => new(
        GetValidCategoryName(),
        GetValidCategoryDescription()
    );

    public string GetValidCategoryName()
    {
        var categoryName = string.Empty;

        while (categoryName.Length < 3)
            categoryName = Faker.Commerce.Categories(1)[0];

        if (categoryName.Length > 255)
            categoryName = categoryName[..255];

        return categoryName;
    }

    public string GetValidCategoryDescription()
    {
        var categoryDescription = Faker.Commerce.ProductDescription();
        return categoryDescription.Length < 10000 ? categoryDescription : categoryDescription[..10000];
    }
}

[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture>;