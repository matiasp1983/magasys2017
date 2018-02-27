using BLL.DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class GeneroBLL
    {
        #region Métodos Públicos

        public Genero ObtenerGenero(long idGenero)
        {
            Genero oGenero = null;

            try
            {
                using (var rep = new Repository<Genero>())
                {
                    oGenero = rep.Find(p => p.ID_GENERO == idGenero);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oGenero;
        }

        public List<Genero> ObtenerGeneros()
        {
            List<Genero> lstGeneros = null;

            try
            {
                using (var rep = new Repository<Genero>())
                {
                    lstGeneros = rep.FindAll();
                    lstGeneros.Sort((x, y) => String.Compare(x.NOMBRE, y.NOMBRE));
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
