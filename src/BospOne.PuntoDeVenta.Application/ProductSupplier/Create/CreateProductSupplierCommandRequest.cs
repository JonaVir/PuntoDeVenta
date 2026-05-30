using BospOne.PuntoDeVenta.Application.Core;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.ProductSupplier.Create 
{
    public class CreateProductSupplierCommandRequest : IRequest<Result<Unit>> 
    {
        public readonly ProductSupplierRequest ProductSupplier;
        public CreateProductSupplierCommandRequest(ProductSupplierRequest productSupplier) 
        {
            ProductSupplier = productSupplier;
        }
    }
}
