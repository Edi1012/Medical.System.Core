using FluentValidation;
using Medical.System.Core.Enums;
using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Repositories.Interfaces;

namespace Medical.System.Core.Validator;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{

    public CreateUserValidator(IUserRepository userRepository)
    {
        UserRepository = userRepository;

        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Login.Username)
            .NotEmpty().WithMessage("El nombre de usuario es requerido")
            .MaximumLength(50).WithMessage("La longitud máxima del nombre de usuario es 50 caracteres")
            .MustAsync(UserNameDoesNotExist).WithMessage("El nombre de usuario ya existe");

        RuleFor(x => x.Login.PasswordHash)
            .NotEmpty().WithMessage("El password del usuario es requerido");

        RuleFor(x => x.Roles)
            .NotEmpty().WithMessage("El usuario debe tener al menos un rol")
            .Must(x => x.All(role => UserRoleConstants.GetRoles().Contains(role))).WithMessage("El rol no existe");
    }

    public IUserRepository UserRepository { get; }

    private async Task<bool> UserNameDoesNotExist(string userName, CancellationToken cancellationToken)
    {
        bool exists = await UserRepository.ExistUserNameAsync(userName);
        return !exists;
    }
}
