namespace Codeflix.Catalog.Application.UseCases.Category.Create;

public record CreateCategoryOutput(
    Guid Id,
    string Name,
    string Description,
    bool IsActive,
    DateTime CreatedOn);