using MediatR;

namespace CleanArchitecture.Domain.Abstractions;

/// <summary>
/// Implementa el patrón publish / subscriber. Tenemos dos agentes importantes:
/// - Publisher: se encarga de generar los eventos de dominio.
/// - Subscribers: agentes que escuchan los mensajes que envía el publisher, los capturan y disparará cierta lógica personalizada.
/// Por ejemplo:
/// - Cada vez que se dispare el registro de un nuevo usuario este evento se publicará en el Domain event.
/// - Posteriormente, un subscriber asignado, hará algo, como enviar un correo electrónico indicando que existe un nuevo usuario.
/// </summary>
public interface IDomainEvent : INotification
{

}