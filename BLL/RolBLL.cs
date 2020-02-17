using BLL.DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class RolBLL
    {
        #region Métodos Públicos

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

        #endregion
    }    
}
