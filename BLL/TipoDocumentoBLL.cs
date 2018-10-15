using System;
using BLL.DAL;
using System.Collections.Generic;

namespace BLL
{
    public class TipoDocumentoBLL
    {
        #region Métodos Públicos

        public List<TipoDocumento> ObtenerTiposDocumento()
        {
            List<TipoDocumento> lstTipoDocumento = null;

            try
            {
                using (var rep = new Repository<TipoDocumento>())
                {
                    lstTipoDocumento = rep.FindAll();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstTipoDocumento;
        }

        #endregion
    }
}
