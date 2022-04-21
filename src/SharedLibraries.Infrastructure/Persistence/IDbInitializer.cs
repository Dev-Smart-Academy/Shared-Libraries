namespace SharedLibraries.Infrastructure.Persistence;

using System.Threading.Tasks;

public interface IDbInitializer
{
    Task Initialize();
}
