using BospOne.PuntoDeVenta.Application.Core;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.Users.UpdateUser
{
    public record UpdateUserCommandRequest(UpdateUserRequest user, Guid UserId) : IRequest<Result<bool>>, ICommandBase;
}
