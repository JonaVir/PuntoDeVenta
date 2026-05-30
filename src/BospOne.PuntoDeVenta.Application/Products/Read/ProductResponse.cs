using BospOne.PuntoDeVenta.Domain.Entities;

namespace BospOne.PuntoDeVenta.Application.Products.Read
{
    public class ProductResponse
    {
        public Guid? Id { get; set; }
        public string? Code { get; set; }
        public string? DisplayName { get; set; }
        public List<Supplier>? suppliers { get; set; }
    }
}
