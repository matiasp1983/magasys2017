using BLL.DAL;
using System;
using System.Transactions;

namespace BLL
{
    public class LibroBLL
    {
        #region Métodos Públicos

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

        #endregion
    }
}
