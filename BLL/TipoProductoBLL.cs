using BLL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    lstTipoProducto = rep.FindAll().OrderBy(p => p.DESCRIPCION).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstTipoProducto;
        }

        public TipoProducto ObtenerTipoProducto(long idTipoProducto)
        {
            TipoProducto oTipoProducto = null;

            try
            {
                using (var rep = new Repository<TipoProducto>())
                {
                    oTipoProducto = rep.Find(p => p.ID_TIPO_PRODUCTO == idTipoProducto);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oTipoProducto;
        }

        public TipoProducto ObtenerTipoProducto(string descripcionTipoProducto)
        {
            TipoProducto oTipoProducto = null;

            try
            {
                using (var rep = new Repository<TipoProducto>())
                {
                    oTipoProducto = rep.Find(p => p.DESCRIPCION == descripcionTipoProducto);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oTipoProducto;
        }
    }

    #endregion
}