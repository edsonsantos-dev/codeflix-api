﻿using Codeflix.Catalog.Domain.SeedWork;
using Codeflix.Catalog.Domain.Validations;

namespace Codeflix.Catalog.Domain.Entities;

public class Category : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedOn { get; set; }

    protected Category()
    {
    }

    public Category(string name, string description, bool isActive = true)
    {
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedOn = DateTime.Now;

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
        DomainValidation.NotNullOrEmpty(Name, nameof(Name));
        DomainValidation.MinLength(Name, minLength: 3, nameof(Name));
        DomainValidation.MaxLength(Name, maxLength: 255, nameof(Name));
        DomainValidation.NotNull(Description, nameof(Description));
        DomainValidation.MaxLength(Description, maxLength: 10000, nameof(Description));
    }
}