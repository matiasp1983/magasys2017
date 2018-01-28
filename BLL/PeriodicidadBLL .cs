using BLL.DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class PeriodicidadBLL
    {
        #region Métodos Públicos

        public List<Periodicidad> ObtenerPeriodicidades()
        {
            List<Periodicidad> lstPeriodicidades = null;

            try
            {
                using (var rep = new Repository<Periodicidad>())
                {
                    lstPeriodicidades = rep.FindAll();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstPeriodicidades;
        }

        #endregion
    }
}
