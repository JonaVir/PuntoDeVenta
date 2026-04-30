using FluentValidation;

namespace BospOne.PuntoDeVenta.Application.UserRegister
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator() 
        {
            RuleFor(x => x.NombreCompleto).NotEmpty().WithMessage("El nombre completo es requerido.");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("El nombre de usuario es requerido.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("El correo electrónico es requerido.").EmailAddress().WithMessage("El correo electrónico no es válido.");
            RuleFor(x => x.Cargo).NotEmpty().WithMessage("El cargo es requerido.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("La contraseña es requerida.").MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");
        }
    }
}
