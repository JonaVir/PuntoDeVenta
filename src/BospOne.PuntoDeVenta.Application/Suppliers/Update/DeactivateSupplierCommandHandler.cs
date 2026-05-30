using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Update
{
    public class DeactivateSupplierCommandHandler : IRequestHandler<DeactivateSupplierCommandRequest, Result<Unit>>
    {
        #region Fields and properties
        private readonly BospOneDbContext DbContext;
        #endregion

        #region Builders
        public DeactivateSupplierCommandHandler(BospOneDbContext dbContext)
        {
            DbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task<Result<Unit>> Handle(DeactivateSupplierCommandRequest request, CancellationToken cancellationToken)
        {
            var supplier = await DbContext.Suppliers!.FirstOrDefaultAsync(x => x.Id == request.SupplierID);

            if (supplier is null)
                return Result<Unit>.Failure("El proveedor especificado no existe");

            supplier.IsActive = false;
            supplier.UpdateAt = DateTimeOffset.UtcNow.DateTime;
            supplier.UpdatedBy = request.UserID;

            DbContext.Entry(supplier).State = EntityState.Modified;
            
            var result = await DbContext.SaveChangesAsync(cancellationToken) > 0;

            return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Error al desactivar el proveedor");
        }
        #endregion
    }
}
