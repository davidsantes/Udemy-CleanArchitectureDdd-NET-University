using CleaArchitecture.Domain.Alquileres;
using CleaArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Alquileres;

public interface IAlquilerRepository
{
    Task<Alquiler?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Garantiza que el vehículo no haya sido registrado en las fechas señaladas
    /// </summary>
    Task<bool> IsOverlappingAsync(
        Vehiculo vehiculo,
        DateRange duracion,
        CancellationToken cancellationToken = default
    );

    void Add(Alquiler alquiler);
}