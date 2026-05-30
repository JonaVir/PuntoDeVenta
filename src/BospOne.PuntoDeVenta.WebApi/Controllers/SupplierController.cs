using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Application.Interfaces;
using BospOne.PuntoDeVenta.Application.ProductSupplier.Create;
using BospOne.PuntoDeVenta.Application.Suppliers.Create;
using BospOne.PuntoDeVenta.Application.Suppliers.Delete;
using BospOne.PuntoDeVenta.Application.Suppliers.Read;
using BospOne.PuntoDeVenta.Application.Suppliers.Update;
using BospOne.PuntoDeVenta.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BospOne.PuntoDeVenta.WebApi.Controllers
{
    [ApiController]
    [Route("api/suppliers")]
    public class SupplierController : ControllerBase
    {
        #region Fields and properties
        private readonly ISender Sender;
        private readonly IUserAccessor Accessor;
        #endregion

        #region Builders
        public SupplierController(ISender sender, IUserAccessor userAccessor)
        {
            Sender = sender;
            Accessor = userAccessor;
        }
        #endregion

        #region Endpoints

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = PolicyMaster.SUPPLIER_READ)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PagedList<SupplierResponse>>> GetSuppliers([FromQuery] SupplierRequest request, CancellationToken cancellationToken)
        {
            var query = new GetSuppliersQueryRequest(request);

            var result = await Sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.value) : NotFound();
        }


        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = PolicyMaster.SUPPLIER_READ)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Result<SupplierResponse>>> GetSupplier(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetSupplierQueryRequest(id);
            var result = await Sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.value) : NotFound(result.Error);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = PolicyMaster.SUPPLIER_WRITE)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Result<Guid>>> CreateSupplier([FromBody] CreateSupplierRequest request, CancellationToken cancellationToken)
        {
            var userID = Accessor.GetUserIdentifier();
            var command = new CreateSupplierCommandRequest(request, userID);
            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.value) : BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CustomRoles.ADMIN)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Result<bool>>> UpdateSupplier([FromBody] UpdateSupplierRequest request,Guid id,CancellationToken cancellationToken)
        {
            var userID = Accessor.GetUserIdentifier();
            var command = new UpdateSupplierCommandRequest(request, id, userID);
            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.value) : BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CustomRoles.ADMIN)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Result<Unit>>> DeleteSupplier(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteSupplierCommandRequest(id);
            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.value) : BadRequest(result.Error);
        }
        [HttpPatch("{id}/deactivate")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CustomRoles.ADMIN)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> DeactivateSupplier(Guid id, CancellationToken cancellationToken)
        {
            var userID = Accessor.GetUserIdentifier();
            var command = new DeactivateSupplierCommandRequest(id, userID);
            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.value) : NotFound();
        }

        [HttpPost("product")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CustomRoles.ADMIN)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Result<Unit>>> AddProduct([FromBody] ProductSupplierRequest request, CancellationToken cancellationToken) 
        {
            var command = new CreateProductSupplierCommandRequest(request);
            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.value) : BadRequest(result.Error);
        }

        #endregion
    }
}
