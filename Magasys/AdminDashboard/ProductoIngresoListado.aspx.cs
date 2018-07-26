using System;
using BLL.Filters;
using NLog;
using BLL.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web.UI.HtmlControls;

namespace PL.AdminDashboard
{
    public partial class ProductoIngresoListado : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                txtFechaAltaDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFechaAltaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CargarProveedores();
                CargarGrillaIngresos();
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            lsvIngresos.DataSource = null;
            lsvIngresos.DataBind();
            CargarGrillaIngresos();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void LsvIngresos_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loIdIngresoProductos = ((BLL.ProductoIngresoListado)e.Item.DataItem).ID_INGRESO_PRODUCTOS.ToString();

                HtmlButton btnVisualizar = ((HtmlButton)e.Item.FindControl("btnVisualizar"));
                btnVisualizar.Attributes.Add("value", loIdIngresoProductos);

                HtmlButton btnModificar = ((HtmlButton)e.Item.FindControl("btnModificar"));
                btnModificar.Attributes.Add("value", loIdIngresoProductos);

            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnVisualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Add(Enums.Session.IdIngresoProductos.ToString(), Convert.ToInt64(((HtmlButton)sender).Attributes["value"]));
                Response.Redirect("DetalleProductoIngresos.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Add(Enums.Session.IdIngresoProductos.ToString(), Convert.ToInt64(((HtmlButton)sender).Attributes["value"]));
                Response.Redirect("DetalleProductoIngresosEditar.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        #endregion

        #region Métodos Privados

        private void OcultarDivsMensajes()
        {
            dvMensajeLsvIngresos.Visible = false;

        }

        private void CargarProveedores()
        {
            var oProveedor = new BLL.ProveedorBLL();

            try
            {
                ddlProveedor.DataSource = oProveedor.ObtenerProveedores();
                ddlProveedor.DataTextField = "RAZON_SOCIAL";
                ddlProveedor.DataValueField = "ID_PROVEEDOR";
                ddlProveedor.DataBind();
                ddlProveedor.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private IngresoProductoFiltro CargarIngresoProductoFiltro()
        {
            IngresoProductoFiltro oIngresoProductoFiltro = null;

            if (!(!String.IsNullOrEmpty(txtFechaAltaDesde.Text) && !String.IsNullOrEmpty(txtFechaAltaHasta.Text) && (Convert.ToDateTime(txtFechaAltaDesde.Text) > Convert.ToDateTime(txtFechaAltaHasta.Text))))
            {
                oIngresoProductoFiltro = new IngresoProductoFiltro();

                if (!String.IsNullOrEmpty(ddlProveedor.SelectedValue))
                    oIngresoProductoFiltro.IdProveedor = Convert.ToInt32(ddlProveedor.SelectedValue);

                if (!String.IsNullOrEmpty(txtFechaAltaDesde.Text))
                    oIngresoProductoFiltro.FechaAltaDesde = Convert.ToDateTime(txtFechaAltaDesde.Text);

                if (!String.IsNullOrEmpty(txtFechaAltaHasta.Text))
                    oIngresoProductoFiltro.FechaAltaHasta = Convert.ToDateTime(txtFechaAltaHasta.Text);
            }

            return oIngresoProductoFiltro;
        }

        private void CargarGrillaIngresos()
        {
            try
            {
                var oIngresoProductoFiltro = CargarIngresoProductoFiltro();

                if (oIngresoProductoFiltro != null)
                {
                    var lstProductoIngreso = new BLL.ProductoIngresoBLL().ObtenerProductoIngreso(oIngresoProductoFiltro);

                    if (lstProductoIngreso != null && lstProductoIngreso.Count > 0)
                        lsvIngresos.DataSource = lstProductoIngreso;
                    else
                    {
                        dvMensajeLsvIngresos.InnerHtml = MessageManager.Info(dvMensajeLsvIngresos, Message.MsjeListadoProductoIngresoFiltrarTotalSinResultados, false);
                        dvMensajeLsvIngresos.Visible = true;
                    }
                }
                else
                {
                    dvMensajeLsvIngresos.InnerHtml = MessageManager.Info(dvMensajeLsvIngresos, Message.MsjeListadoProductoIngresoFechaDesdeMayorQueFechaHasta, false);
                    dvMensajeLsvIngresos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lsvIngresos.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            lsvIngresos.DataBind();
        }

        private void LimpiarCampos()
        {
            FormProductoIngresoListado.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            FormProductoIngresoListado.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            lsvIngresos.Visible = false;
            CargarGrillaIngresos();
        }

        #endregion
    }
}