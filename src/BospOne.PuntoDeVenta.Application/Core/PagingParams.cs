namespace BospOne.PuntoDeVenta.Application.Core
{
    public abstract class PagingParams
    {
        private const int MaxPageSize = 50;
        private int pageSize = 10;
        public int PageNumber { get; set; }
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string? OrderBy { get; set; }
        public bool? OrderAsc { get; set; } = true;

    }
}
