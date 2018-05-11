using System;
using System.Web.UI.WebControls;
using BLL.Common;
using BLL.Filters;
using System.Web.UI.HtmlControls;
using NLog;
using System.Linq;

namespace PL.AdminDashboard
{
    public partial class ProveedorListado : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                CargarGrillaProveedores();
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrillaProveedores();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void LsvProveedores_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loIdProveedor = ((BLL.DAL.Proveedor)e.Item.DataItem).ID_PROVEEDOR.ToString();
                var loCuitProveedor = ((BLL.DAL.Proveedor)e.Item.DataItem).CUIT.ToString();

                HtmlButton btnVisualizar = ((HtmlButton)e.Item.FindControl("btnVisualizar"));
                btnVisualizar.Attributes.Add("value", loIdProveedor);

                HtmlButton btnModificar = ((HtmlButton)e.Item.FindControl("btnModificar"));
                btnModificar.Attributes.Add("value", loIdProveedor);

                HiddenField hdIdCuitProveedorBaja = ((HiddenField)e.Item.FindControl("hdIdCuitProveedorBaja"));
                // Se concatena el IdProveedor y el CUIT:
                hdIdCuitProveedorBaja.Value = string.Format("{0},{1}", loIdProveedor, loCuitProveedor);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Proveedor.aspx", false);
        }

        protected void BtnVisualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var oProveedor = new BLL.ProveedorBLL().ObtenerProveedor(Convert.ToInt64(((HtmlButton)sender).Attributes["value"]));
                Session.Add(Enums.Session.Proveedor.ToString(), oProveedor);
                Response.Redirect("ProveedorVisualizar.aspx", false);
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
                var oProveedor = new BLL.ProveedorBLL().ObtenerProveedor(Convert.ToInt64(((HtmlButton)sender).Attributes["value"]));
                Session.Add(Enums.Session.Proveedor.ToString(), oProveedor);
                Response.Redirect("ProveedorEditar.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnBaja_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(hdIdProveedorBaja.Value))
                {
                    var loIdProveedor = Convert.ToInt64(hdIdProveedorBaja.Value);
                    var oProveedor = new BLL.ProveedorBLL();
                    if (oProveedor.BajaProveedor(loIdProveedor))
                    {
                        CargarGrillaProveedores();
                    }
                }
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
            dvMensajeLsvProveedores.Visible = false;
        }

        private ProveedorFiltro CargarProveedorFiltro()
        {
            ProveedorFiltro oProveedorFiltro = null;

            if (!(!String.IsNullOrEmpty(txtFechaAltaDesde.Text) && !String.IsNullOrEmpty(txtFechaAltaHasta.Text) && (Convert.ToDateTime(txtFechaAltaDesde.Text) > Convert.ToDateTime(txtFechaAltaHasta.Text))))
            {
                oProveedorFiltro = new ProveedorFiltro();

                if (string.IsNullOrEmpty(txtCodigo.Text))
                    oProveedorFiltro.IdProveedor = 0;
                else
                {
                    long loIdProveedor;
                    bool loResultado = long.TryParse(txtCodigo.Text, out loIdProveedor);
                    if (loResultado)
                        oProveedorFiltro.IdProveedor = loIdProveedor;
                    else
                        oProveedorFiltro.IdProveedor = -1;
                }
                if (!String.IsNullOrEmpty(txtCuitBusqueda.Text))
                    oProveedorFiltro.Cuit = txtCuitBusqueda.Text;

                if (!String.IsNullOrEmpty(txtFechaAltaDesde.Text))
                    oProveedorFiltro.FechaAltaDesde = Convert.ToDateTime(txtFechaAltaDesde.Text);

                if (!String.IsNullOrEmpty(txtFechaAltaHasta.Text))
                    oProveedorFiltro.FechaAltaHasta = Convert.ToDateTime(txtFechaAltaHasta.Text);

                if (!String.IsNullOrEmpty(txtRazonSocial.Text))
                    oProveedorFiltro.RazonSocial = txtRazonSocial.Text;
            }

            return oProveedorFiltro;
        }

        private void CargarGrillaProveedores()
        {
            try
            {
                var oProveedorFiltro = CargarProveedorFiltro();

                if (oProveedorFiltro != null)
                {
                    var lstProveedores = new BLL.ProveedorBLL().ObtenerProveedores(oProveedorFiltro);

                    if (lstProveedores != null && lstProveedores.Count > 0)
                        lsvProveedores.DataSource = lstProveedores;
                    else
                    {
                        dvMensajeLsvProveedores.InnerHtml = MessageManager.Info(dvMensajeLsvProveedores, Message.MsjeListadoProveedorFiltrarTotalSinResultados, false);
                        dvMensajeLsvProveedores.Visible = true;
                    }
                }
                else
                {
                    dvMensajeLsvProveedores.InnerHtml = MessageManager.Info(dvMensajeLsvProveedores, Message.MsjeListadoProveedorFechaDesdeMayorQueFechaHasta, false);
                    dvMensajeLsvProveedores.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lsvProveedores.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            lsvProveedores.DataBind();
        }

        private void LimpiarCampos()
        {
            FormProveedorListado.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            CargarGrillaProveedores();
        }

        #endregion        
    }
}