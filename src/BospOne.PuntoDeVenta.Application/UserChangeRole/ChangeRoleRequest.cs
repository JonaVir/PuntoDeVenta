using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BospOne.PuntoDeVenta.Application.UserChangeRole
{
    public class ChangeRoleRequest
    {
        public string? Email { get; set; }
        public string? NewRole { get; set; }
    }
}
