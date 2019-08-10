using BLL.DAL;
using System;
using System.Collections.Generic;

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

        #endregion
    }
}
