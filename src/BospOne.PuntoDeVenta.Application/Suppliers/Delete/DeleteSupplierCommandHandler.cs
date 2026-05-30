using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Delete
{
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommandRequest, Result<Unit>>
    {
        #region Fields and properties
        private readonly BospOneDbContext DbContext;
        #endregion

        #region Builders
        public DeleteSupplierCommandHandler(BospOneDbContext dbContext)
        {
            DbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task<Result<Unit>> Handle(DeleteSupplierCommandRequest request, CancellationToken cancellationToken)
        {
            var supplier = await DbContext.Suppliers!.FirstOrDefaultAsync(x => x.Id == request.SupplierID);

            if (supplier is null)
                return Result<Unit>.Failure("El proveedor no existe");

            DbContext.Suppliers!.Remove(supplier);

            var result = await DbContext.SaveChangesAsync(cancellationToken) > 0;

            return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Eror en la transacción");
        }

        #endregion
    }
}
