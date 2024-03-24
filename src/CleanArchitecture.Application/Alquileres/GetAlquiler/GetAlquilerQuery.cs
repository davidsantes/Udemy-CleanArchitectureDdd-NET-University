using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Alquileres.GetAlquiler;

/// <summary>
/// Consulta para obtener un alquiler.
/// Implementa la interfaz IQuery<T> donde T es AlquilerResponse, indicando que esta clase es una consulta y devolverá una respuesta de tipo AlquilerResponse.
/// </summary>
public sealed record GetAlquilerQuery(Guid AlquilerId) : IQuery<AlquilerResponse>;