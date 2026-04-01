using BospOne.PuntoDeVenta.Application.Interfaces;

namespace BospOne.PuntoDeVenta.Application.Entities
{
    public class Supplier : EntityBase, IAuditory
    {
        /// <summary>
        /// especifica el código del proveedor, 
        /// este código es único para cada proveedor y se utiliza para identificarlo de manera rápida y eficiente en el sistema. 
        /// El código puede ser alfanumérico y debe ser asignado de manera cuidadosa para evitar duplicados y facilitar 
        /// la gestión de los proveedores en la base de datos.
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// especifica la razon social del proveedor, que es el nombre legal bajo el cual opera la empresa.
        /// </summary>
        public string? BusinessName { get; set; }
        /// <summary>
        /// especifica el nombre comercial del proveedor, que es el nombre con el que se conoce comúnmente a la empresa en el mercado.
        /// </summary>
        public string TradeName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}

