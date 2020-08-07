using BLL;
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
                        BLL.DAL.Reserva oReservaConfirmada = new BLL.ReservaBLL().ObtenerReserva(Convert.ToInt32(((Label)loItem.Controls[3]).Text));
                        oReservaConfirmada.COD_ESTADO = 7;


                        if (oReservaConfirmada.COD_TIPO_RESERVA == 1)
                        {
                            BLL.DAL.ReservaEdicion oReservaEdicion = new BLL.ReservaEdicionBLL().ObtenerReservaEdicionDeReservaUnica(oReservaConfirmada.ID_RESERVA);
                            if (oReservaEdicion != null)
                            {
                                oReservaEdicion.COD_ESTADO = 15;
                                loResutado = new BLL.ReservaEdicionBLL().ModificarReservaEdidion(oReservaEdicion);
                                loResutado = new BLL.ReservaBLL().ModificarReserva(oReservaConfirmada);
                            }
                            else
                            {
                                loResutado = false;
                            }
                        }
                        else
                        {
                            loResutado = new BLL.ReservaBLL().ModificarReserva(oReservaConfirmada);
                        }

                        // loResutado = true;
                        if (!loResutado)
                            break;
                    }
                }

                if (loResutado)
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeReservaConfirmacionOk, "Confirmacion Reservas", "Index.aspx"));
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
            Response.Redirect("ProductoIngreso.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarGrilla() // EL SIGUIENTE CÓDIGO ES DE EJEMPLO PARA PODER VISUALIZAR LA GRILLA!!
        {
            //ListView lsvReservas = (ListView)Session[Enums.Session.ListadoReservaConfirmar.ToString()];
            ReservaFiltro oReservaFiltro = new ReservaFiltro();
            oReservaFiltro.COD_ESTADO = 16;

            var lstReservasConfirmar = new BLL.ReservaBLL().ObtenerReservas(oReservaFiltro);

            lsvReservaEdicion.DataSource = lstReservasConfirmar;
            lsvReservaEdicion.DataBind();

            if (lstReservasConfirmar.Count == 0)
            {
                dvMensajeLsvReservas.InnerHtml = MessageManager.Info(dvMensajeLsvReservas, Message.MsjeReservaSinConfirmar, false);
                dvMensajeLsvReservas.Visible = true;
            }
        }

        private List<ReservaClienteListado> MapListViewToListObject(ListView pListView)
        {
            List<ReservaClienteListado> lstReservas = (List<ReservaClienteListado>)pListView.DataSource;

            return lstReservas;
        }

        #endregion
    }
}