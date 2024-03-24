using CleanArchitecture.Domain.Abstractions;
using MediatR;

namespace CleanArchitecture.Application.Abstractions.Messaging;

/// <summary>
/// Interfaz para las consultas que devuelven un resultado de tipo genérico.
/// Implementa IRequest de MediatR con un resultado de tipo Result.
/// </summary>
/// <typeparam name="TResponse">Tipo de dato genérico del resultado de la consulta.</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
    
}