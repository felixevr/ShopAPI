using FluentValidation;
using Tienda.Application.Dtos.Request;

namespace Tienda.Application.Validators
{
    public class UserValidator : AbstractValidator<UserRequestDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name)
                    .NotNull().WithMessage("El campo nombre no puede ser nulo")
                    .NotEmpty().WithMessage("El campo nombre no puede ser vacío");
        }
    }
}
