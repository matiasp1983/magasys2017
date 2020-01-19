using BLL.DAL;
using BLL.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public bool ModificarDetalleProductoIngreso(DetalleProductoIngreso oDetalleProductoIngreso)
        {
            var bRes = false;

            try
            {
                using (var rep = new Repository<DetalleProductoIngreso>())
                {
                    bRes = rep.Update(oDetalleProductoIngreso);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return bRes;
        }

        public List<ProductoIngresoListado> ObtenerProductoIngreso(IngresoProductoFiltro oIngresoProductoFiltro)
        {
            List<ProductoIngresoListado> lstProductoIngresoListado = null;
            List<ProductoIngreso> lstProductoIngreso = null;

            try
            {
                using (var rep = new Repository<ProductoIngreso>())
                {
                    if (oIngresoProductoFiltro.IdProveedor > 0)
                        lstProductoIngreso = rep.Search(p => p.COD_ESTADO == 1 && p.COD_PROVEEDOR == oIngresoProductoFiltro.IdProveedor);

                    if (lstProductoIngreso != null)
                    {
                        if (oIngresoProductoFiltro.FechaAltaDesde != null && oIngresoProductoFiltro.FechaAltaHasta != null)
                            lstProductoIngreso = lstProductoIngreso.FindAll(p => p.FECHA.Date >= oIngresoProductoFiltro.FechaAltaDesde && p.FECHA.Date <= oIngresoProductoFiltro.FechaAltaHasta);
                        else if (oIngresoProductoFiltro.FechaAltaDesde != null && oIngresoProductoFiltro.FechaAltaHasta == null)
                            lstProductoIngreso = lstProductoIngreso.FindAll(p => p.FECHA.Date >= oIngresoProductoFiltro.FechaAltaDesde);
                        else if (oIngresoProductoFiltro.FechaAltaDesde == null && oIngresoProductoFiltro.FechaAltaHasta != null)
                            lstProductoIngreso = lstProductoIngreso.FindAll(p => p.FECHA.Date <= oIngresoProductoFiltro.FechaAltaHasta);
                    }
                    else
                    {
                        if (oIngresoProductoFiltro.FechaAltaDesde != null && oIngresoProductoFiltro.FechaAltaHasta != null)
                            lstProductoIngreso = rep.Search(p => p.COD_ESTADO == 1 && DbFunctions.TruncateTime(p.FECHA) >= DbFunctions.TruncateTime(oIngresoProductoFiltro.FechaAltaDesde) && DbFunctions.TruncateTime(p.FECHA) <= DbFunctions.TruncateTime(oIngresoProductoFiltro.FechaAltaHasta));
                        else if (oIngresoProductoFiltro.FechaAltaDesde != null && oIngresoProductoFiltro.FechaAltaHasta == null)
                            lstProductoIngreso = rep.Search(p => p.COD_ESTADO == 1 && DbFunctions.TruncateTime(p.FECHA) >= DbFunctions.TruncateTime(oIngresoProductoFiltro.FechaAltaDesde));
                        else if (oIngresoProductoFiltro.FechaAltaDesde == null && oIngresoProductoFiltro.FechaAltaHasta != null)
                            lstProductoIngreso = rep.Search(p => p.COD_ESTADO == 1 && DbFunctions.TruncateTime(p.FECHA) <= DbFunctions.TruncateTime(oIngresoProductoFiltro.FechaAltaHasta));
                    }
                }

                ProductoIngresoListado oProductoIngresoListado;
                lstProductoIngresoListado = new List<ProductoIngresoListado>();

                if (lstProductoIngreso != null)
                {
                    foreach (var loProductoIngreso in lstProductoIngreso)
                    {
                        oProductoIngresoListado = new ProductoIngresoListado
                        {
                            ID_INGRESO_PRODUCTOS = loProductoIngreso.ID_INGRESO_PRODUCTOS,
                            FECHA = loProductoIngreso.FECHA,
                            COD_PROVEEDOR = loProductoIngreso.COD_PROVEEDOR,
                        };

                        using (var loRepProveedor = new Repository<Proveedor>())
                            oProductoIngresoListado.DESC_PROVEEDOR = loRepProveedor.Find(p => p.ID_PROVEEDOR == loProductoIngreso.COD_PROVEEDOR).RAZON_SOCIAL;

                        lstProductoIngresoListado.Add(oProductoIngresoListado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstProductoIngresoListado;
        }

        #endregion
    }

    #region Clases

    public class ProductoIngresoListado
    {
        public int ID_INGRESO_PRODUCTOS { get; set; }
        public DateTime FECHA { get; set; }
        public int COD_PROVEEDOR { get; set; }
        public string DESC_PROVEEDOR { get; set; }
    }

    #endregion
}
