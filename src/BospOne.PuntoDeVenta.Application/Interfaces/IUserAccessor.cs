namespace BospOne.PuntoDeVenta.Application.Interfaces
{
    public interface IUserAccessor
    {
        public string GetUserIdentifier();
        string GetUserName();
        string GetEmail();
    }
}
