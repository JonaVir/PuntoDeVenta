namespace BospOne.PuntoDeVenta.Domain.Entities
{
    public class ProductSupplier
    {
        public Guid ProductID { get; set; }
        public Guid SupplierID { get; set; }

        public Product? Product { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
