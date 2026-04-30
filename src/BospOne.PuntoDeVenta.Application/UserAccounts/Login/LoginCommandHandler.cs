using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Application.Interfaces;
using BospOne.PuntoDeVenta.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BospOne.PuntoDeVenta.Application.UserAccounts.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, Result<Profile>>
    {
        #region Fields and properties
        private readonly UserManager<AppUser> UserManager;
        private readonly ITokenService TokenService;
        #endregion

        #region Builders
        public LoginCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            UserManager = userManager;
            TokenService = tokenService;
        }
        #endregion

        #region Methods
        public async Task<Result<Profile>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await UserManager.Users.FirstOrDefaultAsync(x => x.Email == request.Login.Email);

            if (user is null)
                return Result<Profile>.Failure("No se encontro el usuario");

            var result = await UserManager.CheckPasswordAsync(user, request.Login.Password!);

            if(!result)
                return Result<Profile>.Failure("Contraseña incorrecta");

            var profile = new Profile
            {
                Email = user.Email,
                NombreCompleto = user.NombreCompleto,
                UserName = user.UserName,
                Token = await TokenService.CreateToken(user)
            };

            return Result<Profile>.Success(profile);
        }
        #endregion
    }
}
