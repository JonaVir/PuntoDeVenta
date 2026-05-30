using BospOne.PuntoDeVenta.Application.Core;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Update
{
    public sealed class UpdateSupplierCommandRequest : IRequest<Result<bool>>, ICommandBase
    {
        public readonly UpdateSupplierRequest Supplier;
        public readonly Guid SupplierID;
        public readonly string UserID;

        public UpdateSupplierCommandRequest(UpdateSupplierRequest supplier, Guid supplierID, string userID) 
        {
            Supplier = supplier;
            SupplierID =supplierID;
            UserID =userID;
        }
    }
}
