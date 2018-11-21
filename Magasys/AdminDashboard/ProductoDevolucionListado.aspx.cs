using System;
using NLog;
using BLL.Filters;
using BLL.Common;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace PL.AdminDashboard
{
    public partial class ProductoDevolucionListado : System.Web.UI.Page
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
                CargarGrillaDevoluciones();
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrillaDevoluciones();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void LsvDevoluciones_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loIdProductoDevolucion = ((BLL.ProductoDevolucionListado)e.Item.DataItem).ID_PRODUCTO_DEVOLUCION.ToString();

                HtmlButton btnVisualizar = ((HtmlButton)e.Item.FindControl("btnVisualizar"));
                btnVisualizar.Attributes.Add("value", loIdProductoDevolucion);

                HiddenField hdIdDevolucion = ((HiddenField)e.Item.FindControl("hdIdDevolucion"));
                hdIdDevolucion.Value = loIdProductoDevolucion.ToString();

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
                Session.Add(Enums.Session.IdProductoDevolucion.ToString(), Convert.ToInt64(((HtmlButton)sender).Attributes["value"]));
                Response.Redirect("DetalleProductoDevoluciones.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnAnular_Click(object sender, EventArgs e)
        {
            bool loResultado = false;

            try
            {
                if (!String.IsNullOrEmpty(hdIdDevolucionAnular.Value))
                {
                    var loIdProductoDevolucion = Convert.ToInt32(hdIdDevolucionAnular.Value);
                    loResultado = new BLL.ProductoDevolucionBLL().AnularDevolucion(loIdProductoDevolucion);
                }

                if (loResultado)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeDevolucionAnularOk));
                    CargarGrillaDevoluciones();
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeDevolucionAnularFailure));
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
            dvMensajeLsvDevoluciones.Visible = false;
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

        private IngresoProductoFiltro CargarDevolucionFiltro()
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

        private void CargarGrillaDevoluciones()
        {
            try
            {
                var oDevolucionFiltro = CargarDevolucionFiltro();

                if (oDevolucionFiltro != null)
                {
                    var lstDevoluciones = new BLL.ProductoDevolucionBLL().ObtenerDevolucion(oDevolucionFiltro);

                    if (lstDevoluciones != null && lstDevoluciones.Count > 0)
                    {
                        lsvDevoluciones.Visible = true;
                        lsvDevoluciones.DataSource = lstDevoluciones;
                    }
                    else
                    {
                        dvMensajeLsvDevoluciones.InnerHtml = MessageManager.Info(dvMensajeLsvDevoluciones, Message.MsjeListadoDevolucionListadoSinResultados, false);
                        dvMensajeLsvDevoluciones.Visible = true;
                    }
                }
                else
                {
                    dvMensajeLsvDevoluciones.InnerHtml = MessageManager.Info(dvMensajeLsvDevoluciones, Message.MsjeListadoFechaDesdeMayorQueFechaHasta, false);
                    dvMensajeLsvDevoluciones.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lsvDevoluciones.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            lsvDevoluciones.DataBind();
        }

        private void LimpiarCampos()
        {
            FormProductoDevolucionListado.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            FormProductoDevolucionListado.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            lsvDevoluciones.Visible = false;
        }

        #endregion
    }
}