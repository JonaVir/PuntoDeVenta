using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Application.Interfaces;
using BospOne.PuntoDeVenta.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BospOne.PuntoDeVenta.Application.UserAccounts.Query
{
    public class UserQueryHandler : IRequestHandler<UserQueryRequest, Result<Profile>>
    {
        #region Fields and properties
        private readonly UserManager<AppUser> UserManager;
        private readonly ITokenService TokenService;
        #endregion

        #region Builders
        public UserQueryHandler(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            UserManager = userManager;
            TokenService = tokenService;
        }
        #endregion

        #region Methods
        public async Task<Result<Profile>> Handle(UserQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await UserManager.Users.FirstOrDefaultAsync(x => x.Email == request.User.Email, cancellationToken);

            if (user is null)
                return Result<Profile>.Failure("No se encontro el usuario");

            var profile = new Profile
            {
                Email = user.Email,
                NombreCompleto = user.NombreCompleto,
                Token = await TokenService.CreateToken(user),
                UserName = user.UserName
            };

            return Result<Profile>.Success(profile);
        }
        #endregion
    }
}
