using BLL.DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class EstadoProductoBLL
    {
        #region Métodos Públicos

        public List<EstadoProducto> ObtenerEstadosProducto()
        {
            List<EstadoProducto> lstEstadoProducto = null;

            try
            {
                using (var rep = new Repository<EstadoProducto>())
                {
                    lstEstadoProducto = rep.FindAll();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstEstadoProducto;
        }

        #endregion
    }
}
