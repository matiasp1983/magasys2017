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
                var oUsuario = (BLL.DAL.Usuario)Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()];
                var oCliente = new BLL.ClienteBLL().ObtenerClientePorUsuario(oUsuario.ID_USUARIO);
                MarcarNotificacionComoVisto(oCliente.ID_CLIENTE);
                CargarNotificaciones();
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            var oUsuario = (BLL.DAL.Usuario)Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()];
            var oCliente = new BLL.ClienteBLL().ObtenerClientePorUsuario(oUsuario.ID_USUARIO);
            new BLL.DAL.MAGASYSEntities().EliminarNotificacionesPorCiente(oCliente.ID_CLIENTE);
            CargarNotificaciones();
        }

        #endregion

        #region Métodos Privados

        private void CargarNotificaciones()
        {
            var oUsuario = (BLL.DAL.Usuario)Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()];
            var oClienteSession = new BLL.ClienteBLL().ObtenerClientePorUsuario(oUsuario.ID_USUARIO);
            var lstNotificaciones = new BLL.MensajeBLL().ObtenerMensajes(oClienteSession.ID_CLIENTE, ddlNotificaciones.SelectedValue);
            if (lstNotificaciones != null && lstNotificaciones.Count > 0)
            {
                lsvNotificaciones.DataSource = lstNotificaciones;
                dvMensajeLsvNotificaciones.Visible = false;
                btnEliminarNotificaciones.Visible = true;
            }
            else
            {
                dvMensajeLsvNotificaciones.InnerHtml = MessageManager.Info(dvMensajeLsvNotificaciones, Message.MsjeListadoNotificacionSinResultados, false);
                dvMensajeLsvNotificaciones.Visible = true;
                btnEliminarNotificaciones.Visible = false;
            }

            lsvNotificaciones.DataBind();
            lsvNotificaciones.Visible = true;
        }

        private void MarcarNotificacionComoVisto(long codCliente)
        {
            if (codCliente > 0)
            {
                var oMensajeBLL = new BLL.MensajeBLL();
                var lstMensaje = oMensajeBLL.ObtenerMensajesNuevos(codCliente);

                if (lstMensaje != null)
                {
                    foreach (var item in lstMensaje)
                    {
                        item.MENSAJE_VISTO = true;
                        var loResultado = oMensajeBLL.ModificarMensaje(item);
                    }
                }
            }
        }

        #endregion

        #region Métodos Públicos Estaticos

        [WebMethod]
        public static bool MarcarNotificacionComoEliminada(string pIdMensaje)
        {
            bool loResultado = false;

            if (!string.IsNullOrEmpty(pIdMensaje))
            {
                var oMensajeBLL = new BLL.MensajeBLL();
                var oMensaje = oMensajeBLL.ObtenerMensaje(Convert.ToInt64(pIdMensaje));

                if (oMensaje != null)
                {
                    oMensaje.FECHA_MODIFICACION_MENSAJE = DateTime.Now;
                    loResultado = oMensajeBLL.ModificarMensaje(oMensaje);
                }
            }

            return loResultado;
        }

        #endregion

        protected void ddlNotificaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarNotificaciones();
        }
    }
}