using System;
using System.Web.UI.HtmlControls;
using BLL.Common;

namespace PL.AdminDashboard
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            menu.Controls.Add(new HtmlElement { InnerHtml = new Menu().Generar() });
        }

        #endregion
    }
}