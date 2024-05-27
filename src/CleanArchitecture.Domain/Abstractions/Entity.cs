namespace CleanArchitecture.Domain.Abstractions;

public abstract class Entity<TEntityId> : IEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    /// <summary>
    /// Constructor necesario para que EF funcione.
    /// </summary>
    protected Entity()
    {
    }

    protected Entity(TEntityId id)
    {
        Id = id;
    }

    /// <summary>
    /// Identificador de la entidad. Init indica que una vez que ha sido inicializada la propiedad, no se puede cambiar su valor.
    /// </summary>
    public TEntityId? Id { get; init; }

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    /// <summary>
    /// Añade eventos. Solo podrá ser disparado por los hijos de esta clase.
    /// </summary>
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}