using FluentValidation;
using Tienda.Application.Dtos.Request;

namespace Tienda.Application.Validators
{
    public class CartValidator : AbstractValidator<CartRequestDto>
    {
        public CartValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull().WithMessage("El Usuario no puede ser nulo. Debe indicar el UserId")
                .NotEmpty().WithMessage("El Usuario no puede ser vacío. Debe indicar el UserId");
        }
    }
}
