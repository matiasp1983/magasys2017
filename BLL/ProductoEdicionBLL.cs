using BLL.DAL;
using BLL.Filters;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace BLL
{
    public class ProductoEdicionBLL
    {
        #region  Métodos Públicos

        public int AltaProductoEdicion(ProductoEdicion oProductoEdicion)
        {
            var oIdProductoEdicion = 0;

            try
            {
                using (var rep = new Repository<ProductoEdicion>())
                {
                    rep.Create(oProductoEdicion);
                    oIdProductoEdicion = oProductoEdicion.ID_PRODUCTO_EDICION;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return oIdProductoEdicion;
        }

        public bool ModificarProductoEdicion(ProductoEdicion oProductoEdicion)
        {
            var bRes = false;
            try
            {
                using (var rep = new Repository<ProductoEdicion>())
                {
                    bRes = rep.Update(oProductoEdicion);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return bRes;
        }

        public bool ActualizarCantidadDisponible(int codProductoEdicion, int cantidad)
        {
            var bRes = false;
            ProductoEdicion oProductoEdicion = null;

            try
            {
                using (var res = new Repository<ProductoEdicion>())
                {
                    oProductoEdicion = res.Find(p => p.ID_PRODUCTO_EDICION == codProductoEdicion);
                }

                if (oProductoEdicion != null)
                {
                    oProductoEdicion.CANTIDAD_DISPONIBLE = oProductoEdicion.CANTIDAD_DISPONIBLE - cantidad;
                    bRes = ModificarProductoEdicion(oProductoEdicion);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return bRes;
        }

        public ProductoEdicion ConsultarExistenciaEdicion(string nroEdicion, int tipoProducto)
        {
            ProductoEdicion oProductoEdicion = null;

            try
            {
                using (var res = new Repository<ProductoEdicion>())
                {
                    oProductoEdicion = res.Find(p => p.EDICION == nroEdicion && p.COD_TIPO_PRODUCTO == tipoProducto);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return oProductoEdicion;
        }

        #endregion
    }
}
