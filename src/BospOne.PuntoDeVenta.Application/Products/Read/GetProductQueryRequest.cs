using BospOne.PuntoDeVenta.Application.Core;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.Products.Read
{
    public class GetProductQueryRequest : IRequest<Result<ProductResponse>>
    {
        public readonly Guid ProductID;

        public GetProductQueryRequest(Guid productID)
        {
            ProductID = productID;
        }
    }
}
