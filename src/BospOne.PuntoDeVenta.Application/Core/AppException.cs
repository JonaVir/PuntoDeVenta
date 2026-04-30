namespace BospOne.PuntoDeVenta.Application.Core
{
    public class AppException
    {
        #region Fields and properties
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string? Details { get; set; }
        #endregion

        #region Builders
        public AppException(int statusCode, string message, string? details = "")
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
        #endregion
    }
}
