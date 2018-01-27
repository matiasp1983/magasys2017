using BLL.DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class DiaSemanaBLL
    {
        #region Métodos Públicos

        public List<DiaSemana> ObtenerDiasDeSemana()
        {
            List<DiaSemana> lstDiasSemana = null;

            try
            {
                using (var rep = new Repository<DiaSemana>())
                {
                    lstDiasSemana = rep.FindAll();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstDiasSemana;
        }

        #endregion
    }
}
