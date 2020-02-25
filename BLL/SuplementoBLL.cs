using BLL.DAL;
using BLL.Filters;
using System;
using System.Transactions;
using System.Collections.Generic;

namespace BLL
{
    public class SuplementoBLL
    {
        #region Métodos Públicos

        public ProductoSuplemento ObtenerSuplemento(long idProducto)
        {
            Producto oProducto = null;
            Suplemento oSuplemento = null;
            ProductoSuplemento oProductoSuplemento = null;

            try
            {
                using (var loRepProducto = new Repository<Producto>())
                {
                    oProducto = loRepProducto.Find(x => x.ID_PRODUCTO == idProducto);

                    using (var loRepSuplemento = new Repository<Suplemento>())
                    {
                        oSuplemento = loRepSuplemento.Find(x => x.COD_PRODUCTO == oProducto.ID_PRODUCTO);

                        oProductoSuplemento = new ProductoSuplemento
                        {
                            ID_PRODUCTO = oProducto.ID_PRODUCTO,
                            FECHA_ALTA = oProducto.FECHA_ALTA,
                            NOMBRE = oProducto.NOMBRE,
                            DESCRIPCION = oProducto.DESCRIPCION,
                            COD_ESTADO = oProducto.COD_ESTADO,
                            COD_GENERO = oProducto.COD_GENERO,
                            COD_PROVEEDOR = oProducto.COD_PROVEEDOR,
                            COD_TIPO_PRODUCTO = oProducto.COD_TIPO_PRODUCTO,
                            ID_SUPLEMENTO = oSuplemento.ID_SUPLEMENTO,
                            CANTIDAD_DE_ENTREGAS = oSuplemento.CANTIDAD_ENTREGAS,
                            COD_DIARIO = oSuplemento.COD_DIARIO,
                            PRECIO = oSuplemento.PRECIO,
                            ID_DIA_SEMANA = oSuplemento.ID_DIA_SEMANA,
                            NOMBRE_DIARIO = oSuplemento.DiarioDiaSemana.Producto.NOMBRE
                        };

                        if (oProducto.COD_IMAGEN != null)
                            oProductoSuplemento.IMAGEN = oProducto.Imagen;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oProductoSuplemento;
        }

        public List<SuplementoEdicion> ObtenerSuplementosParaEdicion(ProductoFiltro oProductoFiltro)
        {
            List<Producto> lstProductos = null;
            List<SuplementoEdicion> lstSuplementoEdicion = null;

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

                SuplementoEdicion oSuplementoEdicion;
                lstSuplementoEdicion = new List<SuplementoEdicion>();

                foreach (var loProducto in lstProductos)
                {
                    oSuplementoEdicion = new SuplementoEdicion
                    {
                        COD_PRODUCTO = loProducto.ID_PRODUCTO,
                        NOMBRE = loProducto.NOMBRE,
                    };

                    lstSuplementoEdicion.Add(oSuplementoEdicion);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstSuplementoEdicion;
        }

        public List<SuplementoEdicion> ObtenerSuplementosEdicion(ProductoFiltro oProductoFiltro)
        {
            List<SuplementoEdicion> lstSuplementoEdicion = null;
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

                    SuplementoEdicion oSuplementoEdicion;
                    lstSuplementoEdicion = new List<SuplementoEdicion>();

                    foreach (var loProductoEdicion in lstProductoEdicion)
                    {
                        // Filtro por Nombre de Producto
                        if ((String.IsNullOrEmpty(oProductoFiltro.NombreProducto)) || (!String.IsNullOrEmpty(oProductoFiltro.NombreProducto) && loProductoEdicion.Producto.NOMBRE.ToUpper().Contains(oProductoFiltro.NombreProducto.ToUpper())))
                        {
                            // Filtro por Descripción del Producto
                            if ((String.IsNullOrEmpty(oProductoFiltro.DescripcionProducto)) || (!String.IsNullOrEmpty(oProductoFiltro.DescripcionProducto) && !string.IsNullOrEmpty(loProductoEdicion.Producto.DESCRIPCION) && loProductoEdicion.Producto.DESCRIPCION.ToUpper().Contains(oProductoFiltro.DescripcionProducto.ToUpper())))
                            {
                                oSuplementoEdicion = new SuplementoEdicion
                                {
                                    COD_PRODUCTO = loProductoEdicion.COD_PRODUCTO,
                                    COD_PRODUCTO_EDICION = loProductoEdicion.ID_PRODUCTO_EDICION,
                                    NOMBRE = loProductoEdicion.Producto.NOMBRE, //nombre del Producto
                                    TIPO_PRODUCTO = loProductoEdicion.Producto.TipoProducto.DESCRIPCION,
                                    EDICION = loProductoEdicion.EDICION,
                                    PRECIO = "$" + loProductoEdicion.PRECIO.ToString(),
                                    CANTIDAD_DISPONIBLE = loProductoEdicion.CANTIDAD_DISPONIBLE
                                };

                                if (loProductoEdicion.FECHA_EDICION != null)
                                    oSuplementoEdicion.FECHA_EDICION = Convert.ToDateTime(loProductoEdicion.FECHA_EDICION);

                                if (!String.IsNullOrEmpty(loProductoEdicion.DESCRIPCION))
                                    oSuplementoEdicion.DESCRIPCION = loProductoEdicion.DESCRIPCION;

                                lstSuplementoEdicion.Add(oSuplementoEdicion);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return lstSuplementoEdicion;
        }

        public bool AltaSuplemento(Producto oProducto, Suplemento oSuplemento)
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
                            using (var loRepSuplemento = new Repository<Suplemento>())
                            {
                                oSuplemento.COD_PRODUCTO = oProducto.ID_PRODUCTO;
                                bRes = loRepSuplemento.Create(oSuplemento) != null;
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

        public bool ModificarSuplemento(Producto oProducto, Suplemento oSuplemento)
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
                            using (var loRepSuplemento = new Repository<Suplemento>())
                            {
                                bRes = loRepSuplemento.Update(oSuplemento);
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

    public class ProductoSuplemento
    {
        public int ID_PRODUCTO { get; set; }
        public DateTime FECHA_ALTA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public int COD_ESTADO { get; set; }
        public int COD_GENERO { get; set; }
        public int COD_PROVEEDOR { get; set; }
        public int COD_TIPO_PRODUCTO { get; set; }
        public int ID_SUPLEMENTO { get; set; }
        public int? ID_DIA_SEMANA { get; set; }
        public int COD_DIARIO { get; set; }
        public double PRECIO { get; set; }
        public int CANTIDAD_DE_ENTREGAS { get; set; }
        public BLL.DAL.Imagen IMAGEN { get; set; }
        public string NOMBRE_DIARIO { get; set; }
    }

    public class SuplementoEdicion
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