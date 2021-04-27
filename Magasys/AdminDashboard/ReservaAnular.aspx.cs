using BLL.Common;
using BLL.Filters;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class ReservaAnular : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                CargarTipoReserva();
                CargarEstados();
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

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reserva.aspx", false);
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
                ddlEstado.DataSource = oEstado.ObtenerEstados("RESERVA").Where(x=>x.ID_ESTADO == 7 || x.ID_ESTADO == 16);
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

        private ReservaFiltro CargarReservaFiltro()
        {
            ReservaFiltro oReservaFiltro = null;

            oReservaFiltro = new ReservaFiltro();

            if (!String.IsNullOrEmpty(ddlTipoReserva.SelectedValue))
                oReservaFiltro.COD_TIPO_RESERVA = Convert.ToInt32(ddlTipoReserva.SelectedValue);

            if (!String.IsNullOrEmpty(txtNombreProducto.Text))
                oReservaFiltro.NOMBRE_PRODUCTO = txtNombreProducto.Text;

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
            FormReservaAnular.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            FormReservaAnular.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            lsvReservas.Visible = false;
        }

        #endregion
    }
}