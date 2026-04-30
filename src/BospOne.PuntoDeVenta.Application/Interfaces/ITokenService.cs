using BospOne.PuntoDeVenta.Persistence.Models;

namespace BospOne.PuntoDeVenta.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
