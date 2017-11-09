using System;
using System.Web;

namespace BLL.Common
{
    public class Menu
    {
        #region Atributos

        private readonly string[] _paginaIndex = { "Index.aspx", "fa fa-home", "Principal" };
        private readonly string[] _seccionProveedores = { "javascript:;", "fa fa-briefcase", "Proveedores" };
        private readonly string[] _paginaProveedorListado = { "ProveedorListado.aspx", "Listado de Proveedores" };
        private readonly string[] _paginaProveedorVisualizar = { "ProveedorVisualizar.aspx" };

        #endregion

        #region Métodos Públicos

        public string Generar()
        {
            var loCurrentPage = HttpContext.Current.Request.Url.Segments[2];
            const StringComparison loCurrentCulture = StringComparison.CurrentCultureIgnoreCase;
            var loCadena = string.Empty;

            if (String.Equals(loCurrentPage, _paginaIndex[0], loCurrentCulture))
                loCadena = DibujarIndex();
            else if (String.Equals(loCurrentPage, _paginaProveedorListado[0], loCurrentCulture))
                loCadena = DibujarProveedorListado();
            else if (String.Equals(loCurrentPage, _paginaProveedorVisualizar[0], loCurrentCulture))
                loCadena = DibujarProveedorVisualizar();

            return loCadena;
        }

        #endregion

        #region Métodos Privados

        private string DibujarIndex()
        {
            var loCadena = string.Format("<li class='active'><a href='{0}'><i class='{1}'></i><span class='nav-label'>{2}</span></a></li>", _paginaIndex[0], _paginaIndex[1], _paginaIndex[2]);
            loCadena += string.Format("<li><a href='{0}'><i class='{1}'></i><span class='nav-label'>{2}</span></a>", _seccionProveedores[0], _seccionProveedores[1], _seccionProveedores[2]);
            loCadena += string.Format("<ul class='nav nav-second-level {0}'>", "collapse");
            loCadena += string.Format("<li><a href='{0}'>{1}</a></li>", _paginaProveedorListado[0], _paginaProveedorListado[1]);
            loCadena += string.Format("</ul></li>");
            return loCadena;
        }        

        private string DibujarProveedorListado()
        {
            var loCadena = string.Format("<li><a href='{0}'><i class='{1}'></i><span class='nav-label'>{2}</span></a></li>", _paginaIndex[0], _paginaIndex[1], _paginaIndex[2]);
            loCadena += string.Format("<li class='active'><a href='{0}'><i class='{1}'></i><span class='nav-label'>{2}</span></a>", _seccionProveedores[0], _seccionProveedores[1], _seccionProveedores[2]);
            loCadena += string.Format("<ul class='nav nav-second-level {0}'>", "collapse in");
            loCadena += string.Format("<li class='active'><a href='{0}'>{1}</a></li>", _paginaProveedorListado[0], _paginaProveedorListado[1]);
            loCadena += string.Format("</ul></li>");
            return loCadena;
        }

        private string DibujarProveedorVisualizar()
        {
            var loCadena = string.Format("<li><a href='{0}'><i class='{1}'></i><span class='nav-label'>{2}</span></a></li>", _paginaIndex[0], _paginaIndex[1], _paginaIndex[2]);
            loCadena += string.Format("<li><a href='{0}'><i class='{1}'></i><span class='nav-label'>{2}</span></a>", _seccionProveedores[0], _seccionProveedores[1], _seccionProveedores[2]);
            loCadena += string.Format("<ul class='nav nav-second-level {0}'>", "collapse");
            loCadena += string.Format("<li><a href='{0}'>{1}</a></li>", _paginaProveedorListado[0], _paginaProveedorListado[1]);
            loCadena += string.Format("</ul></li>");
            return loCadena;
        }

        #endregion
    }
}
