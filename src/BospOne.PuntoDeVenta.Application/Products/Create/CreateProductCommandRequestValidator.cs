using FluentValidation;

namespace BospOne.PuntoDeVenta.Application.Products.Create
{
    public class CreateProductCommandRequestValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandRequestValidator() 
        {
            RuleFor(x => x.Product).SetValidator(new CreateProductValidator());
        }
    }
}
