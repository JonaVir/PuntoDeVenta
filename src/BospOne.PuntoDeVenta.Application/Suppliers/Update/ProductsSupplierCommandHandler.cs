using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Update
{
    public class ProductsSupplierCommandHandler : IRequestHandler<ProductsSupplierCommandRequest, Result<bool>>
    {
        #region Fields and properties
        private readonly BospOneDbContext DbContext;
        #endregion

        #region Builders
        public ProductsSupplierCommandHandler(BospOneDbContext dbContext)
        {
            DbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task<Result<bool>> Handle(ProductsSupplierCommandRequest request, CancellationToken cancellationToken)
        {
            var supplier = await DbContext.Suppliers!
                .Include(p => p.ProductSuppliers)
                .FirstOrDefaultAsync(x => x.Id == request.SupplierID);

            if (supplier is null)
                return Result<bool>.Failure("Proveedor no encontrado");

            var products = await DbContext.Products!
                    .Where(x => request.Products.Contains(x.Id))
                    .ToListAsync();

            return Result<bool>.Success(true);

        }

        #endregion
    }
}
