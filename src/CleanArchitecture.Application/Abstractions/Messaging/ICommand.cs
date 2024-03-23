using CleanArchitecture.Domain.Abstractions;
using MediatR;

namespace CleaArchitecture.Application.Abstractions.Messaging;

/// <summary>
/// Interfaz para los comandos que no devuelven ningún resultado.
/// Implementa IRequest de MediatR y IBaseCommand.
/// </summary>
public interface ICommand : IRequest<Result>, IBaseCommand
{
}

/// <summary>
/// Interfaz para los comandos que devuelven un resultado de tipo genérico.
/// Implementa IRequest de MediatR con un resultado de tipo Result y IBaseCommand.
/// </summary>
/// <typeparam name="TResponse">Tipo de dato del resultado del comando.</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}

/// <summary>
/// Interfaz base para los comandos. Si se necesitan funcionalidades comunes en todos los comandos en el futuro, 
/// se pueden agregar a IBaseCommand para que se reflejen en todos los comandos que la implementen.
/// </summary>
public interface IBaseCommand
{ }