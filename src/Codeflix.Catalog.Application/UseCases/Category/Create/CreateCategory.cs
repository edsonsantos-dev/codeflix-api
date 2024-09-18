using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Domain.Repositories;
using Entity = Codeflix.Catalog.Domain.Entities;

namespace Codeflix.Catalog.Application.UseCases.Category.Create;

public class CreateCategory(ICategoryRepository repository, IUnitOfWork uow) : ICreateCategory
{
    public async Task<CreateCategoryOutput> Handle(CreateCategoryInput input, CancellationToken cancellationToken)
    {
        var category = new Entity.Category(
            input.Name,
            input.Description,
            input.IsActive);

        await repository.Insert(category, cancellationToken);
        await uow.Commit(cancellationToken);

        return new CreateCategoryOutput(
            category.Id,
            category.Name,
            category.Description,
            category.IsActive,
            category.CreatedOn);
    }
}