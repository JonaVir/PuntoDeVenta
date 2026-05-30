using BospOne.PuntoDeVenta.Application.Core;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.Users.GetUsers
{
    public class GetUserQueryRequest : IRequest<Result<UserResponse>>
    {
        public readonly string UserID;

        public GetUserQueryRequest(string userID)
        {
            UserID = userID;
        }
    }
}
