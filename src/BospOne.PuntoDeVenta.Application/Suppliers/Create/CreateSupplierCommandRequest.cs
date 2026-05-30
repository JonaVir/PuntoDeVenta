using BospOne.PuntoDeVenta.Application.Core;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Create
{
    public record CreateSupplierCommandRequest : IRequest<Result<Guid>>, ICommandBase
    {
        public readonly CreateSupplierRequest supplier;
        public readonly string UserID;     
        public CreateSupplierCommandRequest(CreateSupplierRequest request, string userID)
        {
            supplier = request;
            UserID = userID;
        }
    }

}
