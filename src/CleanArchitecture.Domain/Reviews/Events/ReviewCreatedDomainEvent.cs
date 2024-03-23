using CleanArchitecture.Domain.Abstractions;

namespace CleaArchitecture.Domain.Reviews.Events;

public sealed record ReviewCreatedDomainEvent(Guid AlquilerId) : IDomainEvent;