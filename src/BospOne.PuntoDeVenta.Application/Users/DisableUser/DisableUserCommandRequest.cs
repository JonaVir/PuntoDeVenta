using BospOne.PuntoDeVenta.Application.Core;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.Users.DisableUser
{
    public record DisableUserCommandRequest(string userID) : IRequest<Result<Unit>>, ICommandBase;
}