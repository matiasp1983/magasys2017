﻿using BLL.DAL;
using System;
using System.Transactions;

namespace BLL
{
    public class RevistaBLL
    {
        #region Métodos Públicos

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

        #endregion
    }
}
