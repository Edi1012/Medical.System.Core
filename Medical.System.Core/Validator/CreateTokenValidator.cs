using FluentValidation;
using Medical.System.Core.Models.DTOs;

namespace Medical.System.Core.Validator;

public class CreateTokenValidator : AbstractValidator<LoginDTO>
{
    public CreateTokenValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("El nombre de usuario no puede estar vacío.");

        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage("La contraseña no puede estar vacía.");
    }
}
