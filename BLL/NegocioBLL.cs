using System;
using BLL.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NegocioBLL
    {
        #region Métodos Públicos

        public bool AltaNegocio(Negocio oNegocio)
        {
            var bRes = false;

            try
            {
                using (var rep = new Repository<Negocio>())
                {
                    bRes = rep.Create(oNegocio) != null;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public bool ModificarNegocio(Negocio oNegocio)
        {
            var bRes = false;
            Negocio oNegocioAux = null;

            try
            {
                oNegocioAux = ObtenerNegocio();
            }
            catch (Exception)
            {
                return bRes;
            }

            try
            {
                if (oNegocioAux != null)
                {
                    using (var rep = new Repository<Negocio>())
                    {
                        oNegocio.ID_NEGOCIO = oNegocioAux.ID_NEGOCIO;
                        bRes = rep.Update(oNegocio);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return bRes;
        }

        public Negocio ObtenerNegocio()
        {
            Negocio oNegocio = null;
            List<Negocio> lstNegocio = null;

            try
            {
                using (var rep = new Repository<Negocio>())
                {
                    lstNegocio = rep.FindAll();
                    if (lstNegocio != null && lstNegocio.Count > 0)
                        oNegocio = lstNegocio.ElementAt(0);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oNegocio;
        }

        #endregion
    }
}
