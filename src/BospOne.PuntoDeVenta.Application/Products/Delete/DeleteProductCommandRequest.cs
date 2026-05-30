using BospOne.PuntoDeVenta.Application.Core;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.Products.Delete
{
    public class DeleteProductCommandRequest : IRequest<Result<Unit>>
    {
        public readonly Guid ProductId;
        public DeleteProductCommandRequest(Guid productId) 
        {
            ProductId = productId;
        }
    }
}
