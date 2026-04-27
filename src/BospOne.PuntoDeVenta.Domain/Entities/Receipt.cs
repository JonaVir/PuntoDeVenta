using BospOne.PuntoDeVenta.Domain.Interfaces;

namespace BospOne.PuntoDeVenta.Domain.Entities
{
    public class Receipt : EntityBase, IAuditory
    {
        public string? UserID { get; set; }
        public Guid ProductID { get; set; }
        public Guid SupplierID { get; set; }
        public decimal AmountReceived { get; set; }
        public decimal RefundAmount { get; set; }
        public string? SupplierSignature { get; set; } 
        public string? Comments { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public Product? Product { get; set; }
        public Supplier? Supplier { get; set; }
        public ICollection<ReceiptEvidence>? ReceiveEvidences { get; set; }
    }
}
