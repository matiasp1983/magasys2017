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
            catch (Exception)
            {
                throw;
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
    }

    public class ColeccionEdicion
    {
        public int COD_PRODUCTO { get; set; }
        public string NOMBRE { get; set; }
        public string TIPO_PRODUCTO { get; set; }
        public string EDICION { get; set; }
        public System.DateTime FECHA_EDICION { get; set; }
        public string DESCRIPCION { get; set; }
        public double PRECIO { get; set; }
        public int CANTIDAD_DISPONIBLE { get; set; }
        public System.DateTime? FECHA_DEVOLUCION { get; set; }
    }

    #endregion
}
