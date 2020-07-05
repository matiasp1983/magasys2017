using BLL.DAL;
using BLL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public bool ModificarEmpleado(Empleado oEmpleado)
        {
            var bRes = false;
            try
            {
                using (var rep = new Repository<Empleado>())
                {
                    bRes = rep.Update(oEmpleado);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public bool BajaEmpleado(long idEmpleado)
        {
            var bRes = false;
            try
            {
                using (var rep = new Repository<Empleado>())
                {
                    Empleado oEmpleado = rep.Find(p => p.ID_EMPLEADO == idEmpleado);
                    oEmpleado.FECHA_BAJA = DateTime.Now;
                    oEmpleado.COD_ESTADO = 2;
                    bRes = rep.Update(oEmpleado);
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

        public bool ConsultarExistenciaCuil(string cuil)
        {
            bool bEsNuevoCuil = false;

            try
            {
                using (var resEmpleado = new Repository<Empleado>())
                {
                    var oEmpleado = resEmpleado.Find(p => p.CUIL == cuil && p.FECHA_BAJA == null && p.COD_ESTADO == 1);
                    bEsNuevoCuil = oEmpleado == null;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bEsNuevoCuil;
        }

        public List<EmpleadoListado> ObtenerEmpleados(EmpleadoFiltro oClienteFiltro)
        {
            List<EmpleadoListado> lstEmpleadoListado = null;
            List<Empleado> lstEmpleado = null;

            try
            {
                using (var rep = new Repository<Empleado>())
                {
                    lstEmpleado = rep.Search(p => p.FECHA_BAJA == null && p.COD_ESTADO == 1).OrderByDescending(p => p.ID_EMPLEADO).ToList();

                    if (lstEmpleado.Count > 0)
                    {
                        if (oClienteFiltro.Tipo_documento > 0 && oClienteFiltro.Nro_documento > 0 && lstEmpleado.Count > 0)
                            lstEmpleado = lstEmpleado.FindAll(p => p.TIPO_DOCUMENTO == oClienteFiltro.Tipo_documento && p.NRO_DOCUMENTO == oClienteFiltro.Nro_documento);

                        if (!String.IsNullOrEmpty(oClienteFiltro.Apellido) && lstEmpleado.Count > 0)
                            lstEmpleado = lstEmpleado.FindAll(p => p.APELLIDO.ToUpper().Contains(oClienteFiltro.Apellido.ToUpper()));

                        if (!String.IsNullOrEmpty(oClienteFiltro.Nombre) && lstEmpleado.Count > 0)
                            lstEmpleado = lstEmpleado.FindAll(p => p.NOMBRE.ToUpper().Contains(oClienteFiltro.Nombre.ToUpper()));
                    }

                    EmpleadoListado oEmpleadoListado;
                    lstEmpleadoListado = new List<EmpleadoListado>();

                    if (lstEmpleado != null)
                    {
                        foreach (var loEmpleado in lstEmpleado)
                        {
                            oEmpleadoListado = new EmpleadoListado
                            {
                                ID_EMPLEADO = loEmpleado.ID_EMPLEADO,
                                TIPO_DOCUMENTO = loEmpleado.TipoDocumento.DESCRIPCION,
                                NRO_DOCUMENTO = loEmpleado.NRO_DOCUMENTO,
                                NOMBRE = loEmpleado.NOMBRE,
                                APELLIDO = loEmpleado.APELLIDO,
                                CUIL = loEmpleado.CUIL
                            };

                            lstEmpleadoListado.Add(oEmpleadoListado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstEmpleadoListado;
        }

        #endregion
    }

    #region Clases

    public class EmpleadoListado
    {
        public int ID_EMPLEADO { get; set; }
        public string CUIL { get; set; }
        public string TIPO_DOCUMENTO { get; set; }
        public int NRO_DOCUMENTO { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
    }

    #endregion

}