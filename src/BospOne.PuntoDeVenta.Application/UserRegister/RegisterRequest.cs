using BospOne.PuntoDeVenta.Domain.Entities;

namespace BospOne.PuntoDeVenta.Application.UserRegister
{
    public class RegisterRequest
    {
        public string? NombreCompleto { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Cargo { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
