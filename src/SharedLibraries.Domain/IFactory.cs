namespace SharedLibraries.Domain;

public interface IFactory<out TEntity>
  where TEntity : IAggregateRoot
{
    TEntity Build();
}

