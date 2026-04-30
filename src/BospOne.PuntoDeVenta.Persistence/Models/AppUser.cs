using Microsoft.AspNetCore.Identity;

namespace BospOne.PuntoDeVenta.Persistence.Models
{
    public class AppUser : IdentityUser
    {
        public string? NombreCompleto { get; set; }
        public string? Cargo { get; set; }
    }
}
