namespace BospOne.PuntoDeVenta.Application.Core
{
    public class Result<T>
    {
        #region Fields and properties
        public bool IsSuccess { get; set; }
        public T? value { get; set; }
        public string? Error { get; set; }

        public static Result<T> Success(T value) => new Result<T>
        {
            IsSuccess = true,
            value = value
        };

        public static Result<T> Failure(string error) => new Result<T>
        {
            IsSuccess = false,
            Error = error
        };

        #endregion
    }
}
