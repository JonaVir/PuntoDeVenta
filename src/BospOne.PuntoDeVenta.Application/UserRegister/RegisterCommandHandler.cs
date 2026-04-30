using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Application.Interfaces;
using BospOne.PuntoDeVenta.Application.UserAccounts;
using BospOne.PuntoDeVenta.Domain.Entities;
using BospOne.PuntoDeVenta.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BospOne.PuntoDeVenta.Application.UserRegister
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, Result<Profile>>
    {
        #region Fields and properties
        private readonly UserManager<AppUser> UserManager;
        private readonly ITokenService TokenService;
        #endregion

        #region Builders
        public RegisterCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            UserManager = userManager;
            TokenService = tokenService;
        }
        #endregion

        #region Methods
        public async Task<Result<Profile>> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            if (await UserManager.Users.AnyAsync(x => x.Email == request.Register.Email))
                return Result<Profile>.Failure("Este email ya fue registrado por otro usuario");

            if (await UserManager.Users.AnyAsync(x => x.UserName == request.Register.UserName))
                return Result<Profile>.Failure("El nombre de usuario ya fue registrado");

            var user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                NombreCompleto = request.Register.NombreCompleto,
                Email = request.Register.Email,
                Cargo = request.Register.Cargo,
                UserName = request.Register.UserName
            };

            var result = await UserManager.CreateAsync(user, request.Register.Password!);

            if(result.Succeeded)
            {
                if(request.Register.Cargo!.ToLower() == "admin")
                    await UserManager.AddToRoleAsync(user, CustomRoles.ADMIN);
                else
                    await UserManager.AddToRoleAsync(user, CustomRoles.USER);

                var profile = new Profile
                {
                    Email = user.Email,
                    NombreCompleto = user.NombreCompleto,
                    Token = await TokenService.CreateToken(user),
                    UserName = user.UserName
                };

                return Result<Profile>.Success(profile);
            }

            return Result<Profile>.Failure("Errores en el registro del usuario");
        }

        #endregion
    }
}
