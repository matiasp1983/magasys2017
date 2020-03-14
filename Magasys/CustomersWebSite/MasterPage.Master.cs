using BLL.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.CustomersWebSite
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BLL.DAL.Usuario loUsuario = null;

                if (!IsPostBack)
                {
                    if (Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()] != null)
                    {
                        TextInfo loText = new CultureInfo("es-AR", false).TextInfo;
                        loUsuario = (BLL.DAL.Usuario)Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()];
                        /*lblUsuarioLogout.Text = loText.ToUpper(loUsuario.APELLIDO + " " + loUsuario.NOMBRE).ToString();

                        if (loUsuario.AVATAR != null)
                        {
                            string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(loUsuario.AVATAR);
                            imgPerfil.ImageUrl = loImagenDataURL64;
                        }*/

                        Response.ClearHeaders();
                        Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                        Response.AddHeader("Pragma", "no-cache");
                    }
                    else
                    {
                        Response.Redirect("Login.aspx", true);
                    }
                }

                //MenuPrincipal(loUsuario);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()] == null)
            {
                Response.Redirect("Login.aspx", true);
            }
        }

        #endregion
    }
}