using BospOne.PuntoDeVenta.Application.Core;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Delete
{
    public class DeleteSupplierCommandRequest : IRequest<Result<Unit>>, ICommandBase
    {
        public readonly Guid SupplierID;

        public DeleteSupplierCommandRequest(Guid supplierID)
        {
            SupplierID = supplierID;
        }
    }
}
