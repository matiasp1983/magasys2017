using System;
using NLog;
using BLL.Common;
using BLL.Filters;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BLL;
using BLL.DAL;
using System.Collections.Generic;
using System.Linq;

namespace PL.AdminDashboard
{
    public partial class VentaListado : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                txtFechaVentaDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFechaVentaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CargarFormaDePago();
                CargarTiposDocumento();
                CargarEstados();
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

        protected void LsvVentas_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loIdVenta = ((BLL.VentaListado)e.Item.DataItem).ID_VENTA.ToString();

                HtmlButton btnVisualizar = ((HtmlButton)e.Item.FindControl("btnVisualizar"));
                btnVisualizar.Attributes.Add("value", loIdVenta);

                HiddenField hdIdVenta = ((HiddenField)e.Item.FindControl("hdIdVenta"));
                hdIdVenta.Value = loIdVenta.ToString();

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
                Session.Add(Enums.Session.IdVenta.ToString(), Convert.ToInt64(((HtmlButton)sender).Attributes["value"]));
                Response.Redirect("DetalleVentaProductos.aspx", false);
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
                if (!String.IsNullOrEmpty(hdIdVentaAnular.Value))
                {
                    var loIdVenta = Convert.ToInt32(hdIdVentaAnular.Value);
                    loResultado = new BLL.VentaBLL().AnularVenta(loIdVenta);
                }

                if (loResultado)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeVentaAnularOk));
                    CargarGrilla();
                }

                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaAnularFailure));

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
            dvMensajeLsvVentas.Visible = false;
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

        private void CargarTiposDocumento()
        {
            var oTipoDocumento = new BLL.TipoDocumentoBLL();

            try
            {
                ddlTipoDocumento.DataSource = oTipoDocumento.ObtenerTiposDocumento();
                ddlTipoDocumento.DataTextField = "DESCRIPCION";
                ddlTipoDocumento.DataValueField = "ID_TIPO_DOCUMENTO";
                ddlTipoDocumento.DataBind();
                ddlTipoDocumento.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarEstados()
        {
            try
            {
                ddlEstado.DataTextField = "NOMBRE";
                ddlEstado.DataValueField = "ID_ESTADO";
                ddlEstado.DataBind();
                ddlEstado.Items.Insert(0, new ListItem("Pagada", "5"));
                ddlEstado.Items.Insert(1, new ListItem("A Cuenta", "4"));
                ddlEstado.Items.Insert(2, new ListItem("Anulado", "3"));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private VentaFiltro CargarVentaFiltro()
        {
            VentaFiltro oVentaFiltro = null;

            if (!(!String.IsNullOrEmpty(txtFechaVentaDesde.Text) && !String.IsNullOrEmpty(txtFechaVentaHasta.Text) && (Convert.ToDateTime(txtFechaVentaDesde.Text) > Convert.ToDateTime(txtFechaVentaHasta.Text))))
            {
                oVentaFiltro = new VentaFiltro();

                if (!String.IsNullOrEmpty(txtFechaVentaDesde.Text))
                    oVentaFiltro.FECHAVENTADESDE = Convert.ToDateTime(txtFechaVentaDesde.Text);

                if (!String.IsNullOrEmpty(txtFechaVentaHasta.Text))
                    oVentaFiltro.FECHAVENTAHASTA = Convert.ToDateTime(txtFechaVentaHasta.Text);

                if (!String.IsNullOrEmpty(txtCodigoVenta.Text))
                    oVentaFiltro.ID_VENTA = Convert.ToInt32(txtCodigoVenta.Text);

                if (!String.IsNullOrEmpty(ddlFormaPago.SelectedValue))
                    oVentaFiltro.COD_FORMA_PAGO = Convert.ToInt32(ddlFormaPago.SelectedValue);

                oVentaFiltro.COD_ESTADO = Convert.ToInt32(ddlEstado.SelectedValue);

                if (!String.IsNullOrEmpty(ddlTipoDocumento.SelectedValue) && !String.IsNullOrEmpty(txtNroDocumento.Text))
                {
                    oVentaFiltro.TIPO_DOCUMENTO = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
                    oVentaFiltro.NRO_DOCUMENTO = Convert.ToInt32(txtNroDocumento.Text);
                }

                if (!String.IsNullOrEmpty(txtNombre.Text))
                    oVentaFiltro.NOMBRE = txtNombre.Text;

                if (!String.IsNullOrEmpty(txtApellido.Text))
                    oVentaFiltro.APELLIDO = txtApellido.Text;

                if (!String.IsNullOrEmpty(txtAlias.Text))
                    oVentaFiltro.ALIAS = txtAlias.Text;
            }

            return oVentaFiltro;
        }

        private void CargarGrilla()
        {
            try
            {
                var oVentaFiltro = CargarVentaFiltro();

                if (oVentaFiltro != null)
                {
                    var lstVenta = new BLL.VentaBLL().ObtenerVentas(oVentaFiltro);

                    if (lstVenta != null && lstVenta.Count > 0)
                    {
                        lsvVentas.DataSource = lstVenta;
                        lsvVentas.Visible = true;
                    }

                    else
                    {
                        dvMensajeLsvVentas.InnerHtml = MessageManager.Info(dvMensajeLsvVentas, Message.MsjeListadoVentaFiltrarTotalSinResultados, false);
                        dvMensajeLsvVentas.Visible = true;
                    }
                }
                else
                {
                    dvMensajeLsvVentas.InnerHtml = MessageManager.Info(dvMensajeLsvVentas, Message.MsjeListadoFechaDesdeMayorQueFechaHasta, false);
                    dvMensajeLsvVentas.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lsvVentas.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            lsvVentas.DataBind();
        }

        private void LimpiarCampos()
        {
            FormVentaListado.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            FormVentaListado.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            lsvVentas.Visible = false;
        }

        #endregion
    }
}