using System;
using BLL.DAL;
using BLL.Filters;
using System.Collections.Generic;
using System.Linq;

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

        public List<ClienteListado> ObtenerClientes(ClienteFiltro oClienteFiltro)
        {
            List<ClienteListado> lstClienteListado = null;
            List<Cliente> lstCliente = null;

            try
            {
                using (var rep = new Repository<Cliente>())
                {
                    lstCliente = rep.Search(p => p.FECHA_BAJA == null && p.COD_ESTADO == 1).OrderByDescending(p => p.ID_CLIENTE).ToList();

                    if (lstCliente.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(oClienteFiltro.Alias))
                            lstCliente = lstCliente.FindAll(p => p.ALIAS != null && p.ALIAS.ToUpper().Contains(oClienteFiltro.Alias.ToUpper()));

                        if (oClienteFiltro.Id_cliente > 0 && lstCliente.Count > 0)
                            lstCliente = lstCliente.FindAll(p => p.ID_CLIENTE == oClienteFiltro.Id_cliente);

                        if (oClienteFiltro.Tipo_documento > 0 && oClienteFiltro.Nro_documento > 0 && lstCliente.Count > 0)
                            lstCliente = lstCliente.FindAll(p => p.TIPO_DOCUMENTO == oClienteFiltro.Tipo_documento && p.NRO_DOCUMENTO == oClienteFiltro.Nro_documento);

                        if (!String.IsNullOrEmpty(oClienteFiltro.Apellido) && lstCliente.Count > 0)
                            lstCliente = lstCliente.FindAll(p => p.APELLIDO.ToUpper().Contains(oClienteFiltro.Apellido.ToUpper()));

                        if (!String.IsNullOrEmpty(oClienteFiltro.Nombre) && lstCliente.Count > 0)
                            lstCliente = lstCliente.FindAll(p => p.NOMBRE.ToUpper().Contains(oClienteFiltro.Nombre.ToUpper()));

                    }

                    ClienteListado oClienteListado;
                    lstClienteListado = new List<ClienteListado>();

                    if (lstCliente != null)
                    {
                        foreach (var loCliente in lstCliente)
                        {
                            oClienteListado = new ClienteListado
                            {
                                ID_CLIENTE = loCliente.ID_CLIENTE,
                                TIPO_DOCUMENTO = loCliente.TipoDocumento.DESCRIPCION,
                                NRO_DOCUMENTO = loCliente.NRO_DOCUMENTO,
                                NOMBRE = loCliente.NOMBRE,
                                APELLIDO = loCliente.APELLIDO
                            };

                            lstClienteListado.Add(oClienteListado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return lstClienteListado;
        }

        public List<ClienteListadoGrilla> ObtenerClientesGrilla(ClienteFiltro oClienteFiltro)
        {
            List<ClienteListadoGrilla> lstClienteListado = null;
            List<Cliente> lstCliente = null;

            try
            {
                using (var rep = new Repository<Cliente>())
                {
                    lstCliente = rep.Search(p => p.FECHA_BAJA == null && p.COD_ESTADO == 1).OrderByDescending(p => p.ID_CLIENTE).ToList();

                    if (lstCliente.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(oClienteFiltro.Alias))
                            lstCliente = lstCliente.FindAll(p => p.ALIAS != null && p.ALIAS.ToUpper().Contains(oClienteFiltro.Alias.ToUpper()));

                        if (oClienteFiltro.Tipo_documento > 0 && oClienteFiltro.Nro_documento > 0 && lstCliente.Count > 0)
                            lstCliente = lstCliente.FindAll(p => p.TIPO_DOCUMENTO == oClienteFiltro.Tipo_documento && p.NRO_DOCUMENTO == oClienteFiltro.Nro_documento);

                        if (!String.IsNullOrEmpty(oClienteFiltro.Apellido) && lstCliente.Count > 0)
                            lstCliente = lstCliente.FindAll(p => p.APELLIDO.ToUpper().Contains(oClienteFiltro.Apellido.ToUpper()));

                        if (!String.IsNullOrEmpty(oClienteFiltro.Nombre) && lstCliente.Count > 0)
                            lstCliente = lstCliente.FindAll(p => p.NOMBRE.ToUpper().Contains(oClienteFiltro.Nombre.ToUpper()));
                    }

                    ClienteListadoGrilla oClienteListado;
                    lstClienteListado = new List<ClienteListadoGrilla>();

                    if (lstCliente != null)
                    {
                        foreach (var loCliente in lstCliente)
                        {
                            oClienteListado = new ClienteListadoGrilla
                            {
                                ID_CLIENTE = loCliente.ID_CLIENTE,
                                NOMBRE_CLIENTE = loCliente.APELLIDO + " " + loCliente.NOMBRE,
                                TIPO_DOCUMENTO = loCliente.TipoDocumento.DESCRIPCION,
                                NRO_DOCUMENTO = loCliente.NRO_DOCUMENTO,
                                ALIAS = loCliente.ALIAS
                            };

                            lstClienteListado.Add(oClienteListado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return lstClienteListado;
        }

        public bool ModificarCliente(Cliente oCliente)
        {
            var bRes = false;
            try
            {
                using (var rep = new Repository<Cliente>())
                {
                    bRes = rep.Update(oCliente);
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

    #region Clases

    public class ClienteListado
    {
        public int ID_CLIENTE { get; set; }
        public string TIPO_DOCUMENTO { get; set; }
        public int NRO_DOCUMENTO { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
    }

    public class ClienteListadoGrilla
    {
        public int ID_CLIENTE { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public string TIPO_DOCUMENTO { get; set; }
        public int NRO_DOCUMENTO { get; set; }
        public string ALIAS { get; set; }
    }

    #endregion
}
