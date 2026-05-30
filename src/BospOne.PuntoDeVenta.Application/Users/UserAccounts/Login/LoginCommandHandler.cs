using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Application.Interfaces;
using BospOne.PuntoDeVenta.Application.Users.UserAccounts;
using BospOne.PuntoDeVenta.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BospOne.PuntoDeVenta.Application.Users.UserAccounts.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, Result<Profile>>
    {
        #region Fields and properties
        private readonly UserManager<AppUser> UserManager;
        private readonly SignInManager<AppUser> SignInManager;
        private readonly ITokenService TokenService;
        #endregion

        #region Builders
        public LoginCommandHandler(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,ITokenService tokenService)
        {
            UserManager = userManager;
            TokenService = tokenService;
            SignInManager = signInManager;
        }
        #endregion

        #region Methods
        public async Task<Result<Profile>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await UserManager.FindByEmailAsync(request.Login.Email!);

                if (user is null)
                    return Result<Profile>.Failure("Email incorrecto");

                var result = await SignInManager.CheckPasswordSignInAsync(user,request.Login.Password,lockoutOnFailure: true);
                //var result = await UserManager.CheckPasswordAsync(user, request.Login.Password!);

                if (result.IsLockedOut)
                    return Result<Profile>.Failure("usuario deshabilitado");

                if (!result.Succeeded)
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
            catch (Exception ex)
            {
                return Result<Profile>.Failure(ex.ToString());
            }

        }
        #endregion
    }
}
