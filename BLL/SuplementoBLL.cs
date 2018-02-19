using BLL.DAL;
using System;
using System.Transactions;

namespace BLL
{
    public class SuplementoBLL
    {
        #region Métodos Públicos

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

        #endregion
    }
}
