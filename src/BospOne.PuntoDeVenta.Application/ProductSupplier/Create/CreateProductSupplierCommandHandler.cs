using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Domain;
using BospOne.PuntoDeVenta.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BospOne.PuntoDeVenta.Application.ProductSupplier.Create 
{
    public class CreateProductSupplierCommandHandler : IRequestHandler<CreateProductSupplierCommandRequest, Result<Unit>> 
    {
        #region Fields and properties
        private readonly BospOneDbContext Context;
        #endregion

        #region Builders
        public CreateProductSupplierCommandHandler(BospOneDbContext context) 
        {
            Context = context;
        }
        #endregion

        #region Methods
        public async Task<Result<Unit>> Handle(CreateProductSupplierCommandRequest request, CancellationToken cancellationToken) 
        {
            var supplier = await Context.Suppliers!
                .Include(s => s.ProductSuppliers)
                .FirstOrDefaultAsync(s => s.Id == request.ProductSupplier.SupplierID, cancellationToken);

            if(supplier is null)            
                return Result<Unit>.Failure("El proveedor especificado no existe");

            var products = await Context.Products!
                .Where(p => request.ProductSupplier.Products.Contains(p.Id))
                .ToListAsync(cancellationToken);

            if (!products.Any())
                return Result<Unit>.Failure("Productos invalidos");

            var existingProds = supplier.ProductSuppliers!
                .Select(x => x.ProductID)
                .ToHashSet();

            var relations = products
                .Where(p => !existingProds.Contains(p.Id))
                .Select(p => new Domain.Entities.ProductSupplier 
                {
                    ProductID = p.Id,
                    SupplierID = supplier.Id
                });

            await Context.ProductSuppliers.AddRangeAsync(relations, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
        #endregion
    }
}
