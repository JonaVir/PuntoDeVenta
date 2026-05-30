using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Application.Interfaces;
using BospOne.PuntoDeVenta.Application.Products.Create;
using BospOne.PuntoDeVenta.Application.Products.Delete;
using BospOne.PuntoDeVenta.Application.Products.Read;
using BospOne.PuntoDeVenta.Application.Products.Update;
using BospOne.PuntoDeVenta.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BospOne.PuntoDeVenta.WebApi.Controllers
{
    [Controller]
    [Route("Product")]
    public class ProductController : ControllerBase
    {
        #region Fields and properties
        private readonly ISender Sender;
        private readonly IUserAccessor UserAccesor;
        #endregion

        #region Builders
        public ProductController(ISender sender, IUserAccessor userAccesor)
        {
            Sender = sender;
            UserAccesor = userAccesor;
        }
        #endregion

        #region Endpoints

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = PolicyMaster.PRODUCT_READ)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PagedList<ProductResponse>>> GetProducts([FromQuery] ProductRequest request, CancellationToken cancellationToken)
        {
            var query = new GetProductsQueryRequest(request);
            var result = await Sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.value) : NotFound(result.Error);    
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = PolicyMaster.PRODUCT_READ)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Result<ProductResponse>>> GetProduct(Guid id,CancellationToken cancellationToken)
        {
            var query = new GetProductQueryRequest(id);
            var result = await Sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.value) : NotFound(result.Error);
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CustomRoles.ADMIN)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Result<Guid>>> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var userID = UserAccesor.GetUserIdentifier();
            var command = new CreateProductCommandRequest(request, userID);
            var result = await Sender.Send(command,cancellationToken);

            return result.IsSuccess ? Ok(result.value) : BadRequest("Error interno");
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CustomRoles.ADMIN)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Result<Guid>>> UpdateProduct([FromBody] UpdateProductRequest request, Guid id, CancellationToken cancellationToken)
        {
            var UserID = UserAccesor.GetUserIdentifier();
            var command = new UpdateProductCommandRequest(request, UserID, id);
            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.value) : BadRequest(result.Error);
        }

        [HttpDelete("{id}/remove")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CustomRoles.ADMIN)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Result<Unit>>> DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommandRequest(id);
            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.value) : BadRequest(result.Error);
        }
        #endregion
    }
}
