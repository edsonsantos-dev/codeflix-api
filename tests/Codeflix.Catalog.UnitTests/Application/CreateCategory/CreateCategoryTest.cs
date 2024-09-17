using Codeflix.Catalog.Domain.Entities;
using Codeflix.Catalog.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Codeflix.Catalog.UnitTests.Application.CreateCategory;

[Trait("Application", "CreateCategory - Use Cases")]
public class CreateCategoryTest
{
    [Fact(DisplayName = nameof(CreateCategory))]
    public async void CreateCategory()
    {
        var repositoryMock = new Mock<ICategoryRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var useCase = new CreateCategory(repositoryMock.Object, unitOfWorkMock.Object);

        var input = new CreateCategoryInput("Category Name", "Category Description", true);

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(repository =>
                repository.Insert(It.IsAny<Category>(), It.IsAny<CancellationToken>()),
            Times.Once);

        unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Once);

        output.Should().NotBeNull()();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(input.IsActive);
        (output.Id != Guid.Empty && output.Id != null).Should().BeTrue();
        (output.CreatedAt != default && output.CreatedAt != null).Should().BeTrue();
    }
}