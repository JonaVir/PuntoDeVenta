using BospOne.PuntoDeVenta.Application.Core;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Read
{
    public class GetSupplierQueryRequest : IRequest<Result<SupplierResponse>>
    {
        public readonly Guid SupplierID;

        public GetSupplierQueryRequest(Guid supplierID)
        {
            SupplierID = supplierID;
        }
    }
}
