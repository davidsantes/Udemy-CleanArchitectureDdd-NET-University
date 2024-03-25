using CleanArchitecture.Application.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Abstractions.Behaviors;

/// <summary>
/// Interceptor que captura todos los request que envíe el cliente, al insertar un nuevo record de tipo Command que implementen IBaseCommand.
/// Comportamiento para registrar información de log al ejecutar commands (IBaseCommand).
/// </summary>
/// <typeparam name="TRequest">Tipo de la solicitud del command.</typeparam>
/// <typeparam name="TResponse">Tipo de la respuesta del command.</typeparam>
public class LoggingBehavior<TRequest, TResponse>
: IPipelineBehavior<TRequest, TResponse>
where TRequest : IBaseCommand
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
        )
    {
        var name = request.GetType().Name;

        try
        {
            _logger.LogInformation($"Registro del command request {name} antes de ejecutarse");
            var result = await next();
            _logger.LogInformation($"Registro del command request {name} después de ejecutarse");

            return result;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"El comando {name} tuvo errores");
            throw;
        }
    }
}