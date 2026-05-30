using BospOne.PuntoDeVenta.Application.Core;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Read
{
    public class GetSuppliersQueryRequest : IRequest<Result<PagedList<SupplierResponse>>>
    {
        public readonly SupplierRequest Supplier;
        
        public GetSuppliersQueryRequest(SupplierRequest supplier)
        {
            Supplier = supplier;
        }
    }
}
