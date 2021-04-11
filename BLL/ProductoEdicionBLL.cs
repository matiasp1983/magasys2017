using BLL.DAL;
using System;
using System.Collections.Generic;

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
                throw ex;
            }

            return oIdProductoEdicion;
        }

        public bool ModificarProductoEdicion(ProductoEdicion oProductoEdicion)
        {
            var bRes = false;
            try
            {
                if (oProductoEdicion.COD_IMAGEN == null)
                {
                    if (oProductoEdicion.Imagen != null)
                    {
                        using (var loRepImagen = new Repository<Imagen>())
                        {
                            var loImangen = loRepImagen.Create(oProductoEdicion.Imagen);
                            oProductoEdicion.COD_IMAGEN = loImangen.ID_IMAGEN;
                        }
                    }
                }

                using (var rep = new Repository<ProductoEdicion>())
                {
                    bRes = rep.Update(oProductoEdicion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

                    if (oProductoEdicionAux.CANTIDAD_DISPONIBLE < 1)
                    {
                        oProductoEdicionAux.COD_ESTADO = 2;
                        if (fechaDevolucionReal != null)
                            oProductoEdicionAux.FECHA_DEVOLUCION_REAL = fechaDevolucionReal;
                        else if (oProductoEdicion.FECHA_DEVOLUCION_REAL != null)
                            oProductoEdicionAux.FECHA_DEVOLUCION_REAL = oProductoEdicion.FECHA_DEVOLUCION_REAL;
                    }

                    bRes = ModificarProductoEdicion(oProductoEdicionAux);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                throw ex;
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
                throw ex;
            }

            return oProductoEdicion;
        }

        public List<ProdEdicionCustomersWebSite> ObtenerEdiciones(long codigo_tipo_producto, long codigo_producto)
        {
            List<ProdEdicionCustomersWebSite> lstProdEdicionColeccion = null;
            List<ProductoEdicion> lstProductoEdicion = null;

            try
            {
                using (var loRepProductoEdicion = new Repository<ProductoEdicion>())
                {
                    // conultar si se debe controlar Sock:p.CANTIDAD_DISPONIBLE > 0, me parece que no!!
                    lstProductoEdicion = loRepProductoEdicion.Search(p => p.COD_ESTADO == 1 && p.COD_TIPO_PRODUCTO == codigo_tipo_producto && p.COD_PRODUCTO == codigo_producto);

                    ProdEdicionCustomersWebSite oProdEdicionColeccionCustomersWebSite;
                    lstProdEdicionColeccion = new List<ProdEdicionCustomersWebSite>();

                    foreach (var loProductoEdicion in lstProductoEdicion)
                    {
                        oProdEdicionColeccionCustomersWebSite = new ProdEdicionCustomersWebSite
                        {
                            COD_PRODUCTO_EDICION = loProductoEdicion.ID_PRODUCTO_EDICION,
                            COD_PRODUCTO = Convert.ToInt32(codigo_producto),
                            PRECIO = string.Format(System.Globalization.CultureInfo.GetCultureInfo("de-DE"), "{0:0.00}", loProductoEdicion.PRECIO),
                            EDICION = loProductoEdicion.EDICION,
                            CANTIDAD_DISPONIBLE = loProductoEdicion.CANTIDAD_DISPONIBLE
                        };

                        if (loProductoEdicion.DESCRIPCION != null)
                            oProdEdicionColeccionCustomersWebSite.DESCRIPCION = loProductoEdicion.DESCRIPCION;

                        oProdEdicionColeccionCustomersWebSite.IMAGEN = new System.Web.UI.WebControls.Image();

                        if (loProductoEdicion.Imagen != null)
                        {
                            // Covertir la iamgen a un base 64 para mostrarlo en un dato binario
                            string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(loProductoEdicion.Imagen.IMAGEN1);
                            oProdEdicionColeccionCustomersWebSite.IMAGEN.ImageUrl = loImagenDataURL64;
                        }

                        // COntrolar que en la Fecha_Edicion no sea Null y dar formato a la fecha
                        oProdEdicionColeccionCustomersWebSite.FECHA_EDICION = loProductoEdicion.FECHA_EDICION?.ToString("dd/MM/yyyy") ?? string.Empty;

                        lstProdEdicionColeccion.Add(oProdEdicionColeccionCustomersWebSite);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstProdEdicionColeccion;
        }

        public ProductoEdicion ObtenerProductoEdicionPorId(long codProductoEdicion)
        {
            ProductoEdicion oProductoEdicion = new ProductoEdicion();

            try
            {
                using (var loRepProductoEdicion = new Repository<ProductoEdicion>())
                {
                    oProductoEdicion = loRepProductoEdicion.Find(p => p.ID_PRODUCTO_EDICION == codProductoEdicion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oProductoEdicion;
        }

        #endregion
    }

    #region Clases

    public class ProdEdicionCustomersWebSite
    {
        public System.Web.UI.WebControls.Image IMAGEN { get; set; }
        public int COD_PRODUCTO_EDICION { get; set; }
        public int COD_PRODUCTO { get; set; }
        public int COD_TIPO_PRODUCTO { get; set; }
        public string DESCRIPCION { get; set; }
        public string EDICION { get; set; }
        public string PRECIO { get; set; }
        public string FECHA_EDICION { get; set; }
        public int CANTIDAD_DISPONIBLE { get; set; }
    }

    #endregion
}
