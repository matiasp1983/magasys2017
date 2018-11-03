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

            if (Convert.ToInt32(Session[Enums.Session.IdVenta.ToString()]) > 0)
            {
                using (var loRepVenta = new Repository<BLL.DAL.Venta>())
                {
                    var loIdVenta = Convert.ToInt32(Session[Enums.Session.IdVenta.ToString()]);

                    var loVenta = loRepVenta.Find(p => p.COD_ESTADO == 1 && p.ID_VENTA == loIdVenta);

                    if (loVenta != null)
                    {
                        lstVentaListado = new List<BLL.VentaListado>();

                        foreach (var loDetalleVenta in loVenta.DetalleVenta)
                        {
                            oVentaListado = new BLL.VentaListado
                            {
                                ID_VENTA = loVenta.ID_VENTA,
                                COD_EDICION = loDetalleVenta.COD_PRODUCTO_EDICION,
                                EDICION = loDetalleVenta.ProductoEdicion.EDICION,
                                TIPO_PRODUCTO = loDetalleVenta.ProductoEdicion.Producto.TipoProducto.DESCRIPCION,
                                PRECIO_UNITARIO = loDetalleVenta.PRECIO_UNIDAD,
                                CANTIDAD = loDetalleVenta.CANTIDAD,
                                SUBTOTAL = loDetalleVenta.SUBTOTAL
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

            Session.Remove(Enums.Session.IdVenta.ToString());
        }

        #endregion
    }
}