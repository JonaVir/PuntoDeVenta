using BospOne.PuntoDeVenta.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BospOne.PuntoDeVenta.Infrastructure.Security
{
    public class UserAccessor : IUserAccessor
    {
        #region Fields and properties
        private readonly IHttpContextAccessor HttpContextAccessor;
        #endregion

        #region Builders
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Methods

        public string GetUserIdentifier()
        {
            return HttpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }
        public string GetEmail()
        {
            return HttpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email)!;
        }

        public string GetUserName()
        {
            return HttpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Name)!;
        }
        #endregion

    }
}
