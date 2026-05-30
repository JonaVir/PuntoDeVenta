using BospOne.PuntoDeVenta.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using BospOne.PuntoDeVenta.Application.Users.DisableUser;
using BospOne.PuntoDeVenta.Application.Users.UserRegister;
using BospOne.PuntoDeVenta.Application.Users.UserAccounts;
using BospOne.PuntoDeVenta.Application.Users.UserAccounts.Login;
using BospOne.PuntoDeVenta.Application.Users.UpdateUser;
using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Application.Users.GetUsers;

namespace BospOne.PuntoDeVenta.WebApi.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class UserAccountController : ControllerBase
    {
        #region Fields and properties
        private readonly ISender Sender;
        #endregion

        #region Builders
        public UserAccountController(ISender sender)
        {
            Sender = sender;
        }
        #endregion

        #region Endpoints

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CustomRoles.ADMIN)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PagedList<UserResponse>>> GetUsers([FromQuery] UserRequest request, CancellationToken cancellationToken)
        {
            var query = new GetUsersQueryRequest(request);
            var result = await Sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.value) : NotFound(result.Error);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles =CustomRoles.ADMIN)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Result<UserResponse>>> GetUser(string id, CancellationToken cancellationToken)
        {
            var query = new GetUserQueryRequest(id);
            var result = await Sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.value) : NotFound(result.Error);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Profile>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var command = new LoginCommandRequest(request);
            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.value) : BadRequest(result.Error);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Profile>> Register([FromForm] RegisterRequest request, CancellationToken cancellationToken)
        {
            var command = new RegisterCommandRequest(request);
            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.value) : BadRequest();
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = CustomRoles.ADMIN)]
        [HttpPut("users/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateUser([FromBody] UpdateUserRequest request,Guid id, CancellationToken cancellationToken)
        {
            var command = new UpdateUserCommandRequest(request,id);
            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.value) : Unauthorized();
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CustomRoles.ADMIN)]
        [HttpPatch("users/{id}/disable")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> DisableUser(Guid id,CancellationToken cancellationToken)
        {
            var command = new DisableUserCommandRequest(id.ToString());
            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.value) : Unauthorized(result.Error);
        }
        #endregion
    }
}
