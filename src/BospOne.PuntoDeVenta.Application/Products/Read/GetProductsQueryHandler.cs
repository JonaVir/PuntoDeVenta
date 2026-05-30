using AutoMapper;
using AutoMapper.QueryableExtensions;
using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Application.Suppliers.Read;
using BospOne.PuntoDeVenta.Domain.Entities;
using BospOne.PuntoDeVenta.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BospOne.PuntoDeVenta.Application.Products.Read
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQueryRequest, Result<PagedList<ProductResponse>>>
    {
        #region Fields and properties
        private readonly BospOneDbContext Context;
        private readonly IMapper Mapper;
        #endregion

        #region Builders
        public GetProductsQueryHandler(BospOneDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        #endregion

        #region Methods
        public async Task<Result<PagedList<ProductResponse>>> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Product> queryable = Context.Products!
                .Include(s => s.Suppliers);

            var predicate = ExpressionBuilder.New<Product>();

            if (!string.IsNullOrEmpty(request.Product.Code))
                predicate = predicate.And(c => c.Code.ToLower().Contains(request.Product.Code.ToLower()));

            if (!string.IsNullOrEmpty(request.Product.DisplayName))
                predicate = predicate.And(d => d.DisplayName.ToLower().Contains(request.Product.DisplayName.ToLower()));

            if(!string.IsNullOrEmpty(request.Product.OrderBy))
            {
                Expression<Func<Product, object>>? orderBySelector =
                    request.Product.OrderBy!.ToLower() switch
                    {
                        "code" => product => product.Code!,
                        "displayName" => Product => Product.DisplayName!,
                        _ => Product => Product.Code!
                    };

                bool orderBy = request.Product.OrderAsc.HasValue ? request.Product.OrderAsc.Value : true;

                queryable = orderBy ? queryable.OrderBy(orderBySelector) : queryable.OrderByDescending(orderBySelector);
            }

            queryable = queryable.Where(predicate);

            var supplierQuery = queryable.ProjectTo<ProductResponse>(Mapper.ConfigurationProvider).AsQueryable();
            var paginantion = await PagedList<ProductResponse>.CreateAsync(supplierQuery, request.Product.PageNumber, request.Product.PageSize);

            return Result<PagedList<ProductResponse>>.Success(paginantion);
        }
        #endregion
    }
}
