using FluentValidation;

namespace BospOne.PuntoDeVenta.Application.Users.UpdateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator() 
        {
            RuleFor(x => x.NombreCompleto).NotEmpty().WithMessage("El nombre es nulo");
            RuleFor(x => x.Email).NotEmpty().WithMessage("El Email no es correcto");
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Cargo).NotEmpty().WithMessage("El cargo es nulo");
            RuleFor(x => x.NewRole).NotEmpty().WithMessage("El rol es nulo");
        }
    }
}
