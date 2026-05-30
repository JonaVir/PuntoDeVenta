using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BospOne.PuntoDeVenta.Application.Products.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, Result<Guid>>
    {
        #region Fields and properties
        private readonly BospOneDbContext DbContext;
        #endregion

        #region Builders
        public UpdateProductCommandHandler(BospOneDbContext dbContext)
        {
            DbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task<Result<Guid>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await DbContext.Products!.FirstOrDefaultAsync(x => x.Id == request.ProductID);

            if (product is null)
                return Result<Guid>.Failure("El producto no existe");

            product.Code = request.Product.Code;
            product.DisplayName = request.Product.DisplayName;
            product.UpdateAt = DateTime.Now;
            product.UpdatedBy = request.UserID;

            DbContext.Entry(product).State = EntityState.Modified;

            var result = await DbContext.SaveChangesAsync(cancellationToken) > 0;

            return result ? Result<Guid>.Success(product.Id) : Result<Guid>.Failure("Error al modificar producto");
        }
        #endregion
    }
}
