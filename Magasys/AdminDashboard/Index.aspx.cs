using NLog;
using System;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class Index : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {   
                CargarMeses();
            }
        }

        #endregion

        #region Métodos Privados

        private void CargarMeses()
        {
            var oMes = new BLL.MesBLL();

            try
            {
                ddlMes.DataSource = oMes.ObtenerMeses();
                ddlMes.DataTextField = "NOMBRE";
                ddlMes.DataValueField = "ID_MES";
                ddlMes.DataBind();                
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        #endregion
    }
}