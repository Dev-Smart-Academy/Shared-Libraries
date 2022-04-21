namespace SharedLibraries.Domain;

using System;
using System.Threading.Tasks;

public interface IInitialData
{
    Type EntityType { get; }
    Task Seed();
}
