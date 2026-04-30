using BospOne.PuntoDeVenta.Application.Core;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BospOne.PuntoDeVenta.WebApi.Middleware
{
    public class ExceptionMiddleware
    {
        #region Fields and properties
        private readonly RequestDelegate Next;
        private readonly ILogger<ExceptionMiddleware> Logger;
        private readonly IHostEnvironment Env;
        #endregion

        #region Builders
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            Next = next;
            Logger = logger;
            Env = env;
        }
        #endregion

        #region Methods and Functions
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);

                var response = ex switch
                {
                    ValidationException validation => new AppException(
                        StatusCodes.Status400BadRequest,
                        "Error de validación",
                        string.Join(",", validation.Errors.Select(Er => Er.ErrorMessage))
                    ),

                    _ => new AppException(
                        context.Response.StatusCode,
                        ex.Message,
                        ex.StackTrace?.ToString()
                        )
                };

                context.Response.StatusCode = response.StatusCode;
                context.Response.ContentType = "Application/json";

                var json = JsonConvert.SerializeObject(response);

                await context.Response.WriteAsync(json);
            }
        }
        #endregion
    }
}
