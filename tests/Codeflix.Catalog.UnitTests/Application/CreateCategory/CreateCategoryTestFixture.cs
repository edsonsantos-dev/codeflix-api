using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Application.UseCases.Category.Create;
using Codeflix.Catalog.Domain.Repositories;
using Codeflix.Catalog.UnitTests.Common;
using Moq;

namespace Codeflix.Catalog.UnitTests.Application.CreateCategory;

public class CreateCategoryTestFixture : BaseFixture
{
    public CreateCategoryInput GetValidCategoryInput() =>
        new(GetValidCategoryName(), GetValidCategoryDescription(), GetRandomBoolean());

    public Mock<ICategoryRepository> GetRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();

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

    public bool GetRandomBoolean() =>
        new Random().NextDouble() < 0.5;
}

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture>;