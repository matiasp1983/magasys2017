using BLL.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.CustomersWebSite
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviarEmail_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCamposObligatorios())
                {
                    if (ValidarEmail())
                        if (EnviarEmail())
                            Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeForgotPasswordSuccess, "Recupero de contraseña", "Login.aspx"));
                        else
                            Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeForgotPasswordFailure));
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeForgotPasswordIncorrecto));
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeForgotPasswordEmailVacio));
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeForgotPasswordFailure));
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private bool ValidarCamposObligatorios()
        {
            bool loResutado = false;
            if (!string.IsNullOrEmpty(txtEmail.Text))
                loResutado = true;

            return loResutado;
        }

        private bool ValidarEmail()
        {
            bool loResultado = false;
            string loExpresion;
            loExpresion = @"^[-a-z0-9~!$%^&*_=+}{\'?]+(\.[-a-z0-9~!$%^&*_=+}{\'?]+)*@([a-z0-9_][-a-z0-9_]*(\.[-a-z0-9_]+)" +
                @"*\.(aero|arpa|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|travel|mobi|[a-z][a-z])|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,5})?$";
            if (Regex.IsMatch(txtEmail.Text, loExpresion))
            {
                if (Regex.Replace(txtEmail.Text, loExpresion, string.Empty).Length == 0)
                    loResultado = true;
            }

            return loResultado;
        }

        private bool EnviarEmail()
        {
            return true;
        }

        #endregion
    }
}