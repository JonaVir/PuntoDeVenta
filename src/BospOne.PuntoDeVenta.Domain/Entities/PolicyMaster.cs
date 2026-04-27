namespace BospOne.PuntoDeVenta.Domain.Entities
{
    public class PolicyMaster
    {
        public const string SUPPLIER_READ = nameof(SUPPLIER_READ);
        public const string SUPPLIER_WRITE = nameof(SUPPLIER_WRITE);
        public const string SUPPLIER_UPDATE = nameof(SUPPLIER_UPDATE);
        public const string SUPPLIER_DELETE = nameof(SUPPLIER_DELETE);

        public const string PRODUCT_READ = nameof(PRODUCT_READ);
        public const string PRODUCT_WRITE = nameof(PRODUCT_WRITE);
        public const string PRODUCT_UPDATE = nameof(PRODUCT_UPDATE);
        public const string PRODUCT_DELETE = nameof(PRODUCT_DELETE);

        public const string RECEIPT_READ = nameof(RECEIPT_READ);
        public const string RECEIPT_WRITE = nameof(RECEIPT_WRITE);
    }
}
