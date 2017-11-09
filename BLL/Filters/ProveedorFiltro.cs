using System;

namespace BLL.Filters
{
    public class ProveedorFiltro
    {
        #region Propiedades

        public long IdProveedor { get; set; }
        public DateTime? FechaAltaDesde { get; set; }
        public DateTime? FechaAltaHasta { get; set; }
        public string Cuit { get; set; }
        public string RazonSocial { get; set; }

        #endregion
    }
}
