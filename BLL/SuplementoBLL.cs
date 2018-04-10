using BLL.DAL;
using System;
using System.Transactions;

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
                            ID_DIA_SEMANA = oSuplemento.ID_DIA_SEMANA
                        };
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oProductoSuplemento;
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
    }

    #endregion
}
