using BospOne.PuntoDeVenta.Application.Core;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.Products.Update
{
    public class UpdateProductCommandRequest : IRequest<Result<Guid>>
    {
        public readonly UpdateProductRequest Product;
        public readonly string UserID;
        public readonly Guid ProductID;

        public UpdateProductCommandRequest(UpdateProductRequest product, string userID, Guid productID)
        {
            Product = product;
            UserID = userID;
            ProductID = productID;
        }
    }
}
