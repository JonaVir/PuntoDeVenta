using BospOne.PuntoDeVenta.Application.Core;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Read
{
    public class SupplierRequest : PagingParams
    {
        public string? Code { get; set; }
        public string? TradeName { get; set; }
    }
}
