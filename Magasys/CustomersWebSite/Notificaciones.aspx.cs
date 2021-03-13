using BLL.Common;
using System;
using System.Web.Services;
using System.Web.UI;

namespace PL.CustomersWebSite
{
    public partial class Notificaciones : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarNotificaciones();
            }
        }

        #endregion

        #region Métodos Privados

        private void CargarNotificaciones()
        {
            var lstNotificaciones = new BLL.MensajeBLL().ObtenerMensajes(30);
            if (lstNotificaciones != null && lstNotificaciones.Count > 0)
            {
                lsvNotificaciones.DataSource = lstNotificaciones;
                dvMensajeLsvNotificaciones.Visible = false;
            }
            else
            {
                dvMensajeLsvNotificaciones.InnerHtml = MessageManager.Info(dvMensajeLsvNotificaciones, Message.MsjeListadoNotificacionSinResultados, false);
                dvMensajeLsvNotificaciones.Visible = true;
            }

            lsvNotificaciones.DataBind();
            lsvNotificaciones.Visible = true;
        }

        #endregion

        [WebMethod]
        public static bool MarcarNotificacionComoVisto(string pIdMensaje)
        {
            bool loResultado = false;

            if (!string.IsNullOrEmpty(pIdMensaje))
            {
                var oMensajeBLL = new BLL.MensajeBLL();
                var oMensaje = oMensajeBLL.ObtenerMensaje(Convert.ToInt64(pIdMensaje));

                if (oMensaje != null)
                {
                    oMensaje.MENSAJE_VISTO = true;
                    oMensaje.FECHA_MODIFICACION_MENSAJE = DateTime.Now;
                    loResultado = oMensajeBLL.ModificarMensaje(oMensaje);
                }                
            }

            return loResultado;
        }
    }
}