using BLL.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.CustomersWebSite
{
    public partial class RestorePassword : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCambiarContrasenia_Click(object sender, EventArgs e)
        {
            try
            {
                string loUsuarioHash = ObtenerParametro();
                if (!string.IsNullOrEmpty(loUsuarioHash))
                {
                    if (ValidarCamposObligatorios())
                    {
                        if (ValidarLength())
                        {
                            if (ValidarCoincidencia())
                            {
                                var oUsuario = new BLL.UsuarioBLL().ObtenerUsuario(loUsuarioHash);

                                oUsuario.CONTRASENIA = txtContraseniaNueva.Text;

                                bool loResutado = new BLL.UsuarioBLL().ModificarUsuario(oUsuario);

                                if (loResutado)
                                {
                                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeMsjeRestorePasswordSuccessAlta, "Restaurar Contraseña","Login.aspx"));
                                }
                                else
                                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeMsjeRestorePasswordFailure));
                            }
                            else
                                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeRestorePasswordCoincidencia));
                        }
                        else
                            Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeRestorePasswordLength));
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeRestorePasswordContraseniaNuevaConfirmarContraseniaVacios));
                }
                else
                    Response.Redirect("Login.aspx", false);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeMsjeRestorePasswordFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        #endregion

        #region Métodos Privados

        private string ObtenerParametro()
        {
            string loResutado = string.Empty;

            if (Request.QueryString["p"] != null)
                loResutado = Request.QueryString["p"].ToString();

            return loResutado;
        }

        private bool ValidarCamposObligatorios()
        {
            bool loResutado = false;
            if (!string.IsNullOrEmpty(txtContraseniaNueva.Text) && !string.IsNullOrEmpty(txtContraseniaNuevaConfirmar.Text))
                loResutado = true;

            return loResutado;
        }

        private bool ValidarLength()
        {
            bool loResutado = false;
            if (txtContraseniaNueva.Text.Length > 7 && txtContraseniaNuevaConfirmar.Text.Length > 7)
                loResutado = true;

            return loResutado;
        }

        private bool ValidarCoincidencia()
        {
            bool loResutado = false;
            if (txtContraseniaNueva.Text.Equals(txtContraseniaNuevaConfirmar.Text))
                loResutado = true;

            return loResutado;
        }

        private BLL.DAL.Usuario CargarDesdeControles(BLL.DAL.Usuario oUsuario)
        {
            oUsuario.CONTRASENIA = txtContraseniaNueva.Text;
            return oUsuario;
        }

        #endregion
    }
}