using System;
using System.Web.UI.HtmlControls;

namespace BLL.Common
{
    public class MessageManager
    {
        #region Atributos

        private const string loHtmlButton = "<button type='button' id='btnClose' aria-hidden='true' data-dismiss='alert' class='close'>×</button>";

        #endregion

        #region Métodos Púbicos

        public static string Success(HtmlGenericControl div, string mensaje, bool botonCerrar = true)
        {
            div.Attributes.Add("class", "alert alert-success alert-dismissable");
            var loContenido = string.Empty;
            if (botonCerrar)
                loContenido = loHtmlButton + mensaje;
            else
                loContenido = mensaje;
            return loContenido;
        }

        public static string Info(HtmlGenericControl div, string mensaje, bool botonCerrar = true)
        {
            div.Attributes.Add("class", "alert alert-info alert-dismissable");
            var loContenido = string.Empty;
            if (botonCerrar)
                loContenido = loHtmlButton + mensaje;
            else
                loContenido = mensaje;
            return loContenido;
        }

        public static string Warning(HtmlGenericControl div, string mensaje, bool botonCerrar = true)
        {
            div.Attributes.Add("class", "alert alert-warning alert-dismissable");
            var loContenido = string.Empty;
            if (botonCerrar)
                loContenido = loHtmlButton + mensaje;
            else
                loContenido = mensaje;
            return loContenido;
        }

        public static string Danger(HtmlGenericControl div, string mensaje, bool botonCerrar = true)
        {
            div.Attributes.Add("class", "alert alert-danger alert-dismissable");
            var loContenido = string.Empty;
            if (botonCerrar)
                loContenido = loHtmlButton + mensaje;
            else
                loContenido = mensaje;
            return loContenido;
        }

        public static string SuccessModal(string texto, string titulo = "", string pagina = "")
        {
            var loScript = "<script src='js/plugins/sweetalert/sweetalert.min.js'></script>";
            loScript += "<script language='javascript'>";
            loScript += "swal(";
            loScript += "{ title: '" + titulo + "', text: '" + texto.Replace("'", String.Empty).Trim() + "', confirmButtonText: 'Aceptar', type: 'success' }";

            if (!String.IsNullOrEmpty(pagina))
            {
                loScript += ",function(){";
                loScript += " var loLocation = window.location;";
                loScript += " var loPathName = loLocation.pathname.substring(0, loLocation.pathname.lastIndexOf('/') + 1);";
                loScript += " var url = loLocation.href.substring(0, loLocation.href.length - ((loLocation.pathname + loLocation.search + loLocation.hash).length - loPathName.length));";
                loScript += " window.location.href= url + '" + pagina + "'; ";
                loScript += "}";
            }

            loScript += ");";
            loScript += "</script>";

            return loScript;
        }

        public static string InfoModal(string texto, string titulo="", string pagina = "")
        {
            var loScript = "<script src='js/plugins/sweetalert/sweetalert.min.js'></script>";
            loScript += "<script language='javascript'>";
            loScript += "swal(";
            loScript += "{ title: '" + titulo + "', text: '" + texto.Replace("'", String.Empty).Trim() + "', confirmButtonText: 'Aceptar', type: 'info' }";

            if (!String.IsNullOrEmpty(pagina))
            {
                loScript += ",function(){";
                loScript += " var loLocation = window.location;";
                loScript += " var loPathName = loLocation.pathname.substring(0, loLocation.pathname.lastIndexOf('/') + 1);";
                loScript += " var url = loLocation.href.substring(0, loLocation.href.length - ((loLocation.pathname + loLocation.search + loLocation.hash).length - loPathName.length));";
                loScript += " window.location.href= url + '" + pagina + "'; ";
                loScript += "}";
            }

            loScript += ");";
            loScript += "</script>";

            return loScript;
        }

        public static string WarningModal(string texto, string titulo = "", string pagina = "")
        {
            var loScript = "<script src='js/plugins/sweetalert/sweetalert.min.js'></script>";
            loScript += "<script language='javascript'>";
            loScript += "swal(";
            loScript += "{ title: '" + titulo + "', text: '" + texto.Replace("'", String.Empty).Trim() + "', confirmButtonText: 'Aceptar', type: 'warning' }";

            if (!String.IsNullOrEmpty(pagina))
            {
                loScript += ",function(){";
                loScript += " var loLocation = window.location;";
                loScript += " var loPathName = loLocation.pathname.substring(0, loLocation.pathname.lastIndexOf('/') + 1);";
                loScript += " var url = loLocation.href.substring(0, loLocation.href.length - ((loLocation.pathname + loLocation.search + loLocation.hash).length - loPathName.length));";
                loScript += " window.location.href= url + '" + pagina + "'; ";
                loScript += "}";
            }

            loScript += ");";
            loScript += "</script>";

            return loScript;
        }

        public static string DangerModal(string texto, string titulo = "", string pagina = "")
        {
            var loScript = "<script src='js/plugins/sweetalert/sweetalert.min.js'></script>";
            loScript += "<script language='javascript'>";
            loScript += "swal(";
            loScript += "{ title: '" + titulo + "', text: '" + texto.Replace("'", String.Empty).Trim() + "', confirmButtonText: 'Aceptar', type: 'error' }";

            if (!String.IsNullOrEmpty(pagina))
            {
                loScript += ",function(){";
                loScript += " var loLocation = window.location;";
                loScript += " var loPathName = loLocation.pathname.substring(0, loLocation.pathname.lastIndexOf('/') + 1);";
                loScript += " var url = loLocation.href.substring(0, loLocation.href.length - ((loLocation.pathname + loLocation.search + loLocation.hash).length - loPathName.length));";
                loScript += " window.location.href= url + '" + pagina + "'; ";
                loScript += "}";
            }

            loScript += ");";
            loScript += "</script>";

            return loScript;
        }

        #endregion
    }
}
