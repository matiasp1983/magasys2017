using System;
using BLL.DAL;
using System.Collections.Generic;
using System.Transactions;
using System.Linq;

namespace BLL
{
    public class VentaBLL
    {
        #region Métodos Públicos

        public int ObtenerUltimaVenta()
        {
            List<Venta> lstVenta = null;
            int loIdVenta = 0;

            try
            {
                using (var rep = new Repository<Venta>())
                {
                    // Ordenar la lista descendentemente
                    lstVenta = rep.Search(p => p.COD_ESTADO == 1).OrderByDescending(p => p.ID_VENTA).ToList();

                    if (lstVenta.Count > 0)
                        loIdVenta = lstVenta[0].ID_VENTA + 1;
                    else
                        loIdVenta = 1;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return loIdVenta;
        }

        public bool AltaVenta(Venta oVenta, List<DetalleVenta> lstDetalleVenta)
        {
            var bRes = false;

            try
            {
                using (TransactionScope loTransactionScope = new TransactionScope())
                {
                    using (var loRepVenta = new Repository<Venta>())
                    {
                        bRes = loRepVenta.Create(oVenta) != null;

                        if (bRes)
                        {
                            using (var loRepDetalleVenta = new Repository<DetalleVenta>())
                            {
                                foreach (var oDetalleVenta in lstDetalleVenta)
                                {
                                    oDetalleVenta.COD_VENTA = oVenta.ID_VENTA;
                                    bRes = loRepDetalleVenta.Create(oDetalleVenta) != null;
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

    #region Clases

    public class VentaProductos
    {
        public int COD_PRODUCTO { get; set; }
        public int COD_PRODUCTO_EDICION { get; set; }
        public string NOMBRE { get; set; }
        public string TIPO_PRODUCTO { get; set; }
        public string EDICION { get; set; }
        public double PRECIO_UNITARIO { get; set; }
        public int CANTIDAD { get; set; }
        public double VALOR_TOTAL { get; set; }
    }

    #endregion
}
