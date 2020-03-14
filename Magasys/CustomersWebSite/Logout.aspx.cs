using BLL;
using BLL.Common;
using System;

namespace PL.CustomersWebSite
{
    public partial class Logout : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()] != null)
            {
                if (new LoginBLL().CerrarSessionCustomersWebSite())
                {
                    Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()] = null;
                    Session.Abandon();
                    Response.Redirect("Login.aspx", false);
                }
            }
        }

        #endregion
    }
}