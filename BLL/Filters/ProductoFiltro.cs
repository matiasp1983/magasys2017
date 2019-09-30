using System;

namespace BLL.Filters
{
    public class ProductoFiltro
    {
        #region Propiedades

        public long IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public string NombreEdicion { get; set; }
        public string DescripcionEdicion { get; set; }
        public long CodTipoProducto { get; set; }
        public long CodEstado { get; set; }
        public long CodProveedor { get; set; }
        public long CodGenero { get; set; }

        #endregion
    }
}
