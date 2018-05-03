using BLL.DAL;
using BLL.Filters;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class ProductoBLL
    {
        #region Métodos Públicos

        public List<ProductoListado> ObtenerProductos(ProductoFiltro oProductoFiltro)
        {

            List<Producto> lstProductos = null;
            List<ProductoListado> lstProductoListado = null;

            try
            {
                using (var loRepProducto = new Repository<Producto>())
                {
                    lstProductos = loRepProducto.Search(p => p.FECHA_BAJA == null);

                    if (oProductoFiltro.IdProducto > 0 && lstProductos.Count > 0)
                        lstProductos = lstProductos.FindAll(p => p.ID_PRODUCTO.ToString().Contains(oProductoFiltro.IdProducto.ToString()));

                    if (!String.IsNullOrEmpty(oProductoFiltro.Nombre) && lstProductos.Count > 0)
                        lstProductos = lstProductos.FindAll(p => p.NOMBRE.ToUpper().Contains(oProductoFiltro.Nombre.ToUpper()));
                    //lstProductos = lstProductos.FindAll(p => p.NOMBRE.Contains(String.Format("%{0}%", oProductoFiltro.Nombre)));

                    if (oProductoFiltro.CodTipoProducto > 0 && lstProductos.Count > 0)
                        lstProductos = lstProductos.FindAll(p => p.COD_TIPO_PRODUCTO == oProductoFiltro.CodTipoProducto);

                    if (oProductoFiltro.CodEstado > 0 && lstProductos.Count > 0)
                        lstProductos = lstProductos.FindAll(p => p.COD_ESTADO == oProductoFiltro.CodEstado);

                    if (oProductoFiltro.CodProveedor > 0 && lstProductos.Count > 0)
                        lstProductos = lstProductos.FindAll(p => p.COD_PROVEEDOR == oProductoFiltro.CodProveedor);

                    ProductoListado oProductoListado;
                    lstProductoListado = new List<ProductoListado>();

                    foreach (var loProducto in lstProductos)
                    {
                        oProductoListado = new ProductoListado
                        {
                            ID_PRODUCTO = loProducto.ID_PRODUCTO,
                            NOMBRE = loProducto.NOMBRE,
                            COD_ESTADO = loProducto.COD_ESTADO,
                            COD_GENERO = loProducto.COD_GENERO,
                            COD_PROVEEDOR = loProducto.COD_GENERO,
                            COD_TIPO_PRODUCTO = loProducto.COD_TIPO_PRODUCTO
                        };

                        using (var loRepEstadoProducto = new Repository<EstadoProducto>())
                            oProductoListado.DESC_ESTADO = loRepEstadoProducto.Find(p => p.ID_ESTADO_PROD == loProducto.COD_ESTADO).NOMBRE;

                        using (var loRepGenero = new Repository<Genero>())
                            oProductoListado.DESC_GENERO = loRepGenero.Find(p => p.ID_GENERO == loProducto.COD_GENERO).NOMBRE;

                        using (var loRepProveedor = new Repository<Proveedor>())
                            oProductoListado.DESC_PROVEEDOR = loRepProveedor.Find(p => p.ID_PROVEEDOR == loProducto.COD_PROVEEDOR).RAZON_SOCIAL;

                        using (var loRepTipoProducto = new Repository<TipoProducto>())
                            oProductoListado.DESC_TIPO_PRODUCTO = loRepTipoProducto.Find(p => p.ID_TIPO_PRODUCTO == loProducto.COD_TIPO_PRODUCTO).DESCRIPCION;

                        lstProductoListado.Add(oProductoListado);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return lstProductoListado;
        }

        #endregion
    }

    #region Clases

    public class ProductoListado
    {
        public int ID_PRODUCTO { get; set; }
        public string NOMBRE { get; set; }
        public int COD_ESTADO { get; set; }
        public string DESC_ESTADO { get; set; }
        public int COD_GENERO { get; set; }
        public string DESC_GENERO { get; set; }
        public int COD_PROVEEDOR { get; set; }
        public string DESC_PROVEEDOR { get; set; }
        public int COD_TIPO_PRODUCTO { get; set; }
        public string DESC_TIPO_PRODUCTO { get; set; }
    }

    #endregion

}
