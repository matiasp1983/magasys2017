using System;
using BLL.DAL;
using System.Collections.Generic;
using BLL.Filters;
using System.Linq;

namespace BLL
{
    public class VentaBLL
    {
        #region Métodos Públicos

        public int ObtenerUltimaVenta()
        {
            List<Venta> lstVenta = null;
            int loIdVenta = 0;

            try
            {
                using (var rep = new Repository<Venta>())
                {
                    // Ordenar la lista descendentemente
                    lstVenta = rep.Search(p => p.COD_ESTADO == 1).OrderByDescending(p => p.ID_VENTA).ToList();

                    if (lstVenta.Count > 0)
                        loIdVenta = lstVenta[0].ID_VENTA + 1;
                    else
                        loIdVenta = 1;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return loIdVenta;
        }

        public bool AltaVenta(Venta oVenta, List<DetalleVenta> lstDetalleVenta)
        {
            var bRes = false;

            try
            {
                lstDetalleVenta.ForEach(x => oVenta.DetalleVenta.Add(x));

                using (var loRepVenta = new Repository<Venta>())
                    bRes = loRepVenta.Create(oVenta) != null;
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public List<VentaListado> ObtenerVentas(VentaFiltro oVentaFiltro)
        {
            List<VentaListado> lstVentaListado = null;
            List<Venta> lstVenta = null;

            try
            {
                using (var loRepVenta = new Repository<Venta>())
                {
                    lstVenta = loRepVenta.Search(p => p.COD_ESTADO == 1);

                    if (lstVenta.Count > 0)
                    {
                        if (oVentaFiltro.FECHAVENTADESDE != null && oVentaFiltro.FECHAVENTAHASTA != null)
                            lstVenta = lstVenta.FindAll(p => p.FECHA.Date >= oVentaFiltro.FECHAVENTADESDE && p.FECHA.Date <= oVentaFiltro.FECHAVENTAHASTA);
                        else if (oVentaFiltro.FECHAVENTADESDE != null && oVentaFiltro.FECHAVENTAHASTA == null)
                            lstVenta = lstVenta.FindAll(p => p.FECHA.Date >= oVentaFiltro.FECHAVENTADESDE);
                        else if (oVentaFiltro.FECHAVENTADESDE == null && oVentaFiltro.FECHAVENTAHASTA != null)
                            lstVenta = lstVenta.FindAll(p => p.FECHA.Date <= oVentaFiltro.FECHAVENTAHASTA);

                        if (lstVenta.Count > 0 && oVentaFiltro.ID_VENTA != 0)
                            lstVenta = lstVenta.FindAll(p => p.ID_VENTA == oVentaFiltro.ID_VENTA);

                        if (lstVenta.Count > 0 && oVentaFiltro.COD_FORMA_PAGO != 0)
                            lstVenta = lstVenta.FindAll(p => p.COD_FORMA_PAGO == oVentaFiltro.COD_FORMA_PAGO);

                        if (lstVenta.Count > 0 && oVentaFiltro.TIPO_DOCUMENTO != 0 && oVentaFiltro.NRO_DOCUMENTO != 0)
                            lstVenta = lstVenta.FindAll(p => p.COD_CLIENTE != null && p.Cliente.TIPO_DOCUMENTO == oVentaFiltro.TIPO_DOCUMENTO && p.Cliente.NRO_DOCUMENTO == oVentaFiltro.NRO_DOCUMENTO);

                        if (lstVenta.Count > 0 && !String.IsNullOrEmpty(oVentaFiltro.NOMBRE))
                            lstVenta = lstVenta.FindAll(p => p.COD_CLIENTE != null && p.Cliente.NOMBRE.ToUpper().Contains(oVentaFiltro.NOMBRE.ToUpper()));

                        if (lstVenta.Count > 0 && !String.IsNullOrEmpty(oVentaFiltro.APELLIDO))
                            lstVenta = lstVenta.FindAll(p => p.COD_CLIENTE != null && p.Cliente.APELLIDO.ToUpper().Contains(oVentaFiltro.APELLIDO.ToUpper()));

                        if (lstVenta.Count > 0 && !String.IsNullOrEmpty(oVentaFiltro.ALIAS))
                            lstVenta = lstVenta.FindAll(p => p.COD_CLIENTE != null && p.Cliente.ALIAS.ToUpper().Contains(oVentaFiltro.ALIAS.ToUpper()));
                    }

                    VentaListado oVentaListado;
                    lstVentaListado = new List<VentaListado>();

                    foreach (var loVenta in lstVenta)
                    {
                        oVentaListado = new VentaListado
                        {
                            ID_VENTA = loVenta.ID_VENTA,
                            FECHA = loVenta.FECHA,
                            FORMA_PAGO = loVenta.FormaPago.DESCRIPCION,
                            TOTAL = "$ " + loVenta.TOTAL.ToString()
                        };

                        if (loVenta.COD_CLIENTE != null)
                            oVentaListado.COD_CLIENTE = Convert.ToInt32(loVenta.COD_CLIENTE);

                        lstVentaListado.Add(oVentaListado);
                    }
                }
            }

            catch (Exception ex)
            {
                throw;
            }

            return lstVentaListado;
        }

        public bool AnularVenta(int idVenta)
        {
            var bRes = false;

            try
            {
                using (var loRepVenta = new Repository<Venta>())
                {
                    var loVenta = loRepVenta.Find(p => p.ID_VENTA == idVenta);

                    if (loVenta != null)
                    {
                        foreach (var oDetalleVenta in loVenta.DetalleVenta)
                        {
                            var oProductoEdicion = new ProductoEdicionBLL().ObtenerEdicion(oDetalleVenta.COD_PRODUCTO_EDICION);
                            oProductoEdicion.CANTIDAD_DISPONIBLE = oProductoEdicion.CANTIDAD_DISPONIBLE + oDetalleVenta.CANTIDAD;
                            bRes = new ProductoEdicionBLL().ModificarProductoEdicion(oProductoEdicion);
                            if (!bRes)
                                break;
                        }

                        if (bRes)
                        {
                            loVenta.COD_ESTADO = 3;
                            bRes = loRepVenta.Update(loVenta);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return bRes;
        }

        #endregion
    }

    #region Clases

    public class VentaProductos
    {
        public int COD_PRODUCTO { get; set; }
        public int COD_PRODUCTO_EDICION { get; set; }
        public string NOMBRE { get; set; }
        public string TIPO_PRODUCTO { get; set; }
        public string EDICION { get; set; }
        public string PRECIO_UNITARIO { get; set; }
        public int CANTIDAD { get; set; }
        public string VALOR_TOTAL { get; set; }
    }

    public class VentaListado
    {
        public int ID_VENTA { get; set; }
        public int COD_EDICION { get; set; }
        public string EDICION { get; set; }
        public string TIPO_PRODUCTO { get; set; }
        public DateTime FECHA { get; set; }
        public int? COD_CLIENTE { get; set; }
        public string FORMA_PAGO { get; set; }
        public string PRECIO_UNITARIO { get; set; }
        public string SUBTOTAL { get; set; }
        public int CANTIDAD { get; set; }
        public string TOTAL { get; set; }
    }

    #endregion
}