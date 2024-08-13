using Codeflix.Catalog.Domain.Exceptions;

namespace Codeflix.Catalog.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public Category(string name, string description, bool isActive = true)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = DateTime.Now;
        
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new EntityValidationException("Name should not be empty or null");

        if (Name.Trim().Length < 3)
            throw new EntityValidationException("Name should be at leats 3 characters long");

        if (Name.Length > 255)
            throw new EntityValidationException("Name should be less or equal 255 characters long");
        
        if (Description == null)
            throw new EntityValidationException("Description should not be null");
        
        if (Description.Length > 10000)
            throw new EntityValidationException("Description should be less or equal 10.000 characters long");
    }
}