using System;

namespace BLL.Filters
{
    public class IngresoProductoFiltro
    {
        #region Propiedades

        public long IdProveedor { get; set; }
        public DateTime? FechaAltaDesde { get; set; }
        public DateTime? FechaAltaHasta { get; set; }

        #endregion
    }
}
