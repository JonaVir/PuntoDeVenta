using FluentValidation;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Update
{
    public class UpdateSupplierValidator : AbstractValidator<UpdateSupplierRequest>
    {
        public UpdateSupplierValidator()
        {
            RuleFor(x => x.TradeName).NotEmpty().WithMessage("El nombre comercial no puede ser nulo");
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
