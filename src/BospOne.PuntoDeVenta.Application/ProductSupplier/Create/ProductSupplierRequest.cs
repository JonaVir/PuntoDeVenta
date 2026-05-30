namespace BospOne.PuntoDeVenta.Application.ProductSupplier.Create 
{
    public class ProductSupplierRequest 
    {
        public Guid SupplierID { get; set; }
        public List<Guid> Products { get; set; }
    }
}
