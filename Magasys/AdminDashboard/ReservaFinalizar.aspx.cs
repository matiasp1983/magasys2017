using BLL.Common;
using System;
using System.Web.UI;

namespace PL.AdminDashboard
{
    public partial class ReservaFinalizar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnFinalizarReserva_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtCodigo.Text))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaCargarCodigo)); //"Se debe completar el código de la reserva."
                return;
            }

            var lvIdReserva = Convert.ToInt32(txtCodigo.Text);
            var oReserva = new BLL.ReservaBLL().ObtenerReserva(lvIdReserva);

            if (oReserva == null)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaNoExiste)); //"La reserva ingresada no existe."
                return;
            }

            if (oReserva.COD_ESTADO == 7)
            {
                if (oReserva.FECHA_FIN >= DateTime.Now.Date)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaNoCaducada)); //"La reserva no ha caducado."
                    return;
                }

                oReserva.COD_ESTADO = 8;
                var loResutado = new BLL.ReservaBLL().ModificarReserva(oReserva);

                if (loResutado)
                {
                    txtCodigo.Text = String.Empty;
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeReservaSuccessModificacion)); //"La reserva se actualizó correctamente."
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaErrorFinalizar)); //"La reserva no se pudo finalizar."
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaNoSePuedeFinalizar)); //"La reserva no se pudo finalizar, no cuenta con el estado requerido."
            }
        }

        protected void BtnEjecutar_Click(object sender, EventArgs e)
        {
            var loResutado = new BLL.ReservaBLL().FinalizarReservas();

            if (loResutado)
            {
                txtCodigo.Text = String.Empty;
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeReservaProcesamientoFinalizado)); //"El procesamiento finalizó con éxito."
            }
            else
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaSinCaducar)); //"No hay reservas para procesar."
        }

        #endregion
    }
}