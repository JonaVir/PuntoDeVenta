using BospOne.PuntoDeVenta.Application.Core;
using MediatR;


namespace BospOne.PuntoDeVenta.Application.Products.Read
{
    public class GetProductsQueryRequest : IRequest<Result<PagedList<ProductResponse>>>
    {
        public readonly ProductRequest Product;

        public GetProductsQueryRequest(ProductRequest product)
        {
            Product = product;
        }
    }
}
