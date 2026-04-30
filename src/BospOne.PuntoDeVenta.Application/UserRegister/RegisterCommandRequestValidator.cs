using FluentValidation;

namespace BospOne.PuntoDeVenta.Application.UserRegister
{
    public class RegisterCommandRequestValidator : AbstractValidator<RegisterCommandRequest>
    {
        public RegisterCommandRequestValidator()
        {
            RuleFor(x => x.Register).SetValidator(new RegisterValidator());
        }
    }
}
