using BospOne.PuntoDeVenta.Application.Interfaces;
using BospOne.PuntoDeVenta.Application.UserAccounts;
using BospOne.PuntoDeVenta.Application.UserAccounts.Login;
using BospOne.PuntoDeVenta.Application.UserRegister;
using BospOne.PuntoDeVenta.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BospOne.PuntoDeVenta.WebApi.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        #region Fields and properties
        private readonly ISender Sender;
        private readonly IUserAccessor User;
        #endregion

        #region Builders
        public AccountController(ISender sender, IUserAccessor user)
        {
            Sender = sender;
            User = user;
        }
        #endregion

        #region Endpoints
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Profile>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var command = new LoginCommandRequest(request);
            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.value) : Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Profile>> Register([FromForm] RegisterRequest request, CancellationToken cancellationToken)
        {
            var command = new RegisterCommandRequest(request);
            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.value) : Unauthorized();
        }

        [Authorize(Roles = CustomRoles.ADMIN)]
        [HttpPatch("user-role")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Profile>> ChangeRoleUser([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            
        }
        #endregion
    }
}
