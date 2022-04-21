namespace SharedLibraries.Infrastructure.Persistence;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public interface IDbContext
{
    IQueryable<TEntity> All<TEntity>() where TEntity : class;

    IQueryable<TEntity> AllAsNoTracking<TEntity>() where TEntity : class;

    Task UpdateAsync<TEntity>(TEntity entity, CancellationToken token = default) where TEntity : class;

    Task SaveChangesAsync(CancellationToken token = default);
}
