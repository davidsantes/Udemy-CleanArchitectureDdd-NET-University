using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.Users.RegisterUser;

internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken
    )
    {
        //1. Validar que el usuario no exista en la base de datos
        var email = new Email(request.Email);
        var userExists = await _userRepository.IsUserExists(email, cancellationToken);
        if (userExists)
        {
            return Result.Failure<Guid>(UserErrors.AlreadyExists);
        }

        //2. Encriptar el password plano del usuario que envió el cliente
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        //3. Crear un objeto de tipo user
        var user = User.Create(
            new Nombre(request.Nombre),
            new Apellido(request.Apellidos),
            new Email(request.Email),
            new PasswordHash(passwordHash)
        );

        //4. Insertar el usuario a la base de datos
        _userRepository.Add(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id!.Value;
    }
}
