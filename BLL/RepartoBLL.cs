using BLL.DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class RepartoBLL
    {
        #region Métodos Públicos

        public bool AltaReparto(Reparto oReparto)
        {
            var bRes = false;

            try
            {
                using (var rep = new Repository<Reparto>())
                {
                    bRes = rep.Create(oReparto) != null;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public bool BorrarReparto()
        {
            List<Reparto> lstReparto = new List<Reparto>();
            var bRes = false;

            try
            {
                using (var rep = new Repository<Reparto>())
                {
                    lstReparto = rep.FindAll();
                }

                foreach (var item in lstReparto)
                {
                    using (var rep = new Repository<Reparto>())
                    {
                        bRes = rep.Delete(item);
                    }
                    if (!bRes) break;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public List<Reparto> ObtenerReparto()
        {
            List<Reparto> lstReparto = null;

            try
            {
                using (var rep = new Repository<Reparto>())
                {
                    lstReparto = rep.FindAll();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstReparto;
        }

        #endregion
    }
}
