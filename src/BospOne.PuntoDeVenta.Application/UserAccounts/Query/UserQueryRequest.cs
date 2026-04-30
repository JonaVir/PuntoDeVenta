using BospOne.PuntoDeVenta.Application.Core;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.UserAccounts.Query
{
    public record UserQueryRequest(UserRequest User) : IRequest<Result<Profile>>;
}
