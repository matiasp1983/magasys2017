using System;
using BLL.Filters;
using BLL.DAL;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class ProductoDevolucionBLL
    {
        #region Métodos Públicos

        public int ObtenerUltimoProductodevolucion()
        {
            List<ProductoDevolucion> lstProductoDevolucion = null;
            int loIdProductoDevolucion = 0;

            try
            {
                using (var rep = new Repository<ProductoDevolucion>())
                {
                    // Ordenar la lista descendentemente
                    lstProductoDevolucion = rep.Search(p => p.COD_ESTADO == 1).OrderByDescending(p => p.ID_PRODUCTO_DEVOLUCION).ToList();

                    if (lstProductoDevolucion.Count > 0)
                        loIdProductoDevolucion = lstProductoDevolucion[0].ID_PRODUCTO_DEVOLUCION + 1;
                    else
                        loIdProductoDevolucion = 1;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return loIdProductoDevolucion;
        }

        public bool AltaDevolucion(ProductoDevolucion oProductoDevolucion, List<DetalleProductoDevolucion> lstDetalleProductoDevolucion)
        {
            var bRes = false;

            try
            {
                lstDetalleProductoDevolucion.ForEach(x => oProductoDevolucion.DetalleProductoDevolucion.Add(x));

                using (var loRepProductoDevolucion = new Repository<ProductoDevolucion>())
                    bRes = loRepProductoDevolucion.Create(oProductoDevolucion) != null;
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public List<Devolucion> ObtenerProductos(ProductoFiltro oProductoFiltro)
        {
            List<Devolucion> lstDevolucion = null;
            Devolucion oDevolucion = null;
            List<DetalleProductoIngreso> lstDetalleProductoIngreso = null;

            try
            {
                using (var loRepDetalleProductoIngreso = new Repository<DetalleProductoIngreso>())
                {
                    lstDetalleProductoIngreso = loRepDetalleProductoIngreso.Search(p => p.FECHA_DEVOLUCION != null && p.ProductoEdicion.COD_ESTADO == 1 && p.ProductoEdicion.COD_TIPO_PRODUCTO == oProductoFiltro.CodTipoProducto && p.ProductoEdicion.Producto.COD_PROVEEDOR == oProductoFiltro.CodProveedor && p.ProductoEdicion.CANTIDAD_DISPONIBLE > 0).GroupBy(x => x.COD_PRODUCTO_EDICION).Select(m => m.First()).ToList();

                    if (!String.IsNullOrEmpty(oProductoFiltro.NombreEdicion) && lstDetalleProductoIngreso.Count > 0)
                        lstDetalleProductoIngreso = lstDetalleProductoIngreso.FindAll(p => p.ProductoEdicion.EDICION.ToUpper().Contains(oProductoFiltro.NombreEdicion.ToUpper()));

                    if (!String.IsNullOrEmpty(oProductoFiltro.DescripcionEdicion) && lstDetalleProductoIngreso.Count > 0)
                        lstDetalleProductoIngreso = lstDetalleProductoIngreso.FindAll(p => !string.IsNullOrEmpty(p.ProductoEdicion.DESCRIPCION) && p.ProductoEdicion.DESCRIPCION.ToUpper().Contains(oProductoFiltro.DescripcionEdicion.ToUpper()));

                    if (!String.IsNullOrEmpty(oProductoFiltro.NombreProducto) && lstDetalleProductoIngreso.Count > 0)
                        lstDetalleProductoIngreso = lstDetalleProductoIngreso.FindAll(p => p.ProductoEdicion.Producto.NOMBRE.ToUpper().Contains(oProductoFiltro.NombreProducto.ToUpper()));

                    if (!String.IsNullOrEmpty(oProductoFiltro.DescripcionProducto) && lstDetalleProductoIngreso.Count > 0)
                        lstDetalleProductoIngreso = lstDetalleProductoIngreso.FindAll(p => !string.IsNullOrEmpty(p.ProductoEdicion.Producto.DESCRIPCION) && p.ProductoEdicion.Producto.DESCRIPCION.ToUpper().Contains(oProductoFiltro.DescripcionProducto.ToUpper()));

                    if (lstDetalleProductoIngreso.Count > 0)
                    {
                        lstDevolucion = new List<Devolucion>();

                        foreach (var loDetalleProductoIngreso in lstDetalleProductoIngreso)
                        {
                            oDevolucion = new Devolucion
                            {
                                COD_PRODUCTO = loDetalleProductoIngreso.ProductoEdicion.COD_PRODUCTO,
                                COD_PRODUCTO_EDICION = loDetalleProductoIngreso.COD_PRODUCTO_EDICION,
                                NOMBRE = loDetalleProductoIngreso.ProductoEdicion.Producto.NOMBRE,
                                TIPO_PRODUCTO = loDetalleProductoIngreso.ProductoEdicion.Producto.TipoProducto.DESCRIPCION,
                                EDICION = loDetalleProductoIngreso.ProductoEdicion.EDICION,
                                FECHA_EDICION = Convert.ToDateTime(loDetalleProductoIngreso.ProductoEdicion.FECHA_EDICION),
                                FECHA_DEVOLUCION = Convert.ToDateTime(loDetalleProductoIngreso.FECHA_DEVOLUCION),
                                CANTIDAD_DISPONIBLE = loDetalleProductoIngreso.ProductoEdicion.CANTIDAD_DISPONIBLE
                            };

                            lstDevolucion.Add(oDevolucion);
                        }
                    }
                }
                return lstDevolucion;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Devolucion> ObtenerDevolucionesDiarias()
        {
            List<Devolucion> lstDevolucion = null;
            Devolucion oDevolucion = null;
            List<DetalleProductoIngreso> lstDetalleProductoIngreso = null;
            DateTime loFechaDia = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));

            try
            {
                using (var loRepDetalleProductoIngreso = new Repository<DetalleProductoIngreso>())
                {
                    lstDetalleProductoIngreso = loRepDetalleProductoIngreso.Search(p => p.FECHA_DEVOLUCION != null && p.FECHA_DEVOLUCION == loFechaDia && p.ProductoEdicion.COD_ESTADO == 1 && p.ProductoEdicion.CANTIDAD_DISPONIBLE > 0).GroupBy(x => x.COD_PRODUCTO_EDICION).Select(m => m.First()).ToList();

                    if (lstDetalleProductoIngreso.Count > 0)
                    {
                        lstDevolucion = new List<Devolucion>();

                        foreach (var loDetalleProductoIngreso in lstDetalleProductoIngreso)
                        {
                            oDevolucion = new Devolucion
                            {
                                COD_PRODUCTO = loDetalleProductoIngreso.ProductoEdicion.COD_PRODUCTO,
                                COD_PRODUCTO_EDICION = loDetalleProductoIngreso.COD_PRODUCTO_EDICION,
                                NOMBRE = loDetalleProductoIngreso.ProductoEdicion.Producto.NOMBRE,
                                TIPO_PRODUCTO = loDetalleProductoIngreso.ProductoEdicion.Producto.TipoProducto.DESCRIPCION,
                                EDICION = loDetalleProductoIngreso.ProductoEdicion.EDICION,
                                FECHA_EDICION = Convert.ToDateTime(loDetalleProductoIngreso.ProductoEdicion.FECHA_EDICION),
                                FECHA_DEVOLUCION = Convert.ToDateTime(loDetalleProductoIngreso.FECHA_DEVOLUCION),
                                CANTIDAD_DISPONIBLE = loDetalleProductoIngreso.ProductoEdicion.CANTIDAD_DISPONIBLE
                            };

                            lstDevolucion.Add(oDevolucion);
                        }
                    }
                }
                return lstDevolucion;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }

    #region Clases

    public class Devolucion
    {
        public int COD_PRODUCTO { get; set; }
        public int COD_PRODUCTO_EDICION { get; set; }
        public string NOMBRE { get; set; }
        public string TIPO_PRODUCTO { get; set; }
        public string EDICION { get; set; }
        public DateTime FECHA_EDICION { get; set; }
        public DateTime FECHA_DEVOLUCION { get; set; }
        public int CANTIDAD_DISPONIBLE { get; set; }
        public int CANTIDAD { get; set; }
    }

    public class DetalleDevolucion
    {
        public int COD_PRODUCTO { get; set; }
        public int COD_PRODUCTO_EDICION { get; set; }
        public string NOMBRE { get; set; }
        public string TIPO_PRODUCTO { get; set; }
        public string EDICION { get; set; }
        public int CANTIDAD { get; set; }
    }

    #endregion
}
