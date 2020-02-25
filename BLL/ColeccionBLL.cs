using BLL.DAL;
using BLL.Filters;
using System;
using System.Transactions;
using System.Collections.Generic;

namespace BLL
{
    public class ColeccionBLL
    {
        #region Métodos Públicos

        public ProductoColeccion ObtenerColeccion(long idProducto)
        {
            Producto oProducto = null;
            Coleccion oColeccion = null;
            ProductoColeccion oProductoColeccion = null;

            try
            {
                using (var loRepProducto = new Repository<Producto>())
                {
                    oProducto = loRepProducto.Find(x => x.ID_PRODUCTO == idProducto);

                    using (var loRepColeccion = new Repository<Coleccion>())
                    {
                        oColeccion = loRepColeccion.Find(x => x.COD_PRODUCTO == oProducto.ID_PRODUCTO);

                        oProductoColeccion = new ProductoColeccion
                        {
                            ID_PRODUCTO = oProducto.ID_PRODUCTO,
                            FECHA_ALTA = oProducto.FECHA_ALTA,
                            NOMBRE = oProducto.NOMBRE,
                            DESCRIPCION = oProducto.DESCRIPCION,
                            COD_ESTADO = oProducto.COD_ESTADO,
                            COD_GENERO = oProducto.COD_GENERO,
                            COD_PROVEEDOR = oProducto.COD_PROVEEDOR,
                            COD_TIPO_PRODUCTO = oProducto.COD_TIPO_PRODUCTO,
                            ID_COLECCION = oColeccion.ID_COLECCION,
                            COD_PERIODICIDAD = oColeccion.COD_PERIODICIDAD,
                            ID_DIA_SEMANA = oColeccion.ID_DIA_SEMANA,
                            CANTIDAD_DE_ENTREGAS = oColeccion.CANTIDAD_ENTREGAS
                        };

                        if (oProducto.COD_IMAGEN != null)
                            oProductoColeccion.IMAGEN = oProducto.Imagen;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oProductoColeccion;
        }

        public List<ColeccionEdicion> ObtenerColeccionesParaEdicion(ProductoFiltro oProductoFiltro)
        {
            List<Producto> lstProductos = null;
            List<ColeccionEdicion> lstColeccionEdicion = null;

            try
            {
                using (var loRepProducto = new Repository<Producto>())
                {
                    lstProductos = loRepProducto.Search(p => p.FECHA_BAJA == null && p.COD_ESTADO == 1);

                    if (oProductoFiltro.CodProveedor > 0 && lstProductos.Count > 0)
                        lstProductos = lstProductos.FindAll(p => p.COD_PROVEEDOR == oProductoFiltro.CodProveedor);

                    if (oProductoFiltro.CodTipoProducto > 0 && lstProductos.Count > 0)
                        lstProductos = lstProductos.FindAll(p => p.COD_TIPO_PRODUCTO == oProductoFiltro.CodTipoProducto);

                    if (!String.IsNullOrEmpty(oProductoFiltro.NombreProducto) && lstProductos.Count > 0)
                        lstProductos = lstProductos.FindAll(p => p.NOMBRE.ToUpper().Contains(oProductoFiltro.NombreProducto.ToUpper()));
                }

                ColeccionEdicion oColeccionEdicion;
                lstColeccionEdicion = new List<ColeccionEdicion>();

                foreach (var loProducto in lstProductos)
                {
                    oColeccionEdicion = new ColeccionEdicion
                    {
                        COD_PRODUCTO = loProducto.ID_PRODUCTO,
                        NOMBRE = loProducto.NOMBRE
                    };

                    lstColeccionEdicion.Add(oColeccionEdicion);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstColeccionEdicion;
        }

        public List<ColeccionEdicion> ObtenerColeccionesEdicion(ProductoFiltro oProductoFiltro)
        {
            List<ColeccionEdicion> lstColeccionEdicion = null;
            List<ProductoEdicion> lstProductoEdicion = null;

            try
            {
                using (var loRepProductoEdicion = new Repository<ProductoEdicion>())
                {
                    lstProductoEdicion = loRepProductoEdicion.Search(p => p.COD_ESTADO == 1 && p.COD_TIPO_PRODUCTO == oProductoFiltro.CodTipoProducto && p.Producto.COD_PROVEEDOR == oProductoFiltro.CodProveedor && p.CANTIDAD_DISPONIBLE > 0);

                    if (!String.IsNullOrEmpty(oProductoFiltro.NombreEdicion) && lstProductoEdicion.Count > 0)
                        lstProductoEdicion = lstProductoEdicion.FindAll(p => p.EDICION.ToUpper().Contains(oProductoFiltro.NombreEdicion.ToUpper()));

                    if (!String.IsNullOrEmpty(oProductoFiltro.DescripcionEdicion) && lstProductoEdicion.Count > 0)
                        lstProductoEdicion = lstProductoEdicion.FindAll(p => !string.IsNullOrEmpty(p.DESCRIPCION) && p.DESCRIPCION.ToUpper().Contains(oProductoFiltro.DescripcionEdicion.ToUpper()));

                    ColeccionEdicion oColeccionEdicion;
                    lstColeccionEdicion = new List<ColeccionEdicion>();

                    foreach (var loProductoEdicion in lstProductoEdicion)
                    {
                        // Filtro por Nombre de Producto
                        if ((String.IsNullOrEmpty(oProductoFiltro.NombreProducto)) || (!String.IsNullOrEmpty(oProductoFiltro.NombreProducto) && loProductoEdicion.Producto.NOMBRE.ToUpper().Contains(oProductoFiltro.NombreProducto.ToUpper())))
                        {
                            // Filtro por Descripción del Producto
                            if ((String.IsNullOrEmpty(oProductoFiltro.DescripcionProducto)) || (!String.IsNullOrEmpty(oProductoFiltro.DescripcionProducto) && !string.IsNullOrEmpty(loProductoEdicion.Producto.DESCRIPCION) && loProductoEdicion.Producto.DESCRIPCION.ToUpper().Contains(oProductoFiltro.DescripcionProducto.ToUpper())))
                            {
                                oColeccionEdicion = new ColeccionEdicion
                                {
                                    COD_PRODUCTO = loProductoEdicion.COD_PRODUCTO,
                                    COD_PRODUCTO_EDICION = loProductoEdicion.ID_PRODUCTO_EDICION,
                                    NOMBRE = loProductoEdicion.Producto.NOMBRE, //nombre del Producto
                                    TIPO_PRODUCTO = loProductoEdicion.Producto.TipoProducto.DESCRIPCION,
                                    EDICION = loProductoEdicion.EDICION,
                                    PRECIO = "$" + loProductoEdicion.PRECIO.ToString(),
                                    CANTIDAD_DISPONIBLE = loProductoEdicion.CANTIDAD_DISPONIBLE
                                };

                                if (!String.IsNullOrEmpty(loProductoEdicion.DESCRIPCION))
                                    oColeccionEdicion.DESCRIPCION = loProductoEdicion.DESCRIPCION;

                                lstColeccionEdicion.Add(oColeccionEdicion);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return lstColeccionEdicion;
        }

        public bool AltaColeccion(Producto oProducto, Coleccion oColeccion)
        {
            var bRes = false;

            try
            {
                using (TransactionScope loTransactionScope = new TransactionScope())
                {
                    using (var loRepProducto = new Repository<Producto>())
                    {
                        bRes = loRepProducto.Create(oProducto) != null;

                        if (bRes)
                        {
                            using (var loRepColeccion = new Repository<Coleccion>())
                            {
                                oColeccion.COD_PRODUCTO = oProducto.ID_PRODUCTO;
                                bRes = loRepColeccion.Create(oColeccion) != null;
                            }
                        }
                    }

                    loTransactionScope.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public bool ModificarColeccion(Producto oProducto, Coleccion oColeccion)
        {
            var bRes = false;

            try
            {
                using (TransactionScope loTransactionScope = new TransactionScope())
                {
                    if (oProducto.Imagen != null)
                    {
                        using (var loRepImagen = new Repository<Imagen>())
                        {
                            var loImangen = loRepImagen.Create(oProducto.Imagen);
                            oProducto.COD_IMAGEN = loImangen.ID_IMAGEN;
                        }
                    }

                    using (var loRepProducto = new Repository<Producto>())
                    {
                        bRes = loRepProducto.Update(oProducto);

                        if (bRes)
                        {
                            using (var loRepColeccion = new Repository<Coleccion>())
                            {
                                bRes = loRepColeccion.Update(oColeccion);
                            }
                        }
                    }

                    loTransactionScope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bRes;
        }

        #endregion
    }

    #region Clases

    public class ProductoColeccion
    {
        public int ID_PRODUCTO { get; set; }
        public DateTime FECHA_ALTA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public int COD_ESTADO { get; set; }
        public int COD_GENERO { get; set; }
        public int COD_PROVEEDOR { get; set; }
        public int COD_TIPO_PRODUCTO { get; set; }
        public int ID_COLECCION { get; set; }
        public int COD_PERIODICIDAD { get; set; }
        public int? ID_DIA_SEMANA { get; set; }
        public int CANTIDAD_DE_ENTREGAS { get; set; }
        public BLL.DAL.Imagen IMAGEN { get; set; }
    }

    public class ColeccionEdicion
    {
        public int COD_PRODUCTO { get; set; }
        public int COD_PRODUCTO_EDICION { get; set; }
        public string NOMBRE { get; set; }
        public string TIPO_PRODUCTO { get; set; }
        public string EDICION { get; set; }
        public System.DateTime FECHA_EDICION { get; set; }
        public string DESCRIPCION { get; set; }
        public string PRECIO { get; set; }
        public int CANTIDAD_DISPONIBLE { get; set; }
        public int CANTIDAD { get; set; }
        public System.DateTime? FECHA_DEVOLUCION { get; set; }
        public System.Web.UI.WebControls.Image IMAGEN { get; set; }
        public string TITULO { get; set; }
    }

    #endregion
}
