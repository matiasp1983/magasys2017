using BLL.DAL;
using System;

namespace BLL
{
    public class EmpleadoBLL
    {
        #region Métodos Públicos

        public bool AltaEmpleado(Empleado oEmpleado)
        {
            var bRes = false;

            try
            {
                using (var repEmpleado = new Repository<Empleado>())
                {
                    bRes = repEmpleado.Create(oEmpleado) != null;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public bool ConsultarExistenciaEmpleado(int tipoDocumento, int nroDocumento)
        {
            bool bEsNuevoEmpleado = false;

            try
            {
                using (var repEmpleado = new Repository<Empleado>())
                {
                    var oEmpleado = repEmpleado.Find(p => p.TIPO_DOCUMENTO == tipoDocumento && p.NRO_DOCUMENTO == nroDocumento && p.FECHA_BAJA == null && p.COD_ESTADO == 1);
                    bEsNuevoEmpleado = oEmpleado == null;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bEsNuevoEmpleado;
        }

        public bool ConsultarExistenciaCuit(string cuit)
        {
            bool bEsNuevoCuit = false;

            try
            {
                using (var resEmpleado = new Repository<Empleado>())
                {
                    var oEmpleado = resEmpleado.Find(p => p.CUIT == cuit && p.FECHA_BAJA == null && p.COD_ESTADO == 1);
                    bEsNuevoCuit = oEmpleado == null;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bEsNuevoCuit;
        }

        #endregion
    }
}
