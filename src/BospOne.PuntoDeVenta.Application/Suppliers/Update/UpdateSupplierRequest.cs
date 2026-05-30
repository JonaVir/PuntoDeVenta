namespace BospOne.PuntoDeVenta.Application.Suppliers.Update
{
    public class UpdateSupplierRequest
    {
        public string TradeName { get; set; } = string.Empty;
        public string? NumberPhone { get; set; }
        public string? Email { get; set; }
    }
}
