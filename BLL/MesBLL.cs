using BLL.DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class MesBLL
    {
        #region Métodos Públicos

        public Mes ObtenerMes(long idMes)
        {
            Mes oMes = null;

            try
            {
                using (var rep = new Repository<Mes>())
                {
                    oMes = rep.Find(p => p.ID_MES == idMes);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oMes;
        }

        public List<Mes> ObtenerMeses()
        {
            List<Mes> lstMeses = null;

            try
            {
                using (var rep = new Repository<Mes>())
                {
                    lstMeses = rep.FindAll();                    
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstMeses;
        }        

        #endregion
    }
}
