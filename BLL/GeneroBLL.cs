using BLL.DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class GeneroBLL
    {
        #region Métodos Públicos

        public List<Genero> ObtenerGeneros()
        {
            List<Genero> lstGeneros = null;

            try
            {
                using (var rep = new Repository<Genero>())
                {
                    lstGeneros = rep.FindAll();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstGeneros;
        }

        #endregion
    }
}
