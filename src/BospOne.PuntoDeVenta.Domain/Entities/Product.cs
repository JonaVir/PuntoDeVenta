using BospOne.PuntoDeVenta.Domain.Interfaces;

namespace BospOne.PuntoDeVenta.Domain.Entities
{
    public class Product : EntityBase, IAuditory
    {
        public string Code { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;     
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public ICollection<Supplier>? Suppliers { get; set; }
        public ICollection<ProductSupplier>? ProductSuppliers { get; set; }
        public ICollection<ProductsReceived>? productsReceiveds { get; set; }
    }
}
