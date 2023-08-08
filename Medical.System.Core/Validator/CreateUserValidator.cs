using FluentValidation;
using FluentValidation.Results;
using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Services.Interfaces;

namespace Medical.System.Core.Validator;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    private readonly ICatalogsService CatalogsService;

    public CreateUserValidator(ICatalogsService CatalogsService)
    {
        this.CatalogsService = CatalogsService;
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("El nombre de usuario es requerido")
            .MaximumLength(50).WithMessage("La longitud máxima del nombre de usuario es 50 caracteres")
            .MustAsync(UserNameDoesNotExist).WithMessage("El nombre de usuario ya existe");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("El password del usuario es requerido");
    }

    private async Task<bool> UserNameDoesNotExist(string userName, CancellationToken cancellationToken)
    {
        bool exists = await CatalogsService.ExistUserNameAsync(userName);
        return !exists;
    }
}
