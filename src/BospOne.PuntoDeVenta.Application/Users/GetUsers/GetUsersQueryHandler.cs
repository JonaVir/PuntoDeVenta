using AutoMapper;
using AutoMapper.QueryableExtensions;
using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Persistence;
using BospOne.PuntoDeVenta.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BospOne.PuntoDeVenta.Application.Users.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQueryRequest, Result<PagedList<UserResponse>>>
    {
        #region Fields and properties
        private readonly BospOneDbContext Context;
        private readonly IMapper Mapper;
        private readonly UserManager<AppUser> UserManager;
        #endregion

        #region Builders
        public GetUsersQueryHandler(BospOneDbContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            Context = context;
            Mapper = mapper;
            UserManager = userManager;
        }
        #endregion

        #region Methods
        public async Task<Result<PagedList<UserResponse>>> Handle(GetUsersQueryRequest request, CancellationToken cancellationToken)
        {
            IQueryable<AppUser> queryable = UserManager.Users!;

            var predicate = ExpressionBuilder.New<AppUser>();

            if (!string.IsNullOrEmpty(request.User.Name))
                predicate = predicate.And(x => x.NombreCompleto!.ToLower().Contains(request.User.Name.ToLower()));

            if (!string.IsNullOrEmpty(request.User.UserName))
                predicate = predicate.And(x => x.UserName!.ToLower().Contains(request.User.UserName.ToLower()));

            if (!string.IsNullOrEmpty(request.User.Email))
                predicate = predicate.And(x => x.Email!.ToLower().Contains(request.User.Email.ToLower()));

            if(!string.IsNullOrEmpty(request.User.OrderBy))
            {
                Expression<Func<AppUser, string>>? orderBySelector =
                    request.User.OrderBy!.ToLower() switch
                    {
                        "name" => User => User.NombreCompleto!,
                        "username" => user => user.UserName!,
                        "email" => user => user.Email!,
                        _ => user => user.UserName!
                    };

                bool orderBy = request.User.OrderAsc.HasValue ? request.User.OrderAsc.Value : true;
                queryable = orderBy ? queryable.OrderBy(orderBySelector) : queryable.OrderByDescending(orderBySelector);
            }

            queryable = queryable.Where(predicate);

            var userQuery = queryable.ProjectTo<UserResponse>(Mapper.ConfigurationProvider).AsQueryable();
            var pagination = await PagedList<UserResponse>.CreateAsync(userQuery,request.User.PageNumber,request.User.PageSize);

            return Result<PagedList<UserResponse>>.Success(pagination);
        }
        #endregion
    }
}
