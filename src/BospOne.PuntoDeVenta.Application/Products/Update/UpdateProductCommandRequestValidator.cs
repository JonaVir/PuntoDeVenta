using FluentValidation;

namespace BospOne.PuntoDeVenta.Application.Products.Update
{
    public class UpdateProductCommandRequestValidator : AbstractValidator<UpdateProductCommandRequest>
    {
        public UpdateProductCommandRequestValidator() 
        {
            RuleFor(x => x.Product).SetValidator(new UpdateProductValidator());
        }
    }
}
