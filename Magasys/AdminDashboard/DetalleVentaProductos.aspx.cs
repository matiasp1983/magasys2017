using System;
using System.Collections.Generic;
using BLL.Common;
using BLL.DAL;

namespace PL.AdminDashboard
{
    public partial class DetalleVentaProductos : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarDetalleVenta();
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

            if (Convert.ToInt32(Session[Enums.Session.IdVenta.ToString()]) > 0)
            {
                loIdVenta = Convert.ToInt32(Session[Enums.Session.IdVenta.ToString()]);
                Session.Remove(Enums.Session.IdVenta.ToString());
            }
            else if (Convert.ToInt32(Session[Enums.Session.CobroVisualizarIdVenta.ToString()]) > 0)
            {
                loIdVenta = Convert.ToInt32(Session[Enums.Session.CobroVisualizarIdVenta.ToString()]);
                Session.Remove(Enums.Session.CobroVisualizarIdVenta.ToString());
            }

            if (loIdVenta > 0)
            {
                using (var loRepVenta = new Repository<BLL.DAL.Venta>())
                {
                    var loVenta = loRepVenta.Find(p => p.ID_VENTA == loIdVenta);

                    if (loVenta != null)
                    {
                        lstVentaListado = new List<BLL.VentaListado>();
                        txtCodigoVenta.Text = loVenta.ID_VENTA.ToString();
                        txtFechaVenta.Text = loVenta.FECHA.ToString("dd/MM/yyyy");
                        txtFormaPago.Text = loVenta.FormaPago.DESCRIPCION;
                        txtEstado.Text = loVenta.Estado.NOMBRE;
                        if (loVenta.Cliente != null)
                        {
                            txtTipoDocumento.Text = loVenta.Cliente.TipoDocumento.DESCRIPCION;
                            txtNumeroDocumento.Text = loVenta.Cliente.NRO_DOCUMENTO.ToString();
                            txtNombre.Text = loVenta.Cliente.NOMBRE.ToString();
                            txtApellido.Text = loVenta.Cliente.APELLIDO.ToString();
                        }
                        lblTotal.Text = loVenta.TOTAL.ToString();

                        foreach (var loDetalleVenta in loVenta.DetalleVenta)
                        {
                            oVentaListado = new BLL.VentaListado
                            {
                                ID_VENTA = loVenta.ID_VENTA,
                                COD_EDICION = loDetalleVenta.COD_PRODUCTO_EDICION,
                                EDICION = loDetalleVenta.ProductoEdicion.EDICION,
                                PRODUCTO = loDetalleVenta.ProductoEdicion.Producto.NOMBRE,
                                TIPO_PRODUCTO = loDetalleVenta.ProductoEdicion.Producto.TipoProducto.DESCRIPCION,
                                PRECIO_UNITARIO = "$" + loDetalleVenta.PRECIO_UNIDAD.ToString(),
                                CANTIDAD = loDetalleVenta.CANTIDAD,
                                SUBTOTAL = "$" + loDetalleVenta.SUBTOTAL.ToString()
                            };

                            if (loDetalleVenta.ProductoEdicion.Producto.COD_TIPO_PRODUCTO == 1)
                                oVentaListado.PRODUCTO = loDetalleVenta.ProductoEdicion.Producto.NOMBRE + " - " + loDetalleVenta.ProductoEdicion.Producto.DESCRIPCION;

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

        #endregion
    }
}