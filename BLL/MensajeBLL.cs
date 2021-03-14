using System;
using BLL.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MensajeBLL
    {
        #region Métodos Públicos

        public Mensaje ObtenerMensaje(long idMensaje)
        {
            Mensaje oMensaje = null;

            using (var loRepMensaje = new Repository<Mensaje>())
            {
                oMensaje = loRepMensaje.Find(p => p.ID_MENSAJE == idMensaje);
            }

            return oMensaje;
        }

        /// <summary>
        /// Obtener las notificaciones que no fueron vistas por el cliente.
        /// </summary>
        /// <param name="codCliente"></param>
        /// <returns></returns>
        public List<Mensaje> ObtenerMensajes(long codCliente)
        {
            List<Mensaje> lstMensaje = null;

            using (var loRepMensaje = new Repository<Mensaje>())
            {
                lstMensaje = loRepMensaje.Search(p => p.COD_CLIENTE == codCliente && p.MENSAJE_VISTO == null);
            }

            return lstMensaje;
        }

        public bool AltaMensaje(Mensaje oMensaje)
        {
            var bRes = false;

            try
            {
                using (var rep = new Repository<Mensaje>())
                {
                    bRes = rep.Create(oMensaje) != null;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public bool ModificarMensaje(Mensaje oMensaje)
        {
            var bRes = false;
            try
            {
                using (var rep = new Repository<Mensaje>())
                {
                    bRes = rep.Update(oMensaje);
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
