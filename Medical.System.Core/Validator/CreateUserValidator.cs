using FluentValidation;
using FluentValidation.Results;
using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Repositories;
using Medical.System.Core.Repositories.Interfaces;
using Medical.System.Core.Services.Interfaces;

namespace Medical.System.Core.Validator;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{

    public CreateUserValidator(IUserRepository userRepository)
    {
        UserRepository = userRepository;

        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("El nombre de usuario es requerido")
            .MaximumLength(50).WithMessage("La longitud máxima del nombre de usuario es 50 caracteres")
            .MustAsync(UserNameDoesNotExist).WithMessage("El nombre de usuario ya existe");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("El password del usuario es requerido");
    }

    public IUserRepository UserRepository { get; }

    private async Task<bool> UserNameDoesNotExist(string userName, CancellationToken cancellationToken)
    {
        bool exists = await UserRepository.ExistUserNameAsync(userName);
        return !exists;
    }
}
