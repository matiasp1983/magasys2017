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
                menu.Controls.Add(new HtmlElement { InnerHtml = new Menu().Generar() });
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex);
            }
        }

        #endregion
    }
}