using BLL.Common;
using BLL.DAL;
using System;
using System.Collections.Generic;

namespace PL.CustomersWebSite
{
    public partial class DetalleVentaProductos : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCantidadDePedidosDesdeSession();
                CargarDetalleVenta();
            }
        }

        #endregion

        #region Métodos Privados

        private void CargarDetalleVenta()
        {
            List<BLL.VentaListado> lstVentaListado = null;
            BLL.VentaListado oVentaListado = null;
            lsvDetalleVenta.DataSource = null;
            lsvDetalleVenta.Visible = false;
            int loIdVenta = 0;

            if (Convert.ToInt32(Session[Enums.Session.DetalleVenta.ToString()]) > 0)
            {
                loIdVenta = Convert.ToInt32(Session[Enums.Session.DetalleVenta.ToString()]);
                Session.Remove(Enums.Session.DetalleVenta.ToString());
            }

            if (loIdVenta > 0)
            {
                using (var loRepVenta = new Repository<Venta>())
                {
                    var loVenta = loRepVenta.Find(p => p.ID_VENTA == loIdVenta);

                    if (loVenta != null)
                    {
                        lstVentaListado = new List<BLL.VentaListado>();
                        txtCodigoVenta.Text = loVenta.ID_VENTA.ToString();
                        txtFechaVenta.Text = loVenta.FECHA.ToString("dd/MM/yyyy");
                        txtFormaPago.Text = loVenta.FormaPago.DESCRIPCION;
                        txtEstado.Text = loVenta.Estado.NOMBRE;
                        lblTotal.Text = loVenta.TOTAL.ToString();

                        foreach (var loDetalleVenta in loVenta.DetalleVenta)
                        {
                            oVentaListado = new BLL.VentaListado
                            {
                                ID_VENTA = loVenta.ID_VENTA,
                                COD_EDICION = loDetalleVenta.COD_PRODUCTO_EDICION,
                                EDICION = loDetalleVenta.ProductoEdicion.EDICION,
                                TIPO_PRODUCTO = loDetalleVenta.ProductoEdicion.Producto.TipoProducto.DESCRIPCION,
                                PRECIO_UNITARIO = "$" + loDetalleVenta.PRECIO_UNIDAD.ToString(),
                                CANTIDAD = loDetalleVenta.CANTIDAD,
                                SUBTOTAL = "$" + loDetalleVenta.SUBTOTAL.ToString()
                            };

                            lstVentaListado.Add(oVentaListado);
                        }

                        if (lstVentaListado.Count > 0)
                        {
                            lsvDetalleVenta.Visible = true;
                            lsvDetalleVenta.DataSource = lstVentaListado;
                            lsvDetalleVenta.DataBind();
                        }
                    }
                }
            }
        }

        private void CargarCantidadDePedidosDesdeSession()
        {
            Master.CantidadDePedidos = Convert.ToInt32(Session[Enums.Session.CantidadDePedidos.ToString()]);
        }

        #endregion
    }
}