using FluentValidation;

namespace BospOne.PuntoDeVenta.Application.Products.Create
{
    public class CreateProductValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductValidator() 
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code es requerido");
            RuleFor(x => x.DisplayName).NotEmpty().WithMessage("DisplayName es requerido");
        }
    }
}
