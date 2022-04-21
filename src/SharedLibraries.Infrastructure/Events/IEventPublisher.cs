namespace SharedLibraries.Infrastructure.Events;

using System;
using System.Threading.Tasks;

using Domain.Events;

internal interface IEventPublisher
{
    Task Publish(IDomainEvent domainEvent);

    Task Publish<TDomainEvent>(TDomainEvent domainEvent, Type domainEventType);
}
