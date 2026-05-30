using BospOne.PuntoDeVenta.Application.Core;

namespace BospOne.PuntoDeVenta.Application.Users.GetUsers
{
    public class UserResponse
    {
        public string? Id { get; set; }
        public string? NombreCompleto { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Cargo { get; set; }
    }
}
