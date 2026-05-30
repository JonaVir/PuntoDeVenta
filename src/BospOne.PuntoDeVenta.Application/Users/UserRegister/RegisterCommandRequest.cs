using BospOne.PuntoDeVenta.Application.Core;
using MediatR;
using BospOne.PuntoDeVenta.Application.Users.UserAccounts;

namespace BospOne.PuntoDeVenta.Application.Users.UserRegister
{
    public record RegisterCommandRequest(RegisterRequest Register) : IRequest<Result<Profile>>,ICommandBase; 
}
