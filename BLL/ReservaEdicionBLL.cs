using BLL.DAL;
using BLL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class ReservaEdicionBLL
    {

        #region Métodos Públicos

        public int ObtenerProximaReserva()
        {
            int loIdReserva = 0;

            try
            {
                using (var rep = new Repository<ReservaEdicion>())
                {
                    var loExiste = rep.FindAll().Any();

                    if (loExiste)
                    {
                        // Devuelve el mayor valor del campo ID_VENTA
                        loIdReserva = rep.FindAll().Max(p => p.ID_RESERVA_EDICION);

                        if (loIdReserva > 0)
                            loIdReserva = loIdReserva + 1;
                    }
                    else
                        loIdReserva = 1;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return loIdReserva;
        }

        public ReservaEdicion ObtenerReservaEdicion(long idReservaEdicion)
        {
            ReservaEdicion oReservaEdicion = null;

            try
            {
                using (var rep = new Repository<ReservaEdicion>())
                {
                    oReservaEdicion = rep.Find(p => p.ID_RESERVA_EDICION == idReservaEdicion);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oReservaEdicion;
        }

        public bool AltaReservaEdicion(ReservaEdicion oReservaEdicion)
        {
            var bRes = false;

            try
            {
                using (var rep = new Repository<ReservaEdicion>())
                {
                    bRes = rep.Create(oReservaEdicion) != null;
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
}
