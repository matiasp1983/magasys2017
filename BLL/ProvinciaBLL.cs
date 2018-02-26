using BLL.DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class ProvinciaBLL
    {
        #region Métodos Públicos

        public Provincia ObtenerProvincia(long idProvincia)
        {
            Provincia oProvincia = null;

            try
            {
                using (var rep = new Repository<Provincia>())
                {
                    oProvincia = rep.Find(p => p.ID_PROVINCIA == idProvincia);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oProvincia;
        }

        public List<Provincia> ObtenerProvincias()
        {
            List<Provincia> lstProvincias = null;

            try
            {
                using (var rep = new Repository<Provincia>())
                {
                    lstProvincias = rep.FindAll();
                    lstProvincias.Sort((x, y) => String.Compare(x.NOMBRE, y.NOMBRE));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstProvincias;
        }

        #endregion
    }
}
