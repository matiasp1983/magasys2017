using BLL.DAL;
using BLL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class CobroBLL
    {
        #region Métodos Públicos

        public bool AltaCobro(Cobro oCobro, List<DetalleCobro> lstDetalleCobro)
        {
            var bRes = false;

            try
            {
                lstDetalleCobro.ForEach(x => oCobro.DetalleCobro.Add(x));

                using (var loRepCobro = new Repository<Cobro>())
                    bRes = loRepCobro.Create(oCobro) != null;
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public bool AnularCobro(int idCobro)
        {
            var bRes = false;
            List<BLL.DAL.DetalleCobro> lstDetalleCobro = new List<DetalleCobro>();

            try
            {
                using (var loRepCobro = new Repository<Cobro>())
                {
                    var loCobro = loRepCobro.Find(p => p.ID_COBRO == idCobro);

                    if (loCobro != null)
                    {
                        foreach (var oDetalleCobro in loCobro.DetalleCobro.ToList())
                        {
                            if (oDetalleCobro.Venta.COD_FORMA_PAGO == 1) // Venta con forma de pago CONTADO
                                oDetalleCobro.Venta.COD_ESTADO = 6; // Se cambia el estado de la venta a ANULADA
                            else
                                oDetalleCobro.Venta.COD_ESTADO = 4; // Venta con forma de pago CUENTA CORRIENTE, el estado de la venta será A CUENTA.

                            lstDetalleCobro.Add(oDetalleCobro);
                        }

                        loCobro.DetalleCobro = lstDetalleCobro;
                        loCobro.COD_ESTADO = 14; // Anulado   
                        bRes = loRepCobro.Update(loCobro);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return bRes;
        }

        public List<CobroListado> ObtenerCobros(CobroFiltro oCobroFiltro)
        {
            List<CobroListado> lstCobroListado = null;
            List<Cobro> lstCobro = null;

            try
            {
                using (var loRepCobro = new Repository<Cobro>())
                {
                    lstCobro = loRepCobro.Search(p => p.COD_ESTADO == oCobroFiltro.COD_ESTADO).OrderByDescending(p => p.ID_COBRO).ToList();

                    if (lstCobro.Count > 0)
                    {
                        if (oCobroFiltro.FECHACOBRODESDE != null && oCobroFiltro.FECHACOBROHASTA != null)
                            lstCobro = lstCobro.FindAll(p => p.FECHA.Date >= oCobroFiltro.FECHACOBRODESDE && p.FECHA.Date <= oCobroFiltro.FECHACOBROHASTA);
                        else if (oCobroFiltro.FECHACOBRODESDE != null && oCobroFiltro.FECHACOBROHASTA == null)
                            lstCobro = lstCobro.FindAll(p => p.FECHA.Date >= oCobroFiltro.FECHACOBRODESDE);
                        else if (oCobroFiltro.FECHACOBRODESDE == null && oCobroFiltro.FECHACOBROHASTA != null)
                            lstCobro = lstCobro.FindAll(p => p.FECHA.Date <= oCobroFiltro.FECHACOBROHASTA);

                        if (lstCobro.Count > 0 && oCobroFiltro.ID_COBRO != 0)
                            lstCobro = lstCobro.FindAll(p => p.ID_COBRO == oCobroFiltro.ID_COBRO);

                        if (lstCobro.Count > 0 && oCobroFiltro.TIPO_DOCUMENTO != 0 && oCobroFiltro.NRO_DOCUMENTO != 0)
                            lstCobro = lstCobro.FindAll(p => p.COD_CLIENTE != null && p.Cliente.TIPO_DOCUMENTO == oCobroFiltro.TIPO_DOCUMENTO && p.Cliente.NRO_DOCUMENTO == oCobroFiltro.NRO_DOCUMENTO);

                        if (lstCobro.Count > 0 && !String.IsNullOrEmpty(oCobroFiltro.NOMBRE))
                            lstCobro = lstCobro.FindAll(p => p.COD_CLIENTE != null && p.Cliente.NOMBRE.ToUpper().Contains(oCobroFiltro.NOMBRE.ToUpper()));

                        if (lstCobro.Count > 0 && !String.IsNullOrEmpty(oCobroFiltro.APELLIDO))
                            lstCobro = lstCobro.FindAll(p => p.COD_CLIENTE != null && p.Cliente.APELLIDO.ToUpper().Contains(oCobroFiltro.APELLIDO.ToUpper()));

                        if (lstCobro.Count > 0 && !String.IsNullOrEmpty(oCobroFiltro.ALIAS))
                            lstCobro = lstCobro.FindAll(p => p.COD_CLIENTE != null && p.Cliente.ALIAS != null && p.Cliente.ALIAS.ToUpper().Contains(oCobroFiltro.ALIAS.ToUpper()));
                    }

                    CobroListado oCobroListado;
                    lstCobroListado = new List<CobroListado>();

                    foreach (var loCobro in lstCobro)
                    {
                        oCobroListado = new CobroListado
                        {
                            ID_COBRO = loCobro.ID_COBRO,
                            FECHA = loCobro.FECHA,
                            TOTAL = "$ " + loCobro.TOTAL.ToString()
                        };

                        if (loCobro.COD_CLIENTE != null)
                            oCobroListado.NOMBRE_CLIENTE = loCobro.Cliente.APELLIDO + " " + loCobro.Cliente.NOMBRE;

                        lstCobroListado.Add(oCobroListado);
                    }
                }
            }

            catch (Exception ex)
            {
                throw;
            }

            return lstCobroListado;
        }

        #endregion
    }

    #region Clases

    public class CobroListado
    {
        public int ID_COBRO { get; set; }
        public DateTime FECHA { get; set; }
        public String NOMBRE_CLIENTE { get; set; }
        public string TOTAL { get; set; }
    }

    #endregion
}
