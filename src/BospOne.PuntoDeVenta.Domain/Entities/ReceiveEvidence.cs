using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BospOne.PuntoDeVenta.Domain.Entities
{
    public class ReceiveEvidence : EntityBase
    {
        public Guid ProductsReceivedID { get; set; }
        public string Url { get; set; } = string.Empty;

        public ProductsReceived? ProductsReceived { get; set; }
    }
}
