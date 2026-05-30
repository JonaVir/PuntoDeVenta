using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Application.Users.UserAccounts;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.Users.UserAccounts.Query
{
    public record UserQueryRequest(UserRequest User) : IRequest<Result<Profile>>;
}
