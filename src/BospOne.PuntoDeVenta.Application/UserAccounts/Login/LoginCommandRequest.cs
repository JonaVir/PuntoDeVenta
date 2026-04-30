using BospOne.PuntoDeVenta.Application.Core;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.UserAccounts.Login
{
    public record LoginCommandRequest(LoginRequest Login) : IRequest<Result<Profile>>, ICommandBase;
}
