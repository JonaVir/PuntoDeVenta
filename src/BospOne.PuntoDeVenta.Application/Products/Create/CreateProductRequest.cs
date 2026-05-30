namespace BospOne.PuntoDeVenta.Application.Products.Create
{
    public class CreateProductRequest
    {
        public required string Code { get; set; }
        public required string DisplayName { get; set; }
    }
}
