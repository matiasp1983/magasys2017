using BLL.DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class LocalidadBLL
    {
        #region Métodos Públicos

        public Localidad ObtenerLocalidad(long idLocalidad)
        {
            Localidad oLocalidad = null;
            try
            {
                using (var rep = new Repository<Localidad>())
                {
                    oLocalidad = rep.Find(l => l.ID_LOCALIDAD == idLocalidad);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oLocalidad;
        }

        public List<Localidad> ObtenerLocalidades(long idProvincia)
        {
            List<Localidad> lstLocalidades = null;
            try
            {
                using (var rep = new Repository<Localidad>())
                {
                    lstLocalidades = rep.Search(l => l.ID_PROVINCIA == idProvincia);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstLocalidades;
        }

        #endregion
    }
}
