using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Update
{
    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommandRequest, Result<bool>>
    {
        #region Fields and properties
        private readonly BospOneDbContext DbContext;
        #endregion

        #region Builders
        public UpdateSupplierCommandHandler(BospOneDbContext dbContext)
        {
            DbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task<Result<bool>> Handle(UpdateSupplierCommandRequest request, CancellationToken cancellationToken)
        {
            var supplier = await DbContext.Suppliers!.FirstOrDefaultAsync(s => s.Id == request.SupplierID);

            if(supplier is null)
                return Result<bool>.Failure("el proveedor especificado no existe");

            supplier.TradeName = request.Supplier.TradeName;
            supplier.Phone = request.Supplier.NumberPhone;
            supplier.Email = request.Supplier.Email;
            supplier.UpdateAt = DateTime.Now;
            supplier.UpdatedBy = request.UserID;

            DbContext.Entry(supplier).State = EntityState.Modified;

            var result = await DbContext.SaveChangesAsync(cancellationToken) > 0;

            return result ? Result<bool>.Success(true) : Result<bool>.Failure("No se pudo guardar los cambios");              
        }

        #endregion
    }
}
