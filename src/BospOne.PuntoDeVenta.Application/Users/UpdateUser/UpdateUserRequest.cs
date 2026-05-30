namespace BospOne.PuntoDeVenta.Application.Users.UpdateUser
{
    public class UpdateUserRequest
    {
        public string? NombreCompleto { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Cargo { get; set; }
        public string? NewRole { get; set; }
        public string? NewPassword { get; set; }
    }
}
