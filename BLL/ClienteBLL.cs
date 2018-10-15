using System;
using BLL.DAL;

namespace BLL
{
    public class ClienteBLL
    {
        #region Métodos Públicos

        public Cliente ObtenerCliente(int tipoDocumento, int nroDocumento)
        {
            Cliente oCliente = null;

            try
            {
                using (var rep = new Repository<Cliente>())
                {
                    oCliente = rep.Find(p => p.TIPO_DOCUMENTO == tipoDocumento && p.NRO_DOCUMENTO == nroDocumento);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oCliente;
        }

        public bool ConsultarExistenciaCliente(int tipoDocumento, int nroDocumento)
        {
            bool bEsNuevoCliente = false;

            try
            {
                using (var rep = new Repository<Cliente>())
                {
                    var oCliente = rep.Find(p => p.TIPO_DOCUMENTO == tipoDocumento && p.NRO_DOCUMENTO == nroDocumento && p.FECHA_BAJA == null && p.COD_ESTADO == 1);
                    bEsNuevoCliente = oCliente == null;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bEsNuevoCliente;
        }

        public bool AltaCliente(Cliente oCliente)
        {
            var bRes = false;

            try
            {
                using (var rep = new Repository<Cliente>())
                {
                    bRes = rep.Create(oCliente) != null;
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
