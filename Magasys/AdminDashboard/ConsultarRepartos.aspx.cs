using BLL.Common;
using NLog;
using System;
using System.Web.UI;

namespace PL.AdminDashboard
{
    public partial class ConsultarRepartos : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            dvMensajeLsvReparto.Visible = false;
            if (!Page.IsPostBack)
                CargarGrilla();
        }

        #endregion

        #region Métodos Privados

        private void CargarGrilla()
        {
            try
            {
                var lstReparto = new BLL.RepartoBLL().ObtenerReparto();

                if (lstReparto != null && lstReparto.Count > 0)
                    lsvReparto.DataSource = lstReparto;
                else
                {
                    dvMensajeLsvReparto.InnerHtml = MessageManager.Info(dvMensajeLsvReparto, Message.MsjeListadoRepartoSinResultados, false);
                    dvMensajeLsvReparto.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lsvReparto.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            lsvReparto.DataBind();
        }

        #endregion
    }
}