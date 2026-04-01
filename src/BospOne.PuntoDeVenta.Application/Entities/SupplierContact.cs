using BospOne.PuntoDeVenta.Application.Interfaces;

namespace BospOne.PuntoDeVenta.Application.Entities
{
    public class SupplierContact : EntityBase, IAuditory
    {
        public Guid SupplierID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? JobPosition { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
