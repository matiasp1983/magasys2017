using BLL.Common;
using BLL.DAL;
using System;

namespace BLL
{
    public class LoginBLL
    {
        #region Métodos Públicos

        public Usuario IniciarSessionAdminDashboard(Usuario oUsuario)
        {
            try
            {
                var loResultado = false;
                Usuario loUsuario;

                using (var rep = new Repository<Usuario>())
                {
                    loUsuario = rep.Find(p => p.NOMBRE_USUARIO.ToUpper() == oUsuario.NOMBRE_USUARIO.ToUpper() && p.FECHA_BAJA.HasValue == false && p.ID_ROL != RolUsuario.Cliente);

                    if (loUsuario != null)
                        if (!string.IsNullOrEmpty(loUsuario.CONTRASENIA))
                            loResultado = (Eramake.eCryptography.Decrypt(loUsuario.CONTRASENIA) == oUsuario.CONTRASENIA);
                }

                if (loResultado)
                {
                    AdminDashboardSessionBLL.UsuarioActual = loUsuario;
                    return loUsuario;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CerrarSessionAdminDashboard()
        {
            AdminDashboardSessionBLL.UsuarioActual = null;
            return true;
        }

        public Usuario IniciarSessionCustomersWebSite(Usuario oUsuario)
        {
            try
            {
                var loResultado = false;
                Usuario loUsuario;

                using (var rep = new Repository<Usuario>())
                {
                    loUsuario = rep.Find(p => p.NOMBRE_USUARIO.ToUpper() == oUsuario.NOMBRE_USUARIO.ToUpper() && p.FECHA_BAJA.HasValue == false && p.ID_ROL == RolUsuario.Cliente);

                    if (loUsuario != null)
                        if (!string.IsNullOrEmpty(loUsuario.CONTRASENIA))
                            loResultado = (Eramake.eCryptography.Decrypt(loUsuario.CONTRASENIA) == oUsuario.CONTRASENIA);
                }

                if (loResultado)
                {
                    CustomersWebSiteSessionBLL.UsuarioActual = loUsuario;
                    return loUsuario;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CerrarSessionCustomersWebSite()
        {
            CustomersWebSiteSessionBLL.UsuarioActual = null;
            return true;
        }

        #endregion
    }
}
