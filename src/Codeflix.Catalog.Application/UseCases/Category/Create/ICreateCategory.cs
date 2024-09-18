namespace Codeflix.Catalog.Application.UseCases.Category.Create;

public interface ICreateCategory
{
    public Task<CreateCategoryOutput> Handle(CreateCategoryInput input, CancellationToken cancellationToken);
}