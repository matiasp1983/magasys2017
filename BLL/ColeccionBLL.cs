using BLL.DAL;
using System;
using System.Transactions;

namespace BLL
{
    public class ColeccionBLL
    {
        #region Métodos Públicos

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

        #endregion
    }
}
