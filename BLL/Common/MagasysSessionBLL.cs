using BLL.DAL;
using System;
using System.Web;

namespace BLL.Common
{
    public static class MagasysSessionBLL
    {
        #region Propiedades

        public static Usuario UsuarioActual
        {
            get
            {
                return (Usuario)HttpContext.Current.Session[DefaultSessionsId.Usuario.ToString()];
            }
            set
            {
                HttpContext.Current.Session[DefaultSessionsId.Usuario.ToString()] = value;
            }
        }

        public sealed class DefaultSessionsId
        {
            private readonly string NombreSesion;

            public static readonly DefaultSessionsId Usuario = new DefaultSessionsId("MagasysUsuario");
            public static readonly DefaultSessionsId UsuarioNombre = new DefaultSessionsId("MagasysUsuarioNombre");
            public static readonly DefaultSessionsId UsuarioApellido = new DefaultSessionsId("MagasysUsuarioApellido");

            public DefaultSessionsId(string pNombreSesion)
            {
                this.NombreSesion = pNombreSesion;
            }

            public override string ToString()
            {
                return this.NombreSesion;
            }

        }

        #endregion
    }
}
