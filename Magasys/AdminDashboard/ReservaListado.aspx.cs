using BLL.Common;
using BLL.DAL;
using BLL.Filters;
using NLog;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class ReservaListado : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                CargarTipoReserva();
                CargarEstados();
                CargarFormaDeEntrega();
                CargarTiposDocumento();
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

        protected void LsvReservas_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loIdReserva = ((BLL.ReservaListado)e.Item.DataItem).ID_RESERVA.ToString();

                HtmlButton btnVisualizar = ((HtmlButton)e.Item.FindControl("btnVisualizar"));
                btnVisualizar.Attributes.Add("value", loIdReserva);

                HtmlButton btnModificar = ((HtmlButton)e.Item.FindControl("btnModificar"));
                btnModificar.Attributes.Add("value", loIdReserva);

                HiddenField hdIdReserva = ((HiddenField)e.Item.FindControl("hdIdReserva"));
                hdIdReserva.Value = loIdReserva.ToString();
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
                Session.Add(Enums.Session.IdReserva.ToString(), ((HtmlButton)sender).Attributes["value"]);
                Response.Redirect("ReservaVisualizar.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlEstado.SelectedValue) == 8)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaFinalizadaNoEditar)); //"La reserva Finalizada no se puede modificar."
                return;
            }
            else if (Convert.ToInt32(ddlEstado.SelectedValue) == 9)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaAnuladaNoEditar)); //"La reserva Anulada no se puede modificar."
                return;
            }

            try
            {
                Session.Add(Enums.Session.IdReserva.ToString(), ((HtmlButton)sender).Attributes["value"]);
                Response.Redirect("ReservaEditar.aspx", false);
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
            BLL.DAL.Reserva loReserva = new BLL.DAL.Reserva();

            try
            {
                if (!String.IsNullOrEmpty(hdIdReservaAnular.Value))
                {
                    var loIdReserva = Convert.ToInt32(hdIdReservaAnular.Value);
                    using (var loRepReserva = new Repository<BLL.DAL.Reserva>())
                    {
                        loReserva = loRepReserva.Find(p => p.ID_RESERVA == loIdReserva);

                        if (loReserva.COD_ESTADO == 8)
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaNoSePuedeAnular)); //"La reserva está Finalizada, no se puede Anular."
                            return;
                        }
                        else if (loReserva.COD_ESTADO == 9)
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaAnulada)); //"La reserva ya se encuentra Anulada."
                            return;
                        }

                        loReserva.COD_ESTADO = 9; // Estado Anulada
                    }

                    loResultado = new BLL.ReservaBLL().ModificarReserva(loReserva);
                    // FALTA AGREGAR ACTUALIZAR STOCK
                }

                if (loResultado)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeReservaAnularOk));
                    CargarGrilla();
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaAnularFailure));
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
            dvMensajeLsvReservas.Visible = false;
        }

        private void CargarTipoReserva()
        {
            var oReservaBLL = new BLL.ReservaBLL();

            try
            {
                ddlTipoReserva.DataSource = oReservaBLL.ObtenerTipoReserva();
                ddlTipoReserva.DataTextField = "DESCRIPCION";
                ddlTipoReserva.DataValueField = "ID_TIPO_RESERVA";
                ddlTipoReserva.DataBind();
                ddlTipoReserva.Items.Insert(0, new ListItem(String.Empty, String.Empty));
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
            var oEstado = new BLL.EstadoBLL();

            try
            {
                ddlEstado.DataSource = oEstado.ObtenerEstados("RESERVA");
                ddlEstado.DataTextField = "NOMBRE";
                ddlEstado.DataValueField = "ID_ESTADO";
                ddlEstado.DataBind();
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarFormaDeEntrega()
        {
            try
            {
                ddlFormaEntrega.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlFormaEntrega.Items.Insert(1, "Retira en Local");
                ddlFormaEntrega.Items.Insert(2, "Envío a Domicilio");
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private ReservaFiltro CargarReservaFiltro()
        {
            ReservaFiltro oReservaFiltro = null;

            if (!String.IsNullOrEmpty(txtFechaIniReservaDesde.Text) && !String.IsNullOrEmpty(txtFechaIniReservaHasta.Text) && (Convert.ToDateTime(txtFechaIniReservaDesde.Text) > Convert.ToDateTime(txtFechaIniReservaHasta.Text)))
                return oReservaFiltro;

            if (!String.IsNullOrEmpty(txtFechaFinReservaDesde.Text) && !String.IsNullOrEmpty(txtFechaFinReservaHasta.Text) && (Convert.ToDateTime(txtFechaFinReservaDesde.Text) > Convert.ToDateTime(txtFechaFinReservaHasta.Text)))
                return oReservaFiltro;

            oReservaFiltro = new ReservaFiltro();

            if (!String.IsNullOrEmpty(txtFechaIniReservaDesde.Text))
                oReservaFiltro.FECHAINICIORESERVADESDE = Convert.ToDateTime(txtFechaIniReservaDesde.Text);

            if (!String.IsNullOrEmpty(txtFechaIniReservaHasta.Text))
                oReservaFiltro.FECHAINICIORESERVAHASTA = Convert.ToDateTime(txtFechaIniReservaHasta.Text);

            if (!String.IsNullOrEmpty(txtFechaFinReservaDesde.Text))
                oReservaFiltro.FECHAFINRESERVADESDE = Convert.ToDateTime(txtFechaFinReservaDesde.Text);

            if (!String.IsNullOrEmpty(txtFechaFinReservaHasta.Text))
                oReservaFiltro.FECHAFINRESERVAHASTA = Convert.ToDateTime(txtFechaFinReservaHasta.Text);

            if (!String.IsNullOrEmpty(ddlTipoReserva.SelectedValue))
                oReservaFiltro.COD_TIPO_RESERVA = Convert.ToInt32(ddlTipoReserva.SelectedValue);

            if (!String.IsNullOrEmpty(txtNombreProducto.Text))
                oReservaFiltro.NOMBRE_PRODUCTO = txtNombreProducto.Text;

            if (!String.IsNullOrEmpty(ddlFormaEntrega.SelectedValue))
                oReservaFiltro.COD_FORMA_ENTREGA = ddlFormaEntrega.SelectedValue;

            if (!String.IsNullOrEmpty(ddlEstado.SelectedValue))
                oReservaFiltro.COD_ESTADO = Convert.ToInt32(ddlEstado.SelectedValue);

            if (!String.IsNullOrEmpty(ddlTipoDocumento.SelectedValue) && !String.IsNullOrEmpty(txtNroDocumento.Text))
            {
                oReservaFiltro.TIPO_DOCUMENTO = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
                oReservaFiltro.NRO_DOCUMENTO = Convert.ToInt32(txtNroDocumento.Text);
            }

            if (!String.IsNullOrEmpty(txtNombre.Text))
                oReservaFiltro.NOMBRE = txtNombre.Text;

            if (!String.IsNullOrEmpty(txtApellido.Text))
                oReservaFiltro.APELLIDO = txtApellido.Text;

            if (!String.IsNullOrEmpty(txtAlias.Text))
                oReservaFiltro.ALIAS = txtAlias.Text;

            return oReservaFiltro;
        }

        private void CargarGrilla()
        {
            try
            {
                var oReservaFiltro = CargarReservaFiltro();

                if (oReservaFiltro != null)
                {
                    var lstReserva = new BLL.ReservaBLL().ObtenerReservas(oReservaFiltro);

                    if (lstReserva != null && lstReserva.Count > 0)
                    {
                        lsvReservas.DataSource = lstReserva;
                        lsvReservas.Visible = true;
                    }

                    else
                    {
                        dvMensajeLsvReservas.InnerHtml = MessageManager.Info(dvMensajeLsvReservas, Message.MsjeListadoReservaFiltroSinResultados, false);
                        dvMensajeLsvReservas.Visible = true;
                    }
                }
                else
                {
                    dvMensajeLsvReservas.InnerHtml = MessageManager.Info(dvMensajeLsvReservas, Message.MsjeListadoFechaDesdeMayorQueFechaHasta, false);
                    dvMensajeLsvReservas.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lsvReservas.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            lsvReservas.DataBind();
        }

        private void LimpiarCampos()
        {
            FormReservaListado.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            FormReservaListado.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            lsvReservas.Visible = false;
        }

        #endregion
    }
}