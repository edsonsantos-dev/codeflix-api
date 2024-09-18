using Codeflix.Catalog.Domain.Entities;
using FluentAssertions;
using Moq;
using UseCase = Codeflix.Catalog.Application.UseCases.Category.Create;

namespace Codeflix.Catalog.UnitTests.Application.CreateCategory;

[Trait("Application", "CreateCategory - Use Cases")]
[Collection(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTest(CreateCategoryTestFixture fixture)
{
    [Fact(DisplayName = nameof(CreateCategory))]
    public async void CreateCategory()
    {
        var repositoryMock = fixture.GetRepositoryMock();
        var unitOfWorkMock = fixture.GetUnitOfWorkMock();
        var createCategory = new UseCase.CreateCategory(repositoryMock.Object, unitOfWorkMock.Object);

        var input = fixture.GetValidCategoryInput();

        var output = await createCategory.Handle(input, CancellationToken.None);

        repositoryMock.Verify(repository =>
                repository.Insert(
                    It.IsAny<Category>(),
                    It.IsAny<CancellationToken>()),
            Times.Once);

        unitOfWorkMock.Verify(uow =>
            uow.Commit(It.IsAny<CancellationToken>()), Times.Once);

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(input.IsActive);
        output.Id.Should().NotBeEmpty();
        output.CreatedOn.Should().NotBeSameDateAs(default);
    }
}