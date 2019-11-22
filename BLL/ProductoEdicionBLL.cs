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
                if (oProductoEdicion.Imagen != null)
                {
                    using (var loRepImagen = new Repository<Imagen>())
                    {
                        var loImangen = loRepImagen.Create(oProductoEdicion.Imagen);
                        oProductoEdicion.COD_IMAGEN = loImangen.ID_IMAGEN;
                    }
                }

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

        public bool ActualizarCantidadDisponible(int codProductoEdicion, int cantidad, DateTime? fechaDevolucionReal = null)
        {
            var bRes = false;
            ProductoEdicion oProductoEdicion = null;
            ProductoEdicion oProductoEdicionAux = null;

            try
            {
                using (var res = new Repository<ProductoEdicion>())
                {
                    oProductoEdicion = res.Find(p => p.ID_PRODUCTO_EDICION == codProductoEdicion);
                }

                if (oProductoEdicion != null)
                {
                    oProductoEdicionAux = new ProductoEdicion
                    {
                        ID_PRODUCTO_EDICION = oProductoEdicion.ID_PRODUCTO_EDICION,
                        COD_PRODUCTO = oProductoEdicion.COD_PRODUCTO,
                        COD_TIPO_PRODUCTO = oProductoEdicion.COD_TIPO_PRODUCTO,
                        COD_ESTADO = oProductoEdicion.COD_ESTADO,
                        EDICION = oProductoEdicion.EDICION,
                        PRECIO = oProductoEdicion.PRECIO
                    };

                    if (oProductoEdicion.FECHA_EDICION != null)
                        oProductoEdicionAux.FECHA_EDICION = oProductoEdicion.FECHA_EDICION;

                    if (!String.IsNullOrEmpty(oProductoEdicion.NOMBRE))
                        oProductoEdicionAux.NOMBRE = oProductoEdicion.NOMBRE;

                    if (!String.IsNullOrEmpty(oProductoEdicion.DESCRIPCION))
                        oProductoEdicionAux.DESCRIPCION = oProductoEdicion.DESCRIPCION;

                    if (oProductoEdicion.COD_IMAGEN != null)
                        oProductoEdicionAux.COD_IMAGEN = oProductoEdicion.COD_IMAGEN;

                    oProductoEdicionAux.CANTIDAD_DISPONIBLE = oProductoEdicion.CANTIDAD_DISPONIBLE - cantidad;

                    if (fechaDevolucionReal != null)
                        oProductoEdicionAux.FECHA_DEVOLUCION_REAL = fechaDevolucionReal;
                    else if (oProductoEdicion.FECHA_DEVOLUCION_REAL != null)
                        oProductoEdicionAux.FECHA_DEVOLUCION_REAL = oProductoEdicion.FECHA_DEVOLUCION_REAL;

                    bRes = ModificarProductoEdicion(oProductoEdicionAux);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return bRes;
        }

        public ProductoEdicion ConsultarExistenciaEdicion(int codProducto, string nroEdicion, int tipoProducto)
        {
            ProductoEdicion oProductoEdicion = null;

            try
            {
                using (var res = new Repository<ProductoEdicion>())
                {
                    oProductoEdicion = res.Find(p => p.COD_PRODUCTO == codProducto && p.EDICION == nroEdicion && p.COD_TIPO_PRODUCTO == tipoProducto);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return oProductoEdicion;
        }

        public ProductoEdicion ObtenerEdicion(int codigo_edicion)
        {
            ProductoEdicion oProductoEdicion = null;

            try
            {
                using (var res = new Repository<ProductoEdicion>())
                {
                    oProductoEdicion = res.Find(p => p.ID_PRODUCTO_EDICION == codigo_edicion);
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
