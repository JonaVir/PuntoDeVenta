namespace BospOne.PuntoDeVenta.Application.Suppliers.Create
{
    public class CreateSupplierRequest
    {
        public string TradeName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
