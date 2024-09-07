using Codeflix.Catalog.Domain.Exceptions;
using Codeflix.Catalog.UnitTests.Domain.Fixturies;
using FluentAssertions;

namespace Codeflix.Catalog.UnitTests.Domain.Entities.Category;

[Trait("Domain", "Catetory - Aggregates")]
[Collection(nameof(CategoryTestFixture))]
public class CategoryTest(CategoryTestFixture categoryTestFixture)
{
    [Fact(DisplayName = nameof(Instantiate))]
    public void Instantiate()
    {
        //Arrange
        var validData = categoryTestFixture.GetValidCategory();

        //Act
        var datetimeBefore = DateTime.Now;
        var category = new Catalog.Domain.Entities.Category(validData.Name, validData.Description);
        var datetimeAfter = DateTime.Now;

        //Assert
        category.Should().NotBeNull();
        category.Name.Should().Be(validData.Name);
        category.Description.Should().Be(validData.Description);
        category.Id.Should().NotBeEmpty();
        category.CreatedAt.Should().NotBeSameDateAs(default);
        category.IsActive.Should().BeTrue();
        (category.CreatedAt >= datetimeBefore && category.CreatedAt <= datetimeAfter).Should().BeTrue();
    }

    [Theory(DisplayName = nameof(InstantiateWithIsActive))]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithIsActive(bool isActive)
    {
        //Arrange
        var validData = categoryTestFixture.GetValidCategory();

        //Act
        var datetimeBefore = DateTime.Now;
        var category = new Catalog.Domain.Entities.Category(validData.Name, validData.Description, isActive);
        var datetimeAfter = DateTime.Now;

        //Assert
        category.Should().NotBeNull();
        category.Name.Should().Be(validData.Name);
        category.Description.Should().Be(validData.Description);
        category.Id.Should().NotBeEmpty();
        category.CreatedAt.Should().NotBeSameDateAs(default);
        category.IsActive.Should().Be(isActive);
        (category.CreatedAt >= datetimeBefore && category.CreatedAt <= datetimeAfter).Should().BeTrue();
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void InstantiateErrorWhenNameIsEmpty(string? name)
    {
        //Arrange
        var validCategory = categoryTestFixture.GetValidCategory();
        Action action = () => new Catalog.Domain.Entities.Category(name, validCategory.Description);

        //Act
        //Assert
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be null or empty");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
    public void InstantiateErrorWhenDescriptionIsNull()
    {
        //Arrange
        var validCategory = categoryTestFixture.GetValidCategory();
        Action action = () => new Catalog.Domain.Entities.Category(validCategory.Name, null);

        //Act
        //Assert
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Description should not be null");
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
    [MemberData(nameof(GetNamesWithIsLessThan3Characters), parameters: 10)]
    public void InstantiateErrorWhenNameIsLessThan3Characters(string? name)
    {
        //Arrange
        var validCategory = categoryTestFixture.GetValidCategory();
        Action action = () => new Catalog.Domain.Entities.Category(name, validCategory.Description);

        //Act
        //Assert
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should be at least 3 characters long");
    }

    public static IEnumerable<object[]> GetNamesWithIsLessThan3Characters(int numberOfTests = 6)
    {
        var fixture = new CategoryTestFixture();

        for (var i = 0; i < numberOfTests; i++)
        {
            var isOdd = i % 2 != 0;
            yield return [fixture.GetValidCategoryName()[..(isOdd ? 1 : 2)]];
        }
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
    public void InstantiateErrorWhenNameIsGreaterThan255Characters()
    {
        //Arrange
        var validCategory = categoryTestFixture.GetValidCategory();
        var invalidName = categoryTestFixture.Faker.Lorem.Letter(256);
        Action action = () => new Catalog.Domain.Entities.Category(
            invalidName,
            validCategory.Description);

        //Act
        //Assert
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should be greater than 255 characters long");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan10000Characters))]
    public void InstantiateErrorWhenDescriptionIsGreaterThan10000Characters()
    {
        //Arrange
        var validCategory = categoryTestFixture.GetValidCategory();
        var invalidDescription = categoryTestFixture.Faker.Lorem.Letter(10001);
        Action action = () => new Catalog.Domain.Entities.Category(validCategory.Name, invalidDescription);

        //Act
        //Assert
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Description should be greater than 10000 characters long");
    }

    [Fact(DisplayName = nameof(Activate))]
    public void Activate()
    {
        //Arrange
        var validData = categoryTestFixture.GetValidCategory();
        var category = new Catalog.Domain.Entities.Category(validData.Name, validData.Description, false);

        //Act
        category.Activate();

        //Assert
        category.IsActive.Should().BeTrue();
    }

    [Fact(DisplayName = nameof(Deactivate))]
    public void Deactivate()
    {
        //Arrange
        var validData = categoryTestFixture.GetValidCategory();
        var category = new Catalog.Domain.Entities.Category(validData.Name, validData.Description);

        //Act
        category.Deactivate();

        //Assert
        category.IsActive.Should().BeFalse();
    }

    [Fact(DisplayName = nameof(Update))]
    public void Update()
    {
        //Arrange
        var validData = categoryTestFixture.GetValidCategory();
        var newValues = categoryTestFixture.GetValidCategory();

        var category = new Catalog.Domain.Entities.Category(validData.Name, validData.Description);

        //Act
        category.Update(newValues.Name, newValues.Description);

        //Assert
        newValues.Name.Should().Be(category.Name);
        newValues.Description.Should().Be(category.Description);
    }

    [Fact(DisplayName = nameof(UpdateOnlyName))]
    public void UpdateOnlyName()
    {
        //Arrange
        var validData = categoryTestFixture.GetValidCategory();
        var newValues = categoryTestFixture.GetValidCategory();

        var category = new Catalog.Domain.Entities.Category(validData.Name, validData.Description);
        var currentDescription = category.Description;

        //Act
        category.Update(newValues.Name);

        //Assert
        newValues.Name.Should().Be(category.Name);
        currentDescription.Should().Be(category.Description);
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsEmpty))]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void UpdateErrorWhenNameIsEmpty(string? name)
    {
        //Arrange
        var category = categoryTestFixture.GetValidCategory();
        Action action = () => category.Update(name);

        //Act
        //Assert
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be null or empty");
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsLessThan3Characters))]
    [InlineData("a")]
    [InlineData(" a")]
    [InlineData("a ")]
    [InlineData("aa")]
    public void UpdateErrorWhenNameIsLessThan3Characters(string? name)
    {
        //Arrange
        var category = categoryTestFixture.GetValidCategory();
        Action action = () => category.Update(name);

        //Act
        //Assert
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should be at least 3 characters long");
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenNameIsGreaterThan255Characters))]
    public void UpdateErrorWhenNameIsGreaterThan255Characters()
    {
        //Arrange
        var category = categoryTestFixture.GetValidCategory();
        var invalidName = categoryTestFixture.Faker.Lorem.Letter(256);
        Action action = () =>
            category.Update(invalidName);

        //Act
        //Assert
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should be greater than 255 characters long");
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenDescriptionIsGreaterThan10000Characters))]
    public void UpdateErrorWhenDescriptionIsGreaterThan10000Characters()
    {
        //Arrange
        var invalidDescription = categoryTestFixture.Faker.Lorem.Letter(10001);
        var category = categoryTestFixture.GetValidCategory();
        Action action = () => category.Update(category.Name, invalidDescription);

        //Act
        //Assert
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Description should be greater than 10000 characters long");
    }
}