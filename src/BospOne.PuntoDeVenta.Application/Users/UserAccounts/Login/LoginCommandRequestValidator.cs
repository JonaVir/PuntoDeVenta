using FluentValidation;

namespace BospOne.PuntoDeVenta.Application.Users.UserAccounts.Login
{
    public class LoginCommandRequestValidator : AbstractValidator<LoginCommandRequest>
    {
        public LoginCommandRequestValidator()
        {
            RuleFor(x => x.Login).SetValidator(new LoginValidator());
        }
    }
}
