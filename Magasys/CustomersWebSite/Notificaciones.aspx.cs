using BLL.Common;
using System;
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
    }
}