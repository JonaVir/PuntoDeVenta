using FluentValidation;

namespace BospOne.PuntoDeVenta.Application.Products.Update
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductValidator() 
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code es requerido");
            RuleFor(x => x.DisplayName).NotEmpty().WithMessage("Nombre es requerido");
        }
    }
}
