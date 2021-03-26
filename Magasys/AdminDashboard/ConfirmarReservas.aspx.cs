﻿using BLL;
using System;
using System.Collections.Generic;
using System.Web.UI;
using BLL.Common;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using NLog;
using BLL.Filters;

namespace PL.AdminDashboard
{
    public partial class ConfirmarReservas : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            dvMensajeLsvReservas.Visible = false;

            if (!Page.IsPostBack)
                CargarGrilla();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            bool loResutado = false;

            if (lsvReservaEdicion.Controls.Count == 0)
            {
                dvMensajeLsvReservas.InnerHtml = MessageManager.Info(dvMensajeLsvReservas, Message.MsjeReservaSinConfirmar, false);
                dvMensajeLsvReservas.Visible = true;
                return;
            }

            List<ReservaClienteListado> lstReservasConfirmar = (List<ReservaClienteListado>)lsvReservaEdicion.DataSource;

            try
            {
                foreach (var loItem in lsvReservaEdicion.Items)
                {
                    if (((HtmlInputCheckBox)loItem.Controls[1]).Checked)
                    {
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

                        // Consultar con los chicos: El siguiente código se comenta porque me parece que una Reserva Registrada no puede tener
                        // ediciones
                        //if (oReservaConfirmada.COD_TIPO_RESERVA == 1)
                        //{
                        //    BLL.DAL.ReservaEdicion oReservaEdicion = new BLL.ReservaEdicionBLL().ObtenerReservaEdicionDeReservaUnica(oReservaConfirmada.ID_RESERVA);
                        //    if (oReservaEdicion != null)
                        //    {
                        //        oReservaEdicion.COD_ESTADO = 15;
                        //        loResutado = new ReservaEdicionBLL().ModificarReservaEdidion(oReservaEdicion);
                        //        loResutado = new ReservaBLL().ModificarReserva(oReservaConfirmada);
                        //    }
                        //}

                        if (!loResutado)
                            break;
                    }
                }

                if (loResutado)
                {
                    CargarGrilla();
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeReservaConfirmacionOk, "Confirmación Reservas"));
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaConfirmacionFailure));
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
            ReservaFiltro oReservaFiltro = new ReservaFiltro();
            oReservaFiltro.COD_ESTADO = 16;

            var lstReservasConfirmar = new ReservaBLL().ObtenerReservas(oReservaFiltro);

            lsvReservaEdicion.DataSource = lstReservasConfirmar;
            lsvReservaEdicion.DataBind();

            if (lstReservasConfirmar.Count == 0)
            {
                dvMensajeLsvReservas.InnerHtml = MessageManager.Info(dvMensajeLsvReservas, Message.MsjeReservaSinConfirmar, false);
                dvMensajeLsvReservas.Visible = true;
            }
        }

        #endregion
    }
}