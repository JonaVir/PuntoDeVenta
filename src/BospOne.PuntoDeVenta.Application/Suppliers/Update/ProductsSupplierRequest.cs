using BospOne.PuntoDeVenta.Domain.Entities;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Update
{
    public class ProductsSupplierRequest
    {
        public required Guid SupplierID { get; set; }
        public required List<Guid> Products { get; set; }
    }
}
