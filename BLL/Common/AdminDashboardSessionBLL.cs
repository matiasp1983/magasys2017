using BLL.DAL;
using System.Web;

namespace BLL.Common
{
    public static class AdminDashboardSessionBLL
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

            public static readonly DefaultSessionsId Usuario = new DefaultSessionsId("AdminDashboardUsuario");            

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
