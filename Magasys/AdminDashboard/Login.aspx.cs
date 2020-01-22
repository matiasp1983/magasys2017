using BLL;
using BLL.Common;
using BLL.DAL;
using NLog;
using System;
using System.Web.UI;

namespace PL.AdminDashboard
{
    public partial class Login : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCamposObligatorios())
                {
                    if (ValidarUsuario())
                        Response.Redirect("Index.aspx", false);
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeLoginUsuarioIncorrecto));
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeLoginUsuarioYOContraseniaVacios));

            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        #endregion

        #region Métodos Privados

        private bool ValidarCamposObligatorios()
        {
            bool loResutado = false;
            if (!string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(txtContrasenia.Text))
                loResutado = true;

            return loResutado;
        }

        private bool ValidarUsuario()
        {
            bool loResultado = false;

            Usuario oUsuario = new Usuario
            {
                NOMBRE_USUARIO = txtUsuario.Text,
                CONTRASENIA = txtContrasenia.Text
            };

            loResultado = new LoginBLL().IniciarSesion(oUsuario) != null;

            return loResultado;
        } 

        #endregion
    }
}