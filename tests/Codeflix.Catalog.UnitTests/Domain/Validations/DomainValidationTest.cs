using Bogus;
using Codeflix.Catalog.Domain.Exceptions;
using Codeflix.Catalog.Domain.Validations;
using FluentAssertions;

namespace Codeflix.Catalog.UnitTests.Domain.Validations;

[Trait("Domain", "DomainValidation - Validation")]
public class DomainValidationTest
{
    private Faker Faker { get; set; } = new Faker();
    
    [Fact(DisplayName = nameof(NotNullOk))]
    public void NotNullOk()
    {
        //Arrange
        var value = Faker.Commerce.ProductName();
        
        //Act
        var action = () => DomainValidation.NotNull(value, "Value");
        
        //Assert
        action.Should().NotThrow();
    }
    
    [Fact(DisplayName = nameof(NotNullThrowWhenNull))]
    public void NotNullThrowWhenNull()
    {
        //Arrange
        string? value = null;
        
        //Act
        var action = () => DomainValidation.NotNull(value, "FieldName");
        
        //Assert
        action.Should().Throw<EntityValidationException>()
            .WithMessage("FieldName should not be null");
    }
    
    [Fact(DisplayName = nameof(NotNullOrEmptyOk))]
    public void NotNullOrEmptyOk()
    {
        //Arrange
        var value = Faker.Commerce.ProductName();
        
        //Act
        var action = () => DomainValidation.NotNullOrEmpty(value, "Value");
        
        //Assert
        action.Should().NotThrow();
    }
    
    [Theory(DisplayName = nameof(NotNullOrEmptyThrowWhenEmpty))]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void NotNullOrEmptyThrowWhenEmpty(string? target)
    {
        //Arrange
        var action = () => DomainValidation.NotNullOrEmpty(target, "FieldName");
        
        //Act
        //Assert
        action.Should().Throw<EntityValidationException>()
            .WithMessage("FieldName should not be null or empty");
    }
}