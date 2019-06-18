using BLL.DAL;
using System;
using System.Linq;

namespace BLL
{
    public class ReservaBLL
    {
        #region Métodos Públicos

        public int ObtenerProximaReserva()
        {
            int loIdReserva = 0;

            try
            {
                using (var rep = new Repository<Reserva>())
                {
                    var loExiste = rep.FindAll().Any();

                    if (loExiste)
                    {
                        // Devuelve el mayor valor del campo ID_VENTA
                        loIdReserva = rep.FindAll().Max(p => p.ID_RESERVA);

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

        public bool AltaReserva(Reserva oReserva)
        {
            var bRes = false;

            try
            {
                using (var rep = new Repository<Reserva>())
                {
                    bRes = rep.Create(oReserva) != null;
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
