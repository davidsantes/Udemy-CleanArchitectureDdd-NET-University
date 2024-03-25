using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Infrastructure;

internal sealed class EmailService : IEmailService
{
    public Task SendAsync(Email recipient, string subject, string body)
    {
        //TODO: aquí debería implementarse la lógica de envío de correo electrónico.
        //Simulamos un envío del correo.
        return Task.CompletedTask;
    }
}