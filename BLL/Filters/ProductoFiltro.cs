using System;

namespace BLL.Filters
{
    public class ProductoFiltro
    {
        #region Propiedades

        public long IdProducto { get; set; }
        public string Nombre { get; set; }
        public long CodTipoProducto { get; set; }
        public long CodEstado { get; set; }
        public long CodProveedor { get; set; }

        #endregion
    }
}
