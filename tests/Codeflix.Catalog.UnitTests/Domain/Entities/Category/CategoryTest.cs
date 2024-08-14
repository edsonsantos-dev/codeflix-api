using Codeflix.Catalog.Domain.Exceptions;

namespace Codeflix.Catalog.UnitTests.Domain.Entities.Category;

[Trait("Domain", "Catetory - Aggregates")]
public class CategoryTest
{
    [Fact(DisplayName = nameof(Instantiate))]
    public void Instantiate()
    {
        //Arrange
        var validData = new
        {
            Name = "category name",
            Description = "category description"
        };

        //Act
        var datetimeBefore = DateTime.Now;
        var category = new Catalog.Domain.Entities.Category(validData.Name, validData.Description);
        var datetimeAfter = DateTime.Now;

        //Assert
        Assert.NotNull(category);
        Assert.Equal(validData.Name, category.Name);
        Assert.Equal(validData.Description, category.Description);
        Assert.NotEqual(category.Id, default);
        Assert.NotEqual(category.CreatedAt, default);
        Assert.True(category.IsActive);
        Assert.True(category.CreatedAt >= datetimeBefore && category.CreatedAt <= datetimeAfter);
    }

    [Theory(DisplayName = nameof(InstantiateWithIsActive))]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithIsActive(bool isActive)
    {
        //Arrange
        var validData = new
        {
            Name = "category name",
            Description = "category description"
        };

        //Act
        var datetimeBefore = DateTime.Now;
        var category = new Catalog.Domain.Entities.Category(validData.Name, validData.Description, isActive);
        var datetimeAfter = DateTime.Now;

        //Assert
        Assert.NotNull(category);
        Assert.Equal(validData.Name, category.Name);
        Assert.Equal(validData.Description, category.Description);
        Assert.NotEqual(category.Id, default);
        Assert.NotEqual(category.CreatedAt, default);
        Assert.Equal(category.IsActive, isActive);
        Assert.True(category.CreatedAt >= datetimeBefore && category.CreatedAt <= datetimeAfter);
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void InstantiateErrorWhenNameIsEmpty(string? name)
    {
        //Arrange
        Action action = () => new Catalog.Domain.Entities.Category(name, "Category Description");

        //Act
        var exception = Assert.Throws<EntityValidationException>(action);

        //Assert
        Assert.Equal("Name should not be empty or null", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
    public void InstantiateErrorWhenDescriptionIsNull()
    {
        //Arrange
        Action action = () => new Catalog.Domain.Entities.Category("Category Name", null);

        //Act
        var exception = Assert.Throws<EntityValidationException>(action);

        //Assert
        Assert.Equal("Description should not be null", exception.Message);
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
    [InlineData("a")]
    [InlineData(" a")]
    [InlineData("a ")]
    [InlineData("aa")]
    public void InstantiateErrorWhenNameIsLessThan3Characters(string? name)
    {
        //Arrange
        Action action = () => new Catalog.Domain.Entities.Category(name, "Category Description");

        //Act
        var exception = Assert.Throws<EntityValidationException>(action);

        //Assert
        Assert.Equal("Name should be at leats 3 characters long", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
    public void InstantiateErrorWhenNameIsGreaterThan255Characters()
    {
        //Arrange
        Action action = () => new Catalog.Domain.Entities.Category(
            "Jornadas Épicas que Transcendem o Tempo e o Espaço: Uma Odisseia Cinematográfica Através de Mundos Fantásticos, Realidades Alternativas e Universos Paralelos, Onde Heróis Enfrentam Desafios Monumentais e Descobrem o Verdadeiro Significado da Coragem, da Amizade e do Amor em Uma Luta Infinita Contra as Forças do Mal",
            "Category Description");

        //Act
        var exception = Assert.Throws<EntityValidationException>(action);

        //Assert
        Assert.Equal("Name should be less or equal 255 characters long", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan10000Characters))]
    public void InstantiateErrorWhenDescriptionIsGreaterThan10000Characters()
    {
        //Arrange
        var invalidDescription = string.Join(null, Enumerable.Range(1, 10001).Select(_ => "a"));
        Action action = () => new Catalog.Domain.Entities.Category("Category Name", invalidDescription);

        //Act
        var exception = Assert.Throws<EntityValidationException>(action);

        //Assert
        Assert.Equal("Description should be less or equal 10.000 characters long", exception.Message);
    }

    [Fact(DisplayName = nameof(Activate))]
    public void Activate()
    {
        //Arrange
        var validData = new
        {
            Name = "Category name",
            Description = "Category description"
        };

        var category = new Catalog.Domain.Entities.Category(validData.Name, validData.Description, false);

        //Act
        category.Activate();

        //Assert
        Assert.True(category.IsActive);
    }

    [Fact(DisplayName = nameof(Deactivate))]
    public void Deactivate()
    {
        //Arrange
        var validData = new
        {
            Name = "Category name",
            Description = "Category description"
        };

        var category = new Catalog.Domain.Entities.Category(validData.Name, validData.Description);

        //Act
        category.Deactivate();

        //Assert
        Assert.False(category.IsActive);
    }

    [Fact(DisplayName = nameof(Update))]
    public void Update()
    {
        //Arrange
        var validData = new
        {
            Name = "Category name",
            Description = "Category description"
        };

        var newValues = new
        {
            Name = "New name",
            Description = "New description"
        };

        var category = new Catalog.Domain.Entities.Category(validData.Name, validData.Description);

        //Act
        category.Update(newValues.Name, newValues.Description);

        //Assert
        Assert.Equal(newValues.Name, category.Name);
        Assert.Equal(newValues.Description, category.Description);
    }

    [Fact(DisplayName = nameof(UpdateOnlyName))]
    public void UpdateOnlyName()
    {
        //Arrange
        var validData = new
        {
            Name = "Category name",
            Description = "Category description"
        };

        var newValues = new
        {
            Name = "New name",
        };

        var category = new Catalog.Domain.Entities.Category(validData.Name, validData.Description);
        var currentDescription = category.Description;

        //Act
        category.Update(newValues.Name);

        //Assert
        Assert.Equal(newValues.Name, category.Name);
        Assert.Equal(currentDescription, category.Description);
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsEmpty))]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void UpdateErrorWhenNameIsEmpty(string? name)
    {
        //Arrange
        var category = new Catalog.Domain.Entities.Category("Category Name", "Category Description");
        Action action = () => category.Update(name);

        //Act
        var exception = Assert.Throws<EntityValidationException>(action);

        //Assert
        Assert.Equal("Name should not be empty or null", exception.Message);
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsLessThan3Characters))]
    [InlineData("a")]
    [InlineData(" a")]
    [InlineData("a ")]
    [InlineData("aa")]
    public void UpdateErrorWhenNameIsLessThan3Characters(string? name)
    {
        //Arrange
        var category = new Catalog.Domain.Entities.Category("Category Name", "Category Description");
        Action action = () => category.Update(name);
    
        //Act
        var exception = Assert.Throws<EntityValidationException>(action);
    
        //Assert
        Assert.Equal("Name should be at leats 3 characters long", exception.Message);
    }
    
    [Fact(DisplayName = nameof(UpdateErrorWhenNameIsGreaterThan255Characters))]
    public void UpdateErrorWhenNameIsGreaterThan255Characters()
    {
        //Arrange
        var category = new Catalog.Domain.Entities.Category("Category Name", "Category Description");
        Action action = () =>
            category.Update(
                "Jornadas Épicas que Transcendem o Tempo e o Espaço: Uma Odisseia Cinematográfica Através de Mundos Fantásticos, Realidades Alternativas e Universos Paralelos, Onde Heróis Enfrentam Desafios Monumentais e Descobrem o Verdadeiro Significado da Coragem, da Amizade e do Amor em Uma Luta Infinita Contra as Forças do Mal");
    
        //Act
        var exception = Assert.Throws<EntityValidationException>(action);
    
        //Assert
        Assert.Equal("Name should be less or equal 255 characters long", exception.Message);
    }
    
    [Fact(DisplayName = nameof(UpdateErrorWhenDescriptionIsGreaterThan10000Characters))]
    public void UpdateErrorWhenDescriptionIsGreaterThan10000Characters()
    {
        //Arrange
        var invalidDescription = string.Join(null, Enumerable.Range(1, 10001).Select(_ => "a"));
        var category = new Catalog.Domain.Entities.Category("Category Name", "Category Description");
        Action action = () => category.Update(category.Name, invalidDescription);
    
        //Act
        var exception = Assert.Throws<EntityValidationException>(action);
    
        //Assert
        Assert.Equal("Description should be less or equal 10.000 characters long", exception.Message);
    }
}