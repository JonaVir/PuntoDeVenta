using FluentValidation;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Create
{
    public class CreateSupplierValidator : AbstractValidator<CreateSupplierRequest>
    {
        public CreateSupplierValidator() 
        {
            RuleFor(x => x.TradeName).NotEmpty().WithMessage("Nombre comercial no puede ser nulo o vacio");
        }
    }
}
