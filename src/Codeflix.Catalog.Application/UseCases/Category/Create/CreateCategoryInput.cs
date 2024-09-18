namespace Codeflix.Catalog.Application.UseCases.Category.Create;

public record CreateCategoryInput(string Name, string Description, bool IsActive = true);