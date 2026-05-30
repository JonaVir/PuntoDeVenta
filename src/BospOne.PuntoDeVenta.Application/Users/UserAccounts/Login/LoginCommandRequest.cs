using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Application.Users.UserAccounts;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.Users.UserAccounts.Login
{
    public record LoginCommandRequest(LoginRequest Login) : IRequest<Result<Profile>>, ICommandBase;
}
