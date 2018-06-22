using BLL.DAL;
using BLL.Filters;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace BLL
{
    public class ProductoIngresoBLL
    {
        #region Métodos Públicos

        public bool AltaIngreso(ProductoIngreso oProductoIngreso, List<DetalleProductoIngreso> lstDetalleProductoIngreso)
        {
            var bRes = false;

            try
            {
                using (TransactionScope loTransactionScope = new TransactionScope())
                {
                    using (var loRepProductoIngreso = new Repository<ProductoIngreso>())
                    {
                        bRes = loRepProductoIngreso.Create(oProductoIngreso) != null;

                        if (bRes)
                        {
                            using (var loRepDetalleProductoIngreso = new Repository<DetalleProductoIngreso>())
                            {
                                foreach (var oDetalleIngresoProducto in lstDetalleProductoIngreso)
                                {
                                    oDetalleIngresoProducto.COD_INGRESO_PRODUCTO = oProductoIngreso.ID_INGRESO_PRODUCTOS;
                                    bRes = loRepDetalleProductoIngreso.Create(oDetalleIngresoProducto) != null;
                                    if (bRes == false)
                                        break;
                                }
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
