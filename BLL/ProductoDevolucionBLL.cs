using System;
using BLL.Filters;
using BLL.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

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
                            var loCantidadReservaEdicionPorProdEdicion = new ReservaEdicionBLL().CantidadReservaEdicionPorProductoEdicion(loDetalleProductoIngreso.COD_PRODUCTO_EDICION);

                            oDevolucion = new Devolucion
                            {
                                COD_PRODUCTO = loDetalleProductoIngreso.ProductoEdicion.COD_PRODUCTO,
                                COD_PRODUCTO_EDICION = loDetalleProductoIngreso.COD_PRODUCTO_EDICION,
                                NOMBRE = loDetalleProductoIngreso.ProductoEdicion.Producto.NOMBRE,
                                TIPO_PRODUCTO = loDetalleProductoIngreso.ProductoEdicion.Producto.TipoProducto.DESCRIPCION,
                                EDICION = loDetalleProductoIngreso.ProductoEdicion.EDICION,
                                FECHA_EDICION = Convert.ToDateTime(loDetalleProductoIngreso.ProductoEdicion.FECHA_EDICION),
                                FECHA_DEVOLUCION = Convert.ToDateTime(loDetalleProductoIngreso.FECHA_DEVOLUCION),
                                CANTIDAD_DISPONIBLE = loDetalleProductoIngreso.ProductoEdicion.CANTIDAD_DISPONIBLE,
                                CANTIDAD_RESERVAS = loCantidadReservaEdicionPorProdEdicion
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

        public List<ProductoDevolucionListado> ObtenerDevolucion(IngresoProductoFiltro oDevolucionFiltro)
        {
            List<ProductoDevolucionListado> lstProductoDevolucionListado = null;
            List<DetalleProductoDevolucion> lstDetalleProductoDevolucion = null;

            try
            {
                using (var loRepDetalleProductoDevolucion = new Repository<DetalleProductoDevolucion>())
                {
                    if (oDevolucionFiltro.IdProveedor > 0)
                        lstDetalleProductoDevolucion = loRepDetalleProductoDevolucion.Search(p => p.ProductoDevolucion.COD_ESTADO == 1 && p.ProductoEdicion.Producto.COD_PROVEEDOR == oDevolucionFiltro.IdProveedor).GroupBy(x => x.COD_PRODUCTO_DEVOLUCION).Select(m => m.First()).ToList();

                    if (lstDetalleProductoDevolucion != null)
                    {
                        if (oDevolucionFiltro.FechaAltaDesde != null && oDevolucionFiltro.FechaAltaHasta != null)
                            lstDetalleProductoDevolucion = lstDetalleProductoDevolucion.FindAll(p => p.ProductoDevolucion.FECHA.Date >= oDevolucionFiltro.FechaAltaDesde && p.ProductoDevolucion.FECHA.Date <= oDevolucionFiltro.FechaAltaHasta);
                        else if (oDevolucionFiltro.FechaAltaDesde != null && oDevolucionFiltro.FechaAltaHasta == null)
                            lstDetalleProductoDevolucion = lstDetalleProductoDevolucion.FindAll(p => p.ProductoDevolucion.FECHA.Date >= oDevolucionFiltro.FechaAltaDesde);
                        else if (oDevolucionFiltro.FechaAltaDesde == null && oDevolucionFiltro.FechaAltaHasta != null)
                            lstDetalleProductoDevolucion = lstDetalleProductoDevolucion.FindAll(p => p.ProductoDevolucion.FECHA.Date <= oDevolucionFiltro.FechaAltaHasta);
                    }
                    else
                    {
                        if (oDevolucionFiltro.FechaAltaDesde != null && oDevolucionFiltro.FechaAltaHasta != null)
                            lstDetalleProductoDevolucion = loRepDetalleProductoDevolucion.Search(p => p.ProductoDevolucion.COD_ESTADO == 1 && DbFunctions.TruncateTime(p.ProductoDevolucion.FECHA) >= DbFunctions.TruncateTime(oDevolucionFiltro.FechaAltaDesde) && DbFunctions.TruncateTime(p.ProductoDevolucion.FECHA) <= DbFunctions.TruncateTime(oDevolucionFiltro.FechaAltaHasta)).GroupBy(x => x.COD_PRODUCTO_DEVOLUCION).Select(m => m.First()).ToList();
                        else if (oDevolucionFiltro.FechaAltaDesde != null && oDevolucionFiltro.FechaAltaHasta == null)
                            lstDetalleProductoDevolucion = loRepDetalleProductoDevolucion.Search(p => p.ProductoDevolucion.COD_ESTADO == 1 && DbFunctions.TruncateTime(p.ProductoDevolucion.FECHA) >= DbFunctions.TruncateTime(oDevolucionFiltro.FechaAltaDesde)).GroupBy(x => x.COD_PRODUCTO_DEVOLUCION).Select(m => m.First()).ToList();
                        else if (oDevolucionFiltro.FechaAltaDesde == null && oDevolucionFiltro.FechaAltaHasta != null)
                            lstDetalleProductoDevolucion = loRepDetalleProductoDevolucion.Search(p => p.ProductoDevolucion.COD_ESTADO == 1 && DbFunctions.TruncateTime(p.ProductoDevolucion.FECHA) <= DbFunctions.TruncateTime(oDevolucionFiltro.FechaAltaHasta)).GroupBy(x => x.COD_PRODUCTO_DEVOLUCION).Select(m => m.First()).ToList();
                    }


                    ProductoDevolucionListado oProductoDevolucionListado;
                    lstProductoDevolucionListado = new List<ProductoDevolucionListado>();

                    if (lstDetalleProductoDevolucion != null)
                    {
                        foreach (var loDetalleProductoDevolucion in lstDetalleProductoDevolucion)
                        {
                            oProductoDevolucionListado = new ProductoDevolucionListado
                            {
                                ID_PRODUCTO_DEVOLUCION = loDetalleProductoDevolucion.COD_PRODUCTO_DEVOLUCION,
                                FECHA = loDetalleProductoDevolucion.ProductoDevolucion.FECHA
                            };

                            lstProductoDevolucionListado.Add(oProductoDevolucionListado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstProductoDevolucionListado;
        }

        public bool AnularDevolucion(int idProductoDevolucion)
        {
            var bRes = false;

            try
            {
                using (var loRepProductoDevolucion = new Repository<ProductoDevolucion>())
                {
                    var loProductoDevolucion = loRepProductoDevolucion.Find(p => p.ID_PRODUCTO_DEVOLUCION == idProductoDevolucion);

                    if (loProductoDevolucion != null)
                    {
                        foreach (var oDetalleProductoDevolucion in loProductoDevolucion.DetalleProductoDevolucion)
                        {
                            var oProductoEdicion = new ProductoEdicionBLL().ObtenerEdicion(oDetalleProductoDevolucion.COD_PRODUCTO_EDICION);
                            oProductoEdicion.CANTIDAD_DISPONIBLE = oProductoEdicion.CANTIDAD_DISPONIBLE + oDetalleProductoDevolucion.CANTIDAD;
                            bRes = new ProductoEdicionBLL().ModificarProductoEdicion(oProductoEdicion);
                            if (!bRes)
                                break;
                        }

                        if (bRes)
                        {
                            loProductoDevolucion.COD_ESTADO = 3;
                            bRes = loRepProductoDevolucion.Update(loProductoDevolucion);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bRes;
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
        public int CANTIDAD_RESERVAS { get; set; }
        public int CANTIDAD { get; set; }
    }

    public class DetalleDevolucion
    {
        public int COD_PRODUCTO { get; set; }
        public int COD_PRODUCTO_EDICION { get; set; }
        public string NOMBRE { get; set; }
        public string TIPO_PRODUCTO { get; set; }
        public string EDICION { get; set; }
        public int STOCK { get; set; }
        public int CANTIDAD { get; set; }
        public int CANTIDAD_RESERVAS { get; set; }
    }

    public class ProductoDevolucionListado
    {
        public int ID_PRODUCTO_DEVOLUCION { get; set; }
        public DateTime FECHA { get; set; }
    }

    public class DetalleProductoDevolucionListado
    {
        public int ID_PRODUCTO_DEVOLUCION { get; set; }
        public int COD_EDICION { get; set; }
        public string EDICION { get; set; }
        public string TIPO_PRODUCTO { get; set; }
        public string NOMBRE_PRODUCTO { get; set; }
        public int CANTIDAD { get; set; }
    }

    #endregion
}
