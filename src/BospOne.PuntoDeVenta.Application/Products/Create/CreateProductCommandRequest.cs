using BospOne.PuntoDeVenta.Application.Core;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.Products.Create
{
    public class CreateProductCommandRequest : IRequest<Result<Guid>>, ICommandBase
    {
        public readonly CreateProductRequest Product;
        public readonly string UserId;

        public CreateProductCommandRequest(CreateProductRequest product, string userID)
        {
            Product = product;
            UserId = userID;
        }
    }
}
