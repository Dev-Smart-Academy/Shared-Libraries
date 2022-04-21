namespace SharedLibraries.Infrastructure.Repositories;

using System.Threading;
using System.Threading.Tasks;

using Domain;
using Persistence;

public abstract class DataRepository<TDbContext, TEntity> : IDomainRepository<TEntity>
    where TDbContext : IDbContext
    where TEntity : class, IAggregateRoot
{
    protected DataRepository(TDbContext db) => this.Data = db;

    protected TDbContext Data { get; }

    public async Task Save(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        await this.Data.UpdateAsync(entity, cancellationToken);

        await this.Data.SaveChangesAsync(cancellationToken);
    }
}
