using CleanArchitecture.Domain.Abstractions;
using MediatR;

namespace CleanArchitecture.Application.Abstractions.Messaging;

/// <summary>
/// Interfaz para los controladores de comandos que manejan comandos de tipo genérico y producen resultados de tipo Result.
/// Implementa IRequestHandler de MediatR con un tipo de comando y un resultado de tipo Result.
/// </summary>
/// <typeparam name="TCommand">Tipo de dato del comando que maneja el controlador.</typeparam>
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
where TCommand : ICommand
{
}

/// <summary>
/// Interfaz para los controladores de comandos que manejan comandos de tipo genérico y producen resultados de tipo genérico.
/// Implementa IRequestHandler de MediatR con un tipo de comando y un resultado de tipo Result<TResponse>.
/// </summary>
/// <typeparam name="TCommand">Tipo de dato del comando que maneja el controlador.</typeparam>
/// <typeparam name="TResponse">Tipo de dato del resultado que produce el controlador.</typeparam>
public interface ICommandHandler<TCommand, TResponse>
: IRequestHandler<TCommand, Result<TResponse>>
where TCommand : ICommand<TResponse>
{
}