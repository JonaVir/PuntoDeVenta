using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BospOne.PuntoDeVenta.Application.Products.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, Result<Unit>>
    {
        #region Fields and properties
        private readonly BospOneDbContext DbContext;
        #endregion

        #region Builders
        public DeleteProductCommandHandler(BospOneDbContext dbContext)
        {
            DbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task<Result<Unit>> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await DbContext.Products!.Include(x => x.ProductSuppliers).FirstOrDefaultAsync(x => x.Id == request.ProductId);

            if (product is null)
                return Result<Unit>.Failure("El producto no existe");

            DbContext.Products!.Remove(product);

            var result = await DbContext.SaveChangesAsync(cancellationToken) > 0;

            return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Error interno");
        }
        #endregion
    }
}
