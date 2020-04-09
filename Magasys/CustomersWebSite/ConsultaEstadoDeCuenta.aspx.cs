using BLL.Common;
using BLL.Filters;
using NLog;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PL.CustomersWebSite
{
    public partial class ConsultaEstadoDeCuenta : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                CargarCantidadDePedidosDesdeSession();
                CargarEstados();
                CargarFormaDePago();
                CargarGrilla();
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void LsvCuentaCorriente_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loIdVenta = ((BLL.VentaListado)e.Item.DataItem).ID_VENTA.ToString();

                HtmlButton btnVisualizar = ((HtmlButton)e.Item.FindControl("btnVisualizar"));
                btnVisualizar.Attributes.Add("value", loIdVenta);
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
                Session.Add(Enums.Session.DetalleVenta.ToString(), Convert.ToInt64(((HtmlButton)sender).Attributes["value"]));
                Response.Redirect("DetalleVentaProductos.aspx", false);
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
            dvMensajeCuentaCorriente.Visible = false;
        }

        private void CargarEstados()
        {
            try
            {
                ddlEstado.DataTextField = "NOMBRE";
                ddlEstado.DataValueField = "ID_ESTADO";
                ddlEstado.DataBind();
                ddlEstado.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlEstado.Items.Insert(1, new ListItem("Pagada", "5"));
                ddlEstado.Items.Insert(2, new ListItem("A Cuenta", "4"));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarFormaDePago()
        {
            var oFormaPagoBLL = new BLL.FormaPagoBLL();

            try
            {
                ddlFormaPago.DataSource = oFormaPagoBLL.ObtenerFormasPago();
                ddlFormaPago.DataTextField = "DESCRIPCION";
                ddlFormaPago.DataValueField = "ID_FORMA_PAGO";
                ddlFormaPago.DataBind();
                ddlFormaPago.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private VentaFiltro CargarCtaCteFiltro()
        {
            VentaFiltro oVentaFiltro = new VentaFiltro();

            var oUsuario = (BLL.DAL.Usuario)Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()];
            var oClienteSession = new BLL.ClienteBLL().ObtenerClientePorUsuario(oUsuario.ID_USUARIO);

            if (oClienteSession != null)
            {
                oVentaFiltro.TIPO_DOCUMENTO = oClienteSession.TIPO_DOCUMENTO;
                oVentaFiltro.NRO_DOCUMENTO = oClienteSession.NRO_DOCUMENTO;

                if (!String.IsNullOrEmpty(ddlFormaPago.SelectedValue))
                    oVentaFiltro.COD_FORMA_PAGO = Convert.ToInt32(ddlFormaPago.SelectedValue);

                if (!String.IsNullOrEmpty(ddlEstado.SelectedValue))
                    oVentaFiltro.COD_ESTADO = Convert.ToInt32(ddlEstado.SelectedValue);

                if (!String.IsNullOrEmpty(txtFechaDesde.Text))
                    oVentaFiltro.FECHAVENTADESDE = Convert.ToDateTime(txtFechaDesde.Text);

                if (!String.IsNullOrEmpty(txtFechaHasta.Text))
                    oVentaFiltro.FECHAVENTAHASTA = Convert.ToDateTime(txtFechaHasta.Text);
            }

            return oVentaFiltro;
        }

        private void CargarGrilla()
        {
            try
            {
                var oVentaFiltro = CargarCtaCteFiltro();

                if (oVentaFiltro != null)
                {
                    var lstVenta = new BLL.VentaBLL().ObtenerVentasPorCliente(oVentaFiltro);

                    if (lstVenta != null && lstVenta.Count > 0)
                    {
                        lsvCuentaCorriente.DataSource = lstVenta;
                        lsvCuentaCorriente.Visible = true;
                    }
                    else
                    {
                        dvMensajeCuentaCorriente.InnerHtml = MessageManager.Info(dvMensajeCuentaCorriente, Message.MsjeSinResultados, false);
                        dvMensajeCuentaCorriente.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lsvCuentaCorriente.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            lsvCuentaCorriente.DataBind();
        }

        private void LimpiarCampos()
        {
            FormCuentaCorriente.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            FormCuentaCorriente.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
        }

        private void CargarCantidadDePedidosDesdeSession()
        {
            Master.CantidadDePedidos = Convert.ToInt32(Session[Enums.Session.CantidadDePedidos.ToString()]);
        }

        #endregion
    }
}