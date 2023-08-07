using FluentValidation;
using FluentValidation.Results;
using Medical.System.Core.DTOs;
using Medical.System.Core.Services.Interfaces;

namespace Medical.System.Core.Validator.Catalogs.User;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    private readonly ICatalogsService CatalogsService;

    public CreateUserValidator(ICatalogsService CatalogsService)
    {
        this.CatalogsService = CatalogsService;
        this.ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("El nombre de usuario es requerido")
            .MaximumLength(50).WithMessage("No se puede repetir el usuario")
            .MustAsync(async (userName, cancellation) =>
             {
                 bool exists = await CatalogsService.ExistUserNameAsync(userName);
                 return !exists;
             }).WithMessage("El nombre de usuario ya existe");
    }

}
