namespace BospOne.PuntoDeVenta.Application.Interfaces
{
    public interface IAuditory
    {
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
