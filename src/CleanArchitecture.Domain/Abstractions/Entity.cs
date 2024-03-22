namespace CleaArchitecture.Domain.Abstractions;

public abstract class Entity
{
    protected Entity(Guid id)
    {
        Id = id;
    }
    
    /// <summary>
    /// Identificador de la entidad. Init indica que una vez que ha sido inicializada la propiedad, no se puede cambiar su valor.
    /// </summary>
    public Guid Id {get ; init;}
}