using FluentValidation;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Create
{
    public class CreateSupplierCommandRequestValidator : AbstractValidator<CreateSupplierCommandRequest>
    {
        public CreateSupplierCommandRequestValidator() 
        {
            RuleFor(x => x.supplier).SetValidator(new CreateSupplierValidator());
        }
    }
}
