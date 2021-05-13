using System;

namespace BLL.Filters
{
    public class IngresoProductoFiltro
    {
        #region Propiedades

        public long CodTipoProducto { get; set; }
        public long CodEstado { get; set; }
        public long IdProveedor { get; set; }
        public DateTime? FechaAltaDesde { get; set; }
        public DateTime? FechaAltaHasta { get; set; }
        public string NombreProducto { get; set; }
        public string Edicion { get; set; }

        #endregion
    }
}
