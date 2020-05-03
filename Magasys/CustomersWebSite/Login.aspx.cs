using BLL;
using BLL.Common;
using NLog;
using System;
using System.Web.UI;

namespace PL.CustomersWebSite
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

            BLL.DAL.Usuario oUsuario = new BLL.DAL.Usuario
            {
                NOMBRE_USUARIO = txtUsuario.Text,
                CONTRASENIA = txtContrasenia.Text
            };

            var oUsuarioLoguin = new LoginBLL().IniciarSessionCustomersWebSite(oUsuario);
            loResultado = new ClienteBLL().ObtenerClientePorUsuario(oUsuarioLoguin.ID_USUARIO) != null;

            return loResultado;
        }

        #endregion
    }
}