using BLL.DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class RolBLL
    {
        #region Métodos Públicos

        public Rol ObtenerRol(long idRol)
        {
            Rol oRol = null;

            try
            {
                using (var rep = new Repository<Rol>())
                {
                    oRol = rep.Find(p => p.ID_ROL == idRol);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oRol;
        }

        public List<Rol> ObtenerRoles()
        {
            List<Rol> lstRoles = null;

            try
            {
                using (var rep = new Repository<Rol>())
                {
                    lstRoles = rep.FindAll();
                    lstRoles.Sort((x, y) => String.Compare(x.DESCRIPCION, y.DESCRIPCION));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstRoles;
        }

        public List<Rol> ObtenerRolesAdminDashboard()
        {
            List<Rol> lstRoles = null;

            try
            {
                using (var rep = new Repository<Rol>())
                {
                    lstRoles = rep.Search(x => !x.DESCRIPCION.Equals("CLIENTE"));
                    lstRoles.Sort((x, y) => String.Compare(x.DESCRIPCION, y.DESCRIPCION));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstRoles;
        }

        #endregion
    }
}
