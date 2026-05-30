using AutoMapper;
using AutoMapper.QueryableExtensions;
using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Domain.Entities;
using BospOne.PuntoDeVenta.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Read
{
    public class GetSuppliersQueryHandler : IRequestHandler<GetSuppliersQueryRequest, Result<PagedList<SupplierResponse>>>
    {
        #region Fields and properties
        private readonly BospOneDbContext Context;
        private readonly IMapper Mapper;
        #endregion

        #region Builders
        public GetSuppliersQueryHandler(BospOneDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<Result<PagedList<SupplierResponse>>> Handle(GetSuppliersQueryRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Supplier> queryable = Context.Suppliers!
                .Include(x => x.Products);

            var predicate = ExpressionBuilder.New<Supplier>();

            if(!string.IsNullOrEmpty(request.Supplier!.Code))
                predicate = predicate.And(y => y.Code!.ToLower().Contains(request.Supplier.Code.ToLower()));

            if(!string.IsNullOrEmpty(request.Supplier!.TradeName))
                predicate = predicate.And(y => y.TradeName.ToLower().Contains(request.Supplier.TradeName.ToLower()));

            if(!string.IsNullOrEmpty(request.Supplier.OrderBy))
            {
                Expression<Func<Supplier, object>>? orderBySelector =
                    request.Supplier.OrderBy!.ToLower() switch
                    {
                        "code" => Supplier => Supplier.Code!,
                        "tradename" => Supplier => Supplier.TradeName!,
                        _ => Supplier => Supplier.Code!
                    };

                bool orderBy = request.Supplier.OrderAsc.HasValue ? request.Supplier.OrderAsc.Value : true;

                queryable = orderBy ? queryable.OrderBy(orderBySelector) : queryable.OrderByDescending(orderBySelector);
            }

            queryable = queryable.Where(predicate);

            var supplierQuery = queryable.ProjectTo<SupplierResponse>(Mapper.ConfigurationProvider).AsQueryable();
            var paginantion = await PagedList<SupplierResponse>.CreateAsync(supplierQuery,request.Supplier.PageNumber,request.Supplier.PageSize);

            return Result<PagedList<SupplierResponse>>.Success(paginantion);
        }

        #endregion
    }
}
