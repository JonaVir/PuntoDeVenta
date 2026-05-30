using BospOne.PuntoDeVenta.Application.Core;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Update
{
    public class DeactivateSupplierCommandRequest : IRequest<Result<Unit>>, ICommandBase
    {
        public readonly Guid SupplierID;
        public readonly string UserID;

        public DeactivateSupplierCommandRequest(Guid supplierID, string userID)
        {
            SupplierID = supplierID;
            UserID = userID;
        }
    }
}
