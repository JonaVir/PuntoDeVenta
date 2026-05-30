using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Application.Interfaces;
using BospOne.PuntoDeVenta.Domain.Entities;
using BospOne.PuntoDeVenta.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BospOne.PuntoDeVenta.Application.Products.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, Result<Guid>>
    {
        #region Fields and properties
        private readonly BospOneDbContext DbContext;
        #endregion

        #region Builders
        public CreateProductCommandHandler(BospOneDbContext dbContext)
        {
            DbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task<Result<Guid>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var productID = Guid.NewGuid();
            var product = new Product
            {
                Id = productID,
                Code = request.Product.Code,
                DisplayName = request.Product.DisplayName,
                CreateAt = DateTime.Now,
                CreatedBy = request.UserId
            };

            DbContext.Add(product);

            var result = await DbContext.SaveChangesAsync(cancellationToken) > 0;

            return result ? Result<Guid>.Success(productID) : Result<Guid>.Failure("Error al guardar producto");
        }

        #endregion
    }
}
