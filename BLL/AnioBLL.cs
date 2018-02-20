using BLL.DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class AnioBLL
    {
        #region Métodos Públicos

        public List<Anio> ObtenerAnios()
        {
            List<Anio> lstAnios = null;

            try
            {
                using (var rep = new Repository<Anio>())
                {
                    lstAnios = rep.FindAll();
                    lstAnios.Sort((x, y) => y.DESCRIPCION.CompareTo(x.DESCRIPCION));                    
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstAnios;
        }

        #endregion
    }    
}
