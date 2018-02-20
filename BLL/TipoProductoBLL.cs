using BLL.DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class TipoProductoBLL
    {
        #region Métodos Públicos

        public List<TipoProducto> ObtenerTiposProducto()
        {
            List<TipoProducto> lstTipoProducto = null;

            try
            {
                using (var rep = new Repository<TipoProducto>())
                {
                    lstTipoProducto = rep.FindAll();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstTipoProducto;
        }

        #endregion
    }
}
