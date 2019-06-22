using BLL.DAL;
using System;
using BLL.Filters;
using System.Collections.Generic;

namespace BLL
{
    public class EstadoBLL
    {
        #region Métodos Públicos

        public List<Estado> ObtenerEstados(string ambito)
        {
            List<Estado> lstEstado = null;

            try
            {
                using (var rep = new Repository<Estado>())
                {
                    lstEstado = rep.Search(p => p.AMBITO == ambito);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstEstado;
        }

        public Estado ObtenerEstado(EstadoFiltro oEstadoFiltro)
        {
            Estado oEstado = null;

            try
            {
                using (var rep = new Repository<Estado>())
                {
                    oEstado = rep.Find(p => p.NOMBRE == oEstadoFiltro.Nombre && p.AMBITO == oEstadoFiltro.Ambito);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oEstado;
        }
        
        #endregion
    }
}
