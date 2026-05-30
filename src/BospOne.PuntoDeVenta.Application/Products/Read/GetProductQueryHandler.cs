using AutoMapper;
using AutoMapper.QueryableExtensions;
using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BospOne.PuntoDeVenta.Application.Products.Read
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQueryRequest, Result<ProductResponse>>
    {
        #region Fields and properties
        private readonly BospOneDbContext Context;
        private readonly IMapper Mapper;
        #endregion

        #region Builders
        public GetProductQueryHandler(BospOneDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        #endregion

        #region Methods
        public async Task<Result<ProductResponse>> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await Context.Products!
                .Where(x => x.Id == request.ProductID)
                .ProjectTo<ProductResponse>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return Result<ProductResponse>.Success(product!);

        }
        #endregion
    }
}
