using BospOne.PuntoDeVenta.Application.Core;

namespace BospOne.PuntoDeVenta.Application.Products.Read
{
    public class ProductRequest : PagingParams
    {
        public string? Code { get; set; }
        public string? DisplayName { get; set; }
    }
}
