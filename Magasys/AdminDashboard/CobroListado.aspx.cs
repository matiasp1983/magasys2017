using BLL.Common;
using BLL.Filters;
using NLog;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class CobroListado : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                txtFechaCobroDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFechaCobroHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
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

        protected void LsvCobros_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loIdCobro = ((BLL.CobroListado)e.Item.DataItem).ID_COBRO.ToString();

                HtmlButton btnVisualizar = ((HtmlButton)e.Item.FindControl("btnVisualizar"));
                btnVisualizar.Attributes.Add("value", loIdCobro);

                HiddenField hdIdCobro = ((HiddenField)e.Item.FindControl("hdIdCobro"));
                hdIdCobro.Value = loIdCobro.ToString();

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
                Session.Add(Enums.Session.IdCobro.ToString(), Convert.ToInt64(((HtmlButton)sender).Attributes["value"]));
                Response.Redirect("DetalleCobro.aspx", false);
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
                if (!String.IsNullOrEmpty(hdIdCobroAnular.Value))
                {
                    var loIdCobro = Convert.ToInt32(hdIdCobroAnular.Value);
                    loResultado = new BLL.CobroBLL().AnularCobro(loIdCobro);
                }

                if (loResultado)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeCobroAnularOk));
                    CargarGrilla();
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeCobroAnularFailure));
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
            dvMensajeLsvCobros.Visible = false;
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
                ddlEstado.Items.Insert(0, new ListItem("Registrado", "13"));
                ddlEstado.Items.Insert(1, new ListItem("Anulado", "14"));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private CobroFiltro CargarCobroFiltro()
        {
            CobroFiltro oCobroFiltro = null;

            if (!(!String.IsNullOrEmpty(txtFechaCobroDesde.Text) && !String.IsNullOrEmpty(txtFechaCobroHasta.Text) && (Convert.ToDateTime(txtFechaCobroDesde.Text) > Convert.ToDateTime(txtFechaCobroHasta.Text))))
            {
                oCobroFiltro = new CobroFiltro();

                if (!String.IsNullOrEmpty(txtFechaCobroDesde.Text))
                    oCobroFiltro.FECHACOBRODESDE = Convert.ToDateTime(txtFechaCobroDesde.Text);

                if (!String.IsNullOrEmpty(txtFechaCobroHasta.Text))
                    oCobroFiltro.FECHACOBROHASTA = Convert.ToDateTime(txtFechaCobroHasta.Text);

                if (!String.IsNullOrEmpty(txtCodigoCobro.Text))
                    oCobroFiltro.ID_COBRO = Convert.ToInt32(txtCodigoCobro.Text);

                oCobroFiltro.COD_ESTADO = Convert.ToInt32(ddlEstado.SelectedValue);

                if (!String.IsNullOrEmpty(ddlTipoDocumento.SelectedValue) && !String.IsNullOrEmpty(txtNroDocumento.Text))
                {
                    oCobroFiltro.TIPO_DOCUMENTO = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
                    oCobroFiltro.NRO_DOCUMENTO = Convert.ToInt32(txtNroDocumento.Text);
                }

                if (!String.IsNullOrEmpty(txtNombre.Text))
                    oCobroFiltro.NOMBRE = txtNombre.Text;

                if (!String.IsNullOrEmpty(txtApellido.Text))
                    oCobroFiltro.APELLIDO = txtApellido.Text;

                if (!String.IsNullOrEmpty(txtAlias.Text))
                    oCobroFiltro.ALIAS = txtAlias.Text;
            }

            return oCobroFiltro;
        }

        private void CargarGrilla()
        {
            try
            {
                var oCobroFiltro = CargarCobroFiltro();

                if (oCobroFiltro != null)
                {
                    var lstCobro = new BLL.CobroBLL().ObtenerCobros(oCobroFiltro);

                    if (lstCobro != null && lstCobro.Count > 0)
                    {
                        lsvCobros.DataSource = lstCobro;
                        lsvCobros.Visible = true;
                    }

                    else
                    {
                        dvMensajeLsvCobros.InnerHtml = MessageManager.Info(dvMensajeLsvCobros, Message.MsjeListadoCobroFiltrarTotalSinResultados, false);
                        dvMensajeLsvCobros.Visible = true;
                    }
                }
                else
                {
                    dvMensajeLsvCobros.InnerHtml = MessageManager.Info(dvMensajeLsvCobros, Message.MsjeListadoFechaDesdeMayorQueFechaHasta, false);
                    dvMensajeLsvCobros.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lsvCobros.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            lsvCobros.DataBind();
        }

        private void LimpiarCampos()
        {
            FormCobroListado.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            FormCobroListado.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            lsvCobros.Visible = false;
        }

        #endregion
    }
}