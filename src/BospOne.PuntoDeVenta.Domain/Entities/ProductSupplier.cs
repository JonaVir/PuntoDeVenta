namespace BospOne.PuntoDeVenta.Domain.Entities
{
    public class ProductSupplier
    {
        public Guid productID { get; set; }
        public Guid SupplierID { get; set; }

        public Product? product { get; set; }
        public Supplier? supplier { get; set; }
    }
}
