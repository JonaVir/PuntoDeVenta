using BospOne.PuntoDeVenta.Application.Core;

namespace BospOne.PuntoDeVenta.Application.Users.GetUsers
{
    public class UserRequest : PagingParams
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}
