﻿using BLL.Common;
using NLog;
using System;
using System.Web.UI;

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
                    var loForgotPassword = new BLL.ForgotPasswordBLL();
                    var loCliente = new BLL.ClienteBLL();

                    if (loForgotPassword.ValidarEmail(txtEmail.Text))
                    {
                        var loUsuarioHash = loCliente.ConsultarExistenciaCliente(txtEmail.Text);

                        if (!string.IsNullOrEmpty(loUsuarioHash))
                        {
                            if (loForgotPassword.EnviarEmail(txtEmail.Text, loUsuarioHash))
                                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeForgotPasswordSuccess, "Recupero de contraseña", "Login.aspx"));
                            else
                                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeForgotPasswordFailure));
                        }
                        else
                            Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeForgotPasswordFailureNoCliente));
                    }
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

        #endregion
    }
}