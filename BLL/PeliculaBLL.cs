using BLL.DAL;
using System;
using System.Transactions;

namespace BLL
{
    public class PeliculaBLL
    {
        #region Métodos Públicos

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

        #endregion
    }
}
