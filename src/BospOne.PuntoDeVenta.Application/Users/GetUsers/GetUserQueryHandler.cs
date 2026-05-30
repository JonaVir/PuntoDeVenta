using AutoMapper;
using AutoMapper.QueryableExtensions;
using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Persistence;
using BospOne.PuntoDeVenta.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BospOne.PuntoDeVenta.Application.Users.GetUsers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQueryRequest, Result<UserResponse>>
    {
        #region Fields and properties
        private readonly UserManager<AppUser> Context;
        private readonly IMapper Mapper;
        #endregion

        #region Builders
        public GetUserQueryHandler(UserManager<AppUser> context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<Result<UserResponse>> Handle(GetUserQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await Context.Users
                .Where(x => x.Id == request.UserID!)
                .ProjectTo<UserResponse>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return Result<UserResponse>.Success(user!);
        }
        #endregion
    }
}
