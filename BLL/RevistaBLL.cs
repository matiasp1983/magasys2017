using BLL.DAL;
using BLL.Filters;
using System;
using System.Transactions;
using System.Collections.Generic;

namespace BLL
{
    public class RevistaBLL
    {
        #region Métodos Públicos

        public ProductoRevista ObtenerRevista(long idProducto)
        {
            Producto oProducto = null;
            Revista oRevista = null;
            ProductoRevista oProductoRevista = null;

            try
            {
                using (var loRepProducto = new Repository<Producto>())
                {
                    oProducto = loRepProducto.Find(x => x.ID_PRODUCTO == idProducto);

                    using (var loRepRevista = new Repository<Revista>())
                    {
                        oRevista = loRepRevista.Find(x => x.COD_PRODUCTO == oProducto.ID_PRODUCTO);

                        oProductoRevista = new ProductoRevista
                        {
                            ID_PRODUCTO = oProducto.ID_PRODUCTO,
                            FECHA_ALTA = oProducto.FECHA_ALTA,
                            NOMBRE = oProducto.NOMBRE,
                            DESCRIPCION = oProducto.DESCRIPCION,
                            COD_ESTADO = oProducto.COD_ESTADO,
                            COD_GENERO = oProducto.COD_GENERO,
                            COD_PROVEEDOR = oProducto.COD_PROVEEDOR,
                            COD_TIPO_PRODUCTO = oProducto.COD_TIPO_PRODUCTO,
                            ID_REVISTA = oRevista.ID_REVISTA,
                            COD_PERIODICIDAD = oRevista.COD_PERIODICIDAD,
                            ID_DIA_SEMANA = oRevista.ID_DIA_SEMANA,
                            PRECIO = oRevista.PRECIO
                        };
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oProductoRevista;
        }

        public List<RevistaEdicion> ObtenerRevistasParaEdicion(ProductoFiltro oProductoFiltro)
        {
            List<Producto> lstProductos = null;
            List<RevistaEdicion> lstRevistaEdicion = null;

            try
            {
                using (var loRepProducto = new Repository<Producto>())
                {
                    lstProductos = loRepProducto.Search(p => p.FECHA_BAJA == null && p.COD_ESTADO == 1);

                    if (oProductoFiltro.CodProveedor > 0 && lstProductos.Count > 0)
                        lstProductos = lstProductos.FindAll(p => p.COD_PROVEEDOR == oProductoFiltro.CodProveedor);

                    if (oProductoFiltro.CodTipoProducto > 0 && lstProductos.Count > 0)
                        lstProductos = lstProductos.FindAll(p => p.COD_TIPO_PRODUCTO == oProductoFiltro.CodTipoProducto);

                    if (!String.IsNullOrEmpty(oProductoFiltro.Nombre) && lstProductos.Count > 0)
                        lstProductos = lstProductos.FindAll(p => p.NOMBRE.ToUpper().Contains(oProductoFiltro.Nombre.ToUpper()));
                }

                RevistaEdicion oRevistaEdicion;
                lstRevistaEdicion = new List<RevistaEdicion>();

                foreach (var loProducto in lstProductos)
                {
                    oRevistaEdicion = new RevistaEdicion
                    {
                        COD_PRODUCTO = loProducto.ID_PRODUCTO,
                        NOMBRE = loProducto.NOMBRE
                    };

                    lstRevistaEdicion.Add(oRevistaEdicion);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstRevistaEdicion;
        }

        public bool AltaRevista(Producto oProducto, Revista oRevista)
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
                            using (var loRepRevista = new Repository<Revista>())
                            {
                                oRevista.COD_PRODUCTO = oProducto.ID_PRODUCTO;
                                bRes = loRepRevista.Create(oRevista) != null;
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

        public bool ModificarRevista(Producto oProducto, Revista oRevista)
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
                            using (var loRepRevista = new Repository<Revista>())
                            {
                                bRes = loRepRevista.Update(oRevista);
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

    public class ProductoRevista
    {
        public int ID_PRODUCTO { get; set; }
        public DateTime FECHA_ALTA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public int COD_ESTADO { get; set; }
        public int COD_GENERO { get; set; }
        public int COD_PROVEEDOR { get; set; }
        public int COD_TIPO_PRODUCTO { get; set; }
        public int ID_REVISTA { get; set; }
        public int COD_PERIODICIDAD { get; set; }
        public int? ID_DIA_SEMANA { get; set; }
        public double PRECIO { get; set; }
    }

    public class RevistaEdicion
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
