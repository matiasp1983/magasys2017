using System;
using BLL.DAL;
using System.Collections.Generic;

namespace BLL
{
    public class FormaPagoBLL
    {
        #region Métodos Públicos

        public List<FormaPago> ObtenerFormasPago()
        {
            List<FormaPago> lstFormaPago = null;

            try
            {
                using (var rep = new Repository<FormaPago>())
                {
                    lstFormaPago = rep.FindAll();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstFormaPago;
        }

        #endregion
    }
}
