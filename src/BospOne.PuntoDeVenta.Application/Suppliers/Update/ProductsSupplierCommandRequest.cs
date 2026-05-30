using BospOne.PuntoDeVenta.Application.Core;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Update
{
    public class ProductsSupplierCommandRequest : IRequest<Result<bool>>, ICommandBase
    {
        public readonly List<Guid> Products;
        public readonly Guid SupplierID;

        public ProductsSupplierCommandRequest(List<Guid> products, Guid supplierID)
        {
            Products = products;
            SupplierID = supplierID;
        }
    }
}
