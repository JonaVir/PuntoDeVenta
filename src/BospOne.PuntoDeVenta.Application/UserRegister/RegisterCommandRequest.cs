using BospOne.PuntoDeVenta.Application.Core;
using MediatR;
using BospOne.PuntoDeVenta.Application.UserAccounts;

namespace BospOne.PuntoDeVenta.Application.UserRegister
{
    public record RegisterCommandRequest(RegisterRequest Register) : IRequest<Result<Profile>>,ICommandBase; 
}
