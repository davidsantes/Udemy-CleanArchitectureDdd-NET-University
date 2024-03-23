using CleanArchitecture.Domain.Abstractions;
using MediatR;

namespace CleaArchitecture.Application.Abstractions.Messaging;

/// <summary>
/// Interfaz para los controladores de consultas que manejan consultas de tipo genérico y producen resultados de tipo genérico.
/// Implementa IRequestHandler de MediatR con un tipo de consulta y un resultado de tipo Result.
/// </summary>
/// <typeparam name="TQuery">Tipo de dato de la consulta que maneja el controlador.</typeparam>
/// <typeparam name="TResponse">Tipo de dato del resultado que produce el controlador.</typeparam>
public interface IQueryHandler<TQuery, TResponse>
: IRequestHandler<TQuery, Result<TResponse>>
where TQuery : IQuery<TResponse>
{
}