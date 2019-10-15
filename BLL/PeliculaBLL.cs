using BLL.DAL;
using BLL.Filters;
using System;
using System.Transactions;
using System.Collections.Generic;

namespace BLL
{
    public class PeliculaBLL
    {
        #region Métodos Públicos

        public ProductoPelicula ObtenerPelicula(long idProducto)
        {
            Producto oProducto = null;
            Pelicula oPelicula = null;
            ProductoPelicula oProductoPelicula = null;

            try
            {
                using (var loRepProducto = new Repository<Producto>())
                {
                    oProducto = loRepProducto.Find(x => x.ID_PRODUCTO == idProducto);

                    using (var loRepPelicula = new Repository<Pelicula>())
                    {
                        oPelicula = loRepPelicula.Find(x => x.COD_PRODUCTO == oProducto.ID_PRODUCTO);

                        oProductoPelicula = new ProductoPelicula
                        {
                            ID_PRODUCTO = oProducto.ID_PRODUCTO,
                            FECHA_ALTA = oProducto.FECHA_ALTA,
                            NOMBRE = oProducto.NOMBRE,
                            DESCRIPCION = oProducto.DESCRIPCION,
                            COD_ESTADO = oProducto.COD_ESTADO,
                            COD_GENERO = oProducto.COD_GENERO,
                            COD_PROVEEDOR = oProducto.COD_PROVEEDOR,
                            COD_TIPO_PRODUCTO = oProducto.COD_TIPO_PRODUCTO,
                            ID_PELICULA = oPelicula.ID_PELICULA,
                            ANIO = oPelicula.ANIO,
                            PRECIO = oPelicula.PRECIO
                        };

                        if (oProducto.COD_IMAGEN != null)
                            oProductoPelicula.IMAGEN = oProducto.Imagen;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oProductoPelicula;
        }

        public List<PeliculaEdicion> ObtenerPeliculasParaEdicion(ProductoFiltro oProductoFiltro)
        {
            List<Producto> lstProductos = null;
            List<PeliculaEdicion> lstPeliculaEdicion = null;

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

                PeliculaEdicion oPeliculaEdicion;
                lstPeliculaEdicion = new List<PeliculaEdicion>();

                foreach (var loProducto in lstProductos)
                {
                    oPeliculaEdicion = new PeliculaEdicion
                    {
                        COD_PRODUCTO = loProducto.ID_PRODUCTO,
                        NOMBRE = loProducto.NOMBRE,
                    };

                    lstPeliculaEdicion.Add(oPeliculaEdicion);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstPeliculaEdicion;
        }

        public List<PeliculaEdicion> ObtenerPeliculasEdicion(ProductoFiltro oProductoFiltro)
        {
            List<PeliculaEdicion> lstPeliculaEdicion = null;
            List<ProductoEdicion> lstProductoEdicion = null;

            try
            {
                using (var loRepProductoEdicion = new Repository<ProductoEdicion>())
                {
                    lstProductoEdicion = loRepProductoEdicion.Search(p => p.COD_ESTADO == 1 && p.COD_TIPO_PRODUCTO == oProductoFiltro.CodTipoProducto && p.Producto.COD_PROVEEDOR == oProductoFiltro.CodProveedor);

                    if (!String.IsNullOrEmpty(oProductoFiltro.NombreEdicion) && lstProductoEdicion.Count > 0)
                        lstProductoEdicion = lstProductoEdicion.FindAll(p => p.EDICION.ToUpper().Contains(oProductoFiltro.NombreEdicion.ToUpper()));

                    if (!String.IsNullOrEmpty(oProductoFiltro.DescripcionEdicion) && lstProductoEdicion.Count > 0)
                        lstProductoEdicion = lstProductoEdicion.FindAll(p => !string.IsNullOrEmpty(p.DESCRIPCION) && p.DESCRIPCION.ToUpper().Contains(oProductoFiltro.DescripcionEdicion.ToUpper()));

                    PeliculaEdicion oPeliculaEdicion;
                    lstPeliculaEdicion = new List<PeliculaEdicion>();

                    foreach (var loProductoEdicion in lstProductoEdicion)
                    {
                        // Filtro por Nombre de Producto
                        if ((String.IsNullOrEmpty(oProductoFiltro.NombreProducto)) || (!String.IsNullOrEmpty(oProductoFiltro.NombreProducto) && loProductoEdicion.Producto.NOMBRE.ToUpper().Contains(oProductoFiltro.NombreProducto.ToUpper())))
                        {
                            // Filtro por Descripción del Producto
                            if ((String.IsNullOrEmpty(oProductoFiltro.DescripcionProducto)) || (!String.IsNullOrEmpty(oProductoFiltro.DescripcionProducto) && !string.IsNullOrEmpty(loProductoEdicion.Producto.DESCRIPCION) && loProductoEdicion.Producto.DESCRIPCION.ToUpper().Contains(oProductoFiltro.DescripcionProducto.ToUpper())))
                            {
                                oPeliculaEdicion = new PeliculaEdicion
                                {
                                    COD_PRODUCTO = loProductoEdicion.COD_PRODUCTO,
                                    COD_PRODUCTO_EDICION = loProductoEdicion.ID_PRODUCTO_EDICION,
                                    NOMBRE = loProductoEdicion.Producto.NOMBRE, //nombre del Producto
                                    TIPO_PRODUCTO = loProductoEdicion.Producto.TipoProducto.DESCRIPCION,
                                    EDICION = loProductoEdicion.EDICION,
                                    PRECIO = "$ " + loProductoEdicion.PRECIO.ToString(),
                                    CANTIDAD_DISPONIBLE = loProductoEdicion.CANTIDAD_DISPONIBLE
                                };

                                if (loProductoEdicion.FECHA_EDICION != null)
                                    oPeliculaEdicion.FECHA_EDICION = Convert.ToDateTime(loProductoEdicion.FECHA_EDICION);

                                lstPeliculaEdicion.Add(oPeliculaEdicion);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return lstPeliculaEdicion;
        }

        public bool AltaPelicula(Producto oProducto, Pelicula oPelicula)
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
                            using (var loRepPelicula = new Repository<Pelicula>())
                            {
                                oPelicula.COD_PRODUCTO = oProducto.ID_PRODUCTO;
                                bRes = loRepPelicula.Create(oPelicula) != null;
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

        public bool ModificarPelicula(Producto oProducto, Pelicula oPelicula)
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
                            using (var loRepPelicuala = new Repository<Pelicula>())
                            {
                                bRes = loRepPelicuala.Update(oPelicula);
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

        #endregion
    }

    #region Clases

    public class ProductoPelicula
    {
        public int ID_PRODUCTO { get; set; }
        public DateTime FECHA_ALTA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public int COD_ESTADO { get; set; }
        public int COD_GENERO { get; set; }
        public int COD_PROVEEDOR { get; set; }
        public int COD_TIPO_PRODUCTO { get; set; }
        public int ID_PELICULA { get; set; }
        public int ANIO { get; set; }
        public double PRECIO { get; set; }
        public BLL.DAL.Imagen IMAGEN { get; set; }
    }

    public class PeliculaEdicion
    {
        public int COD_PRODUCTO { get; set; }
        public int COD_PRODUCTO_EDICION { get; set; }
        public string NOMBRE { get; set; }
        public string TIPO_PRODUCTO { get; set; }
        public string EDICION { get; set; }
        public DateTime? FECHA_EDICION { get; set; }
        public string DESCRIPCION { get; set; }
        public string PRECIO { get; set; }
        public int CANTIDAD_DISPONIBLE { get; set; }
        public int CANTIDAD { get; set; }
        public DateTime? FECHA_DEVOLUCION { get; set; }
        public System.Web.UI.WebControls.Image IMAGEN { get; set; }
        public string TITULO { get; set; }
    }

    #endregion
}
