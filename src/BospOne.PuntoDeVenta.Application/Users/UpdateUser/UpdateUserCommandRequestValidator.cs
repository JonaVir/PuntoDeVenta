using FluentValidation;

namespace BospOne.PuntoDeVenta.Application.Users.UpdateUser
{
    public class UpdateUserCommandRequestValidator : AbstractValidator<UpdateUserCommandRequest>
    {
        public UpdateUserCommandRequestValidator() 
        {
            RuleFor(x => x.user).SetValidator(new UpdateUserValidator());
            RuleFor(x => x.UserId).NotNull();
        }
    }
}
