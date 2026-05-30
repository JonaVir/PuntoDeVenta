using BospOne.PuntoDeVenta.Application.Core;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.Users.GetUsers
{
    public class GetUsersQueryRequest : IRequest<Result<PagedList<UserResponse>>>
    {
        public readonly UserRequest User;
        public GetUsersQueryRequest(UserRequest user)
        {
            User = user;
        }
    }
}
