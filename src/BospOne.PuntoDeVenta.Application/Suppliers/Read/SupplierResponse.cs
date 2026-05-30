using BospOne.PuntoDeVenta.Domain.Entities;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Read
{
    public class SupplierResponse
    {
        public Guid? Id { get; set; }
        public string? Code { get; set; }
        public string? TradeName { get; set; }
        public string? Phone {  get; set; }
        public string? Email { get; set; }
        public List<Product>? Products { get; set; }
    }
}
