namespace CleanArchitecture.Application.Users.RegisterUser;

public record RegisterUserRequest(string Email, string Nombre, string Apellidos, string Password);
