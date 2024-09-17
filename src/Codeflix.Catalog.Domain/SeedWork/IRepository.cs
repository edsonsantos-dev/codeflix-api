namespace Codeflix.Catalog.Domain.SeedWork;

public interface IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
{
    public Task Insert(TAggregateRoot aggregateRoot, CancellationToken cancellationToken);
}