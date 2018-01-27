using System;
using System.Web.UI.HtmlControls;
using BLL.Common;
using NLog;

namespace PL.AdminDashboard
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                MenuPrincipal();
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        #endregion

        #region Métodos Privados

        private void MenuPrincipal()
        {
            String loActivePage = Request.RawUrl;
            if (loActivePage.Contains("Index.aspx"))
            {
                liPrincipal.Attributes["class"] = "active";
            }
            if (loActivePage.Contains("ProveedorListado.aspx"))
            {
                liProveedores.Attributes["class"] = "active";
                liProveedorListado.Attributes["class"] = "active";
            }
            if (loActivePage.Contains("ProductoListado.aspx"))
            {
                liProductos.Attributes["class"] = "active";
                liProductoListado.Attributes["class"] = "active";
            }
        }

        #endregion
    }
}