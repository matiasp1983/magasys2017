using BLL;
using System;
using System.Web.UI;
using BLL.Common;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using NLog;
using BLL.Filters;
using System.Collections.Generic;

namespace PL.AdminDashboard
{
    public partial class ConfirmarReservas : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            dvMensajeLsvReservasPriodicas.Visible = false;
            dvMensajeLsvReservasUnicas.Visible = false;

            if (!Page.IsPostBack)
                CargarGrilla();
        }

        protected void BtnGuardarReservaPeriodica_Click(object sender, EventArgs e)
        {
            bool loResutado = false;
            bool loHayParaProcesar = false;

            if (lsvReservaPeriodica.Controls.Count == 0)
            {
                dvMensajeLsvReservasPriodicas.InnerHtml = MessageManager.Info(dvMensajeLsvReservasPriodicas, Message.MsjeReservaSinConfirmar, false);
                dvMensajeLsvReservasPriodicas.Visible = true;
                return;
            }

            try
            {
                foreach (var loItem in lsvReservaPeriodica.Items)
                {
                    if (((HtmlInputCheckBox)loItem.Controls[1]).Checked)
                    {
                        loHayParaProcesar = true;
                        BLL.DAL.Reserva oReservaConfirmada = new ReservaBLL().ObtenerReserva(Convert.ToInt32(((Label)loItem.Controls[3]).Text));
                        oReservaConfirmada.COD_ESTADO = 7;
                        loResutado = new ReservaBLL().ModificarReserva(oReservaConfirmada);
                        if (!loResutado)
                            break;

                        // Informar al Cliente que la edición ha sido entregada.
                        BLL.DAL.Mensaje oMensaje = new BLL.DAL.Mensaje()
                        {
                            COD_CLIENTE = Convert.ToInt32(((Label)loItem.Controls[13]).Text),
                            DESCRIPCION = "La reserva " + ((Label)loItem.Controls[3]).Text + " del producto '" + ((Label)loItem.Controls[9]).Text + "' ha sido confirmada.",
                            TIPO_MENSAJE = "success-element",
                            FECHA_REGISTRO_MENSAJE = DateTime.Now
                        };

                        loResutado = new MensajeBLL().AltaMensaje(oMensaje);
                        if (!loResutado)
                            break;
                    }
                }

                if (loHayParaProcesar)
                {
                    if (loResutado)
                    {
                        CargarGrilla();
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeReservaConfirmacionOk, "Confirmación Reservas"));
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaConfirmacionFailure));
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeRepartoInfo));

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaConfirmacionFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnGuardarReservaUnica_Click(object sender, EventArgs e)
        {
            bool loResutado = false;
            bool loHayParaProcesar = false;

            if (lsvReservaUnica.Controls.Count == 0)
            {
                dvMensajeLsvReservasUnicas.InnerHtml = MessageManager.Info(dvMensajeLsvReservasUnicas, Message.MsjeReservaSinConfirmar, false);
                dvMensajeLsvReservasUnicas.Visible = true;
                return;
            }

            try
            {
                foreach (var loItem in lsvReservaUnica.Items)
                {
                    if (((HtmlInputCheckBox)loItem.Controls[1]).Checked)
                    {
                        loHayParaProcesar = true;
                        BLL.DAL.Reserva oReservaConfirmada = new ReservaBLL().ObtenerReserva(Convert.ToInt32(((Label)loItem.Controls[3]).Text));
                        oReservaConfirmada.COD_ESTADO = 7;
                        loResutado = new ReservaBLL().ModificarReserva(oReservaConfirmada);
                        if (!loResutado)
                            break;

                        // Informar al Cliente que la edición ha sido entregada.
                        BLL.DAL.Mensaje oMensaje = new BLL.DAL.Mensaje()
                        {
                            COD_CLIENTE = Convert.ToInt32(((Label)loItem.Controls[15]).Text),
                            DESCRIPCION = "La reserva " + ((Label)loItem.Controls[3]).Text + " del producto '" + ((Label)loItem.Controls[11]).Text + "', edición " + ((Label)loItem.Controls[9]).Text + ", ha sido confirmada.",
                            TIPO_MENSAJE = "success-element",
                            FECHA_REGISTRO_MENSAJE = DateTime.Now
                        };

                        loResutado = new MensajeBLL().AltaMensaje(oMensaje);
                        if (!loResutado)
                            break;

                        BLL.DAL.ReservaEdicion oReservaEdicion = new ReservaEdicionBLL().ObtenerReservaEdicionDeReservaUnica(oReservaConfirmada.ID_RESERVA);
                        if (oReservaEdicion != null)
                        {
                            oReservaEdicion.COD_ESTADO = 15;
                            loResutado = new ReservaEdicionBLL().ModificarReservaEdidion(oReservaEdicion);
                            // Actualizar stock (se reserva stock)
                            loResutado = new ProductoEdicionBLL().ActualizarCantidadDisponible(oReservaEdicion.COD_PROD_EDICION, 1);
                            if (!loResutado)
                                break;
                        }

                        if (!loResutado)
                            break;
                    }
                }

                if (loHayParaProcesar)
                {
                    if (loResutado)
                    {
                        CargarGrilla();
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeReservaConfirmacionOk, "Confirmación Reservas"));
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaConfirmacionFailure));
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeRepartoInfo));
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaConfirmacionFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reserva.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarGrilla()
        {
            List<BLL.ReservaListado> lstReservasPeriodicas = null;
            List<BLL.ReservaListado> lstReservasUnicas = null;

            ReservaFiltro oReservaFiltro = new ReservaFiltro();
            oReservaFiltro.COD_ESTADO = 16;
            oReservaFiltro.COD_TIPO_RESERVA = 2;

            lstReservasPeriodicas = new ReservaBLL().ObtenerReservas(oReservaFiltro);

            lsvReservaPeriodica.DataSource = lstReservasPeriodicas;
            lsvReservaPeriodica.DataBind();

            if (lstReservasPeriodicas.Count == 0)
            {
                dvMensajeLsvReservasPriodicas.InnerHtml = MessageManager.Info(dvMensajeLsvReservasPriodicas, Message.MsjeReservaSinConfirmar, false);
                dvMensajeLsvReservasPriodicas.Visible = true;
            }

            oReservaFiltro.COD_TIPO_RESERVA = 1;

            lstReservasUnicas = new ReservaBLL().ObtenerReservas(oReservaFiltro);

            lsvReservaUnica.DataSource = lstReservasUnicas;
            lsvReservaUnica.DataBind();

            if (lstReservasUnicas.Count == 0)
            {
                dvMensajeLsvReservasUnicas.InnerHtml = MessageManager.Info(dvMensajeLsvReservasUnicas, Message.MsjeReservaSinConfirmar, false);
                dvMensajeLsvReservasUnicas.Visible = true;
            }
        }

        #endregion
    }
}