using BLL.DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class EstadoBLL
    {
        #region Métodos Públicos

        public List<Estado> ObtenerEstados()
        {
            List<Estado> lstEstado = null;

            try
            {
                using (var rep = new Repository<Estado>())
                {
                    lstEstado = rep.FindAll();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstEstado;
        }

        #endregion
    }
}
