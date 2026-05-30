using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BospOne.PuntoDeVenta.Application.Users.DisableUser
{
    public class DisableUserCommandHandler : IRequestHandler<DisableUserCommandRequest, Result<Unit>>
    {
        #region Fields and properties
        private readonly UserManager<AppUser> UserManager;
        #endregion

        #region Builders
        public DisableUserCommandHandler(UserManager<AppUser> userManager)
        {
            UserManager = userManager;
        }
        #endregion

        #region Methods
        public async Task<Result<Unit>> Handle(DisableUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await UserManager.FindByIdAsync(request.userID);

            if (user is null)
                return Result<Unit>.Failure("No se encontro el usuario especificado");

            await UserManager.SetLockoutEnabledAsync(user, true);
            await UserManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);

            return Result<Unit>.Success(Unit.Value);
        }
        #endregion
    }
}
