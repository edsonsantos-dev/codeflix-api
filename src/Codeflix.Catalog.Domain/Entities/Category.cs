using Codeflix.Catalog.Domain.Exceptions;
using Codeflix.Catalog.Domain.SeedWork;

namespace Codeflix.Catalog.Domain.Entities;

public class Category : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; set; }
    
    public Category(string name, string description, bool isActive = true) : base()
    {
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = DateTime.Now;
        
        Validate();
    }

    public void Activate()
    {
        IsActive = true;
        Validate();
    }
    
    public void Deactivate()
    {
        IsActive = false;
        Validate();
    }

    public void Update(string name, string? description = null)
    {
        Name = name;
        Description = description ?? Description;
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