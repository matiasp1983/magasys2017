using System;
using System.Web;

namespace BLL.Common
{
    public class Menu
    {
        #region Atributos

        #region Index

        private readonly string[] _paginaIndex = { "Index.aspx", "fa fa-home", "Principal" }; 

        #endregion

        #region Proveedor

        private readonly string[] _seccionProveedores = { "javascript:;", "fa fa-briefcase", "Proveedores" };
        private readonly string[] _paginaProveedorListado = { "ProveedorListado.aspx", "Listado de Proveedores" };
        private readonly string[] _paginaProveedorVisualizar = { "ProveedorVisualizar.aspx" };

        #endregion

        #region Producto

        private readonly string[] _seccionProductos = { "javascript:;", "fa fa-briefcase", "Productos" };
        private readonly string[] _paginaProductoListado = { "ProductoListado.aspx", "Listado de Productos" };
        private readonly string[] _paginaProductoVisualizar = { "ProductoVisualizar.aspx" }; 

        #endregion

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
            else if (String.Equals(loCurrentPage, _paginaProductoListado[0], loCurrentCulture))
                loCadena = DibujarProductoListado();
            else if (String.Equals(loCurrentPage, _paginaProductoVisualizar[0], loCurrentCulture))
                loCadena = DibujarProductoVisualizar();

            return loCadena;
        }

        #endregion

        #region Métodos Privados

        #region Index

        private string DibujarIndex()
        {
            var loCadena = string.Format("<li class='active'><a href='{0}'><i class='{1}'></i><span class='nav-label'>{2}</span></a></li>", _paginaIndex[0], _paginaIndex[1], _paginaIndex[2]);
            loCadena += string.Format("<li><a href='{0}'><i class='{1}'></i><span class='nav-label'>{2}</span></a>", _seccionProveedores[0], _seccionProveedores[1], _seccionProveedores[2]);
            loCadena += string.Format("<ul class='nav nav-second-level {0}'>", "collapse");
            loCadena += string.Format("<li><a href='{0}'>{1}</a></li>", _paginaProveedorListado[0], _paginaProveedorListado[1]);            
            loCadena += string.Format("</ul></li>");
            loCadena += string.Format("<li><a href='{0}'><i class='{1}'></i><span class='nav-label'>{2}</span></a>", _seccionProductos[0], _seccionProductos[1], _seccionProductos[2]);
            loCadena += string.Format("<ul class='nav nav-second-level {0}'>", "collapse");
            loCadena += string.Format("<li><a href='{0}'>{1}</a></li>", _paginaProductoListado[0], _paginaProductoListado[1]);
            loCadena += string.Format("</ul></li>");
            return loCadena;
        } 

        #endregion

        #region Proveedor

        private string DibujarProveedorListado()
        {
            var loCadena = string.Format("<li><a href='{0}'><i class='{1}'></i><span class='nav-label'>{2}</span></a></li>", _paginaIndex[0], _paginaIndex[1], _paginaIndex[2]);
            loCadena += string.Format("<li class='active'><a href='{0}'><i class='{1}'></i><span class='nav-label'>{2}</span></a>", _seccionProveedores[0], _seccionProveedores[1], _seccionProveedores[2]);
            loCadena += string.Format("<ul class='nav nav-second-level {0}'>", "collapse in");
            loCadena += string.Format("<li class='active'><a href='{0}'>{1}</a></li>", _paginaProveedorListado[0], _paginaProveedorListado[1]);
            loCadena += string.Format("</ul></li>");
            loCadena += string.Format("<li><a href='{0}'><i class='{1}'></i><span class='nav-label'>{2}</span></a>", _seccionProductos[0], _seccionProductos[1], _seccionProductos[2]);
            loCadena += string.Format("<ul class='nav nav-second-level {0}'>", "collapse");
            loCadena += string.Format("<li><a href='{0}'>{1}</a></li>", _paginaProductoListado[0], _paginaProductoListado[1]);
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
            loCadena += string.Format("<li><a href='{0}'><i class='{1}'></i><span class='nav-label'>{2}</span></a>", _seccionProductos[0], _seccionProductos[1], _seccionProductos[2]);
            loCadena += string.Format("<ul class='nav nav-second-level {0}'>", "collapse");
            loCadena += string.Format("<li><a href='{0}'>{1}</a></li>", _paginaProductoListado[0], _paginaProductoListado[1]);
            loCadena += string.Format("</ul></li>");
            return loCadena;
        }

        #endregion

        #region Producto

        private string DibujarProductoListado()
        {
            var loCadena = string.Format("<li><a href='{0}'><i class='{1}'></i><span class='nav-label'>{2}</span></a></li>", _paginaIndex[0], _paginaIndex[1], _paginaIndex[2]);
            loCadena += string.Format("<li><a href='{0}'><i class='{1}'></i><span class='nav-label'>{2}</span></a>", _seccionProveedores[0], _seccionProveedores[1], _seccionProveedores[2]);
            loCadena += string.Format("<ul class='nav nav-second-level {0}'>", "collapse");
            loCadena += string.Format("<li><a href='{0}'>{1}</a></li>", _paginaProveedorListado[0], _paginaProveedorListado[1]);
            loCadena += string.Format("</ul></li>");
            loCadena += string.Format("<li class='active'><a href='{0}'><i class='{1}'></i><span class='nav-label'>{2}</span></a>", _seccionProductos[0], _seccionProductos[1], _seccionProductos[2]);
            loCadena += string.Format("<ul class='nav nav-second-level {0}'>", "collapse in");
            loCadena += string.Format("<li class='active'><a href='{0}'>{1}</a></li>", _paginaProductoListado[0], _paginaProductoListado[1]);
            loCadena += string.Format("</ul></li>");
            return loCadena;
        }

        private string DibujarProductoVisualizar()
        {
            var loCadena = string.Format("<li><a href='{0}'><i class='{1}'></i><span class='nav-label'>{2}</span></a></li>", _paginaIndex[0], _paginaIndex[1], _paginaIndex[2]);
            loCadena += string.Format("<li><a href='{0}'><i class='{1}'></i><span class='nav-label'>{2}</span></a>", _seccionProveedores[0], _seccionProveedores[1], _seccionProveedores[2]);
            loCadena += string.Format("<ul class='nav nav-second-level {0}'>", "collapse");
            loCadena += string.Format("<li><a href='{0}'>{1}</a></li>", _paginaProveedorListado[0], _paginaProveedorListado[1]);
            loCadena += string.Format("</ul></li>");
            loCadena += string.Format("<li><a href='{0}'><i class='{1}'></i><span class='nav-label'>{2}</span></a>", _seccionProductos[0], _seccionProductos[1], _seccionProductos[2]);
            loCadena += string.Format("<ul class='nav nav-second-level {0}'>", "collapse");
            loCadena += string.Format("<li><a href='{0}'>{1}</a></li>", _paginaProductoListado[0], _paginaProductoListado[1]);
            loCadena += string.Format("</ul></li>");
            return loCadena;
        } 

        #endregion

        #endregion
    }
}
