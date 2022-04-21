namespace SharedLibraries.Application.Contracts;

using Domain;

public interface IQueryRepository<in TEntity>
    where TEntity : IAggregateRoot
{
}
