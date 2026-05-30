using FluentValidation;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Update
{
    public class UpdateSupplierCommandRequestValidator : AbstractValidator<UpdateSupplierCommandRequest>
    {
        public UpdateSupplierCommandRequestValidator() 
        {
            RuleFor(x => x.Supplier).SetValidator(new UpdateSupplierValidator());
        }
    }
}
