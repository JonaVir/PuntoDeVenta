using AutoMapper;
using AutoMapper.QueryableExtensions;
using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Read
{
    public class GetSupplierQueryHandler : IRequestHandler<GetSupplierQueryRequest, Result<SupplierResponse>>
    {
        #region Fields and properties
        private readonly BospOneDbContext DbContext;
        private readonly IMapper Mapper;
        #endregion

        #region Builders
        public GetSupplierQueryHandler(BospOneDbContext context, IMapper mapper)
        {
            DbContext = context;
            Mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<Result<SupplierResponse>> Handle(GetSupplierQueryRequest request, CancellationToken cancellationToken)
        {
            var supplier = await DbContext.Suppliers!.Where(x => x.Id == request.SupplierID)
                .Include(x => x.Products)
                .ProjectTo<SupplierResponse>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return Result<SupplierResponse>.Success(supplier!);
        }
        #endregion
    }
}
