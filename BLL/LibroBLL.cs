using BLL.DAL;
using System;
using System.Transactions;

namespace BLL
{
    public class LibroBLL
    {
        #region Métodos Públicos

        public ProductoLibro ObtenerLibro(long idProducto)
        {
            Producto oProducto = null;
            Libro oLibro = null;
            ProductoLibro oProductoLibro = null;

            try
            {
                using (var loRepProducto = new Repository<Producto>())
                {
                    oProducto = loRepProducto.Find(x => x.ID_PRODUCTO == idProducto);

                    using (var loRepLibro = new Repository<Libro>())
                    {
                        oLibro = loRepLibro.Find(x => x.COD_PRODUCTO == oProducto.ID_PRODUCTO);

                        oProductoLibro = new ProductoLibro
                        {
                            ID_PRODUCTO = oProducto.ID_PRODUCTO,
                            FECHA_ALTA = oProducto.FECHA_ALTA,
                            NOMBRE = oProducto.NOMBRE,
                            DESCRIPCION = oProducto.DESCRIPCION,
                            COD_ESTADO = oProducto.COD_ESTADO,
                            COD_GENERO = oProducto.COD_GENERO,
                            COD_PROVEEDOR = oProducto.COD_PROVEEDOR,
                            COD_TIPO_PRODUCTO = oProducto.COD_TIPO_PRODUCTO,
                            ID_LIBRO = oLibro.ID_LIBRO,
                            AUTOR = oLibro.AUTOR,
                            ANIO = oLibro.ANIO,
                            EDITORIAL = oLibro.EDITORIAL,
                            PRECIO = oLibro.PRECIO
                        };
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oProductoLibro;
        }

        public bool AltaLibro(Producto oProducto, Libro oLibro)
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
                            using (var loRepLibro = new Repository<Libro>())
                            {
                                oLibro.COD_PRODUCTO = oProducto.ID_PRODUCTO;
                                bRes = loRepLibro.Create(oLibro) != null;
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

        public bool ModificarLibro(Producto oProducto, Libro oLibro)
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
                            using (var loRepLibro = new Repository<Libro>())
                            {
                                bRes = loRepLibro.Update(oLibro);
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

    public class ProductoLibro
    {
        public int ID_PRODUCTO { get; set; }
        public DateTime FECHA_ALTA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public int COD_ESTADO { get; set; }
        public int COD_GENERO { get; set; }
        public int COD_PROVEEDOR { get; set; }
        public int COD_TIPO_PRODUCTO { get; set; }
        public int ID_LIBRO { get; set; }
        public string AUTOR { get; set; }
        public int ANIO { get; set; }
        public string EDITORIAL { get; set; }
        public double PRECIO { get; set; }
    }

    #endregion
}
