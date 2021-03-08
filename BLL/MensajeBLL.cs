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

        #endregion
    }
}
