using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Net;
using System.Text.RegularExpressions;

namespace BLL
{
    public class ForgotPasswordBLL
    {
        #region Métodos Públicos

        public bool ValidarEmail(string pEmail)
        {
            bool loResultado = false;

            if (!string.IsNullOrEmpty(pEmail))
            {
                string loExpresion;
                loExpresion = @"^[-a-z0-9~!$%^&*_=+}{\'?]+(\.[-a-z0-9~!$%^&*_=+}{\'?]+)*@([a-z0-9_][-a-z0-9_]*(\.[-a-z0-9_]+)" +
                    @"*\.(aero|arpa|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|travel|mobi|[a-z][a-z])|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,5})?$";
                if (Regex.IsMatch(pEmail, loExpresion))
                {
                    if (Regex.Replace(pEmail, loExpresion, string.Empty).Length == 0)
                        loResultado = true;
                }
            }

            return loResultado;
        }

        public bool EnviarEmail(string pEmail, string pUsarioHash)
        {
            bool loResultado = false;

            try
            {
                MimeMessage loMensaje = new MimeMessage();
                loMensaje.From.Add(new MailboxAddress("Magasys", "magasys2020@gmail.com"));
                loMensaje.To.Add(new MailboxAddress(pEmail));
                loMensaje.Subject = "Magasys - Restablecer contraseña";

                var loBuilder = new BodyBuilder();

                loBuilder.HtmlBody = string.Format(@"<html>

<head>
    <title>Restablecer contraseña</title>
</head>

<body style='background-color: #f6f6f6;'>
    <table>
        <tr>
            <td></td>
            <td style='display: block !important; max-width: 600px !important; margin: 0 auto !important; clear: both !important;' width='600'>
                <div style='max-width: 600px; margin: 0 auto; display: block; padding: 20px;'>
                    <table style='background: #fff; border: 1px solid #e9e9e9; border-radius: 3px;' width='100%' cellpadding='0' cellspacing='0'>
                        <tr tyle='padding: 20px;'>
                            <table cellpadding='0' cellspacing='0'>
                                <tr>
                                    <td style='padding: 0 10px 20px;'>
                                        <h3>¡Hola Estimado Cliente!</h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td style='padding: 0 10px 20px;'>
                                        Recibimos un pedido para recuperar tu contraseña. Sólo es necesario que hagas click para seguir las instrucciones.
                                    </td>
                                </tr>                                
                                <tr>
                                    <td style='padding: 0 10px 20px; text-align: center;'>
                                        <a href='http://localhost:25000/CustomersWebSite/RestorePassword.aspx?p={0}' style='text-decoration: none; color: #FFF; background-color: #1ab394; border: solid #1ab394;border-width: 5px 10px;
                                                line-height: 2; font-weight: bold; text-align: center; cursor: pointer; display: inline-block; border-radius: 5px; text-transform: capitalize;'>Restablecer Contraseña</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style='padding: 0 10px 20px;'>
                                        Si no solicitaste el cambio de contraseña, ignora este email
                                    </td>
                                </tr>
                                <tr>
                                    <td style='padding: 0 10px 20px'>
                                        Gracias por elegir Magasys.
                                    </td>
                                </tr>
                            </table>
                        </tr>
                    </table>
                </div>
            </td>
            <td></td>
        </tr>
    </table>
</body>

</html>", pUsarioHash);

                loMensaje.Body = loBuilder.ToMessageBody();               

                using (var loClienteSmtp = new SmtpClient())
                {
                    loClienteSmtp.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                    loClienteSmtp.Authenticate("magasys2020@gmail.com", "Mg2y22020");
                    var options = FormatOptions.Default.Clone();
                    if (loClienteSmtp.Capabilities.HasFlag(SmtpCapabilities.UTF8))
                        options.International = true;

                    loClienteSmtp.Send(options, loMensaje);
                    loClienteSmtp.Disconnect(true);

                    loResultado = true;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return loResultado;
        }

        #endregion
    }
}
