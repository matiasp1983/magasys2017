using BLL;
using BLL.Common;
using System;

namespace PL
{
    public partial class Logout : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (Session[MagasysSessionBLL.DefaultSessionsId.Usuario.ToString()] != null)
            {
                if (new LoginBLL().CerrarSesion())
                {
                    Session[MagasysSessionBLL.DefaultSessionsId.Usuario.ToString()] = null;
                    Session.Abandon();
                    Response.Redirect("Login.aspx", false);
                }
            }
        }

        #endregion
    }
}