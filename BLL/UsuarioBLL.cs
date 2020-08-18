﻿using BLL.Common;
using BLL.DAL;
using BLL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class UsuarioBLL
    {
        #region Métodos Públicos

        public Usuario ObtenerUsuario(long idUsuario)
        {
            Usuario oUsuario = null;

            try
            {
                using (var rep = new Repository<Usuario>())
                {
                    oUsuario = rep.Find(p => p.ID_USUARIO == idUsuario);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oUsuario;
        }

        public List<UsuarioListado> ObtenerUsuarios(UsuarioFiltro oUsuarioFiltro)
        {
            List<UsuarioListado> lstUsuarioListado = null;
            List<Usuario> lstUsuarios = null;

            try
            {
                using (var rep = new Repository<Usuario>())
                {
                    //lstUsuarios = rep.Search(p => p.FECHA_BAJA == null && p.COD_ESTADO == 1 && !p.NOMBRE_USUARIO.ToUpper().Equals("ADMIN") && p.ID_ROL != 3);
                    lstUsuarios = rep.Search(p => p.FECHA_BAJA == null && p.COD_ESTADO == 1 && p.ID_ROL != 3);

                    lstUsuarios.Sort((x, y) => y.FECHA_ALTA.CompareTo(x.FECHA_ALTA));

                    if (lstUsuarios.Count > 0)
                    {
                        if (oUsuarioFiltro.IdUsuario == -1)
                            lstUsuarios = lstUsuarios.FindAll(p => p.ID_USUARIO == oUsuarioFiltro.IdUsuario);
                        else if (oUsuarioFiltro.IdUsuario > 0)
                            lstUsuarios = lstUsuarios.FindAll(p => p.ID_USUARIO == oUsuarioFiltro.IdUsuario);

                        if (!String.IsNullOrEmpty(oUsuarioFiltro.NombreUsuario) && lstUsuarios.Count > 0)
                            lstUsuarios = lstUsuarios.FindAll(p => p.NOMBRE_USUARIO == oUsuarioFiltro.NombreUsuario);

                        if (!String.IsNullOrEmpty(oUsuarioFiltro.Nombre) && lstUsuarios.Count > 0)
                            lstUsuarios = lstUsuarios.FindAll(p => p.NOMBRE == oUsuarioFiltro.NombreUsuario);

                        if (!String.IsNullOrEmpty(oUsuarioFiltro.Apellido) && lstUsuarios.Count > 0)
                            lstUsuarios = lstUsuarios.FindAll(p => p.APELLIDO == oUsuarioFiltro.Apellido);

                        if (oUsuarioFiltro.IdRol > 0 && lstUsuarios.Count > 0)
                            lstUsuarios = lstUsuarios.FindAll(p => p.ID_ROL == oUsuarioFiltro.IdRol);
                    }

                    UsuarioListado oUsuarioListado;
                    lstUsuarioListado = new List<UsuarioListado>();

                    if (lstUsuarios != null)
                    {
                        foreach (var loUsuario in lstUsuarios)
                        {
                            oUsuarioListado = new UsuarioListado
                            {
                                ID_USUARIO = loUsuario.ID_USUARIO,
                                NOMBRE_USUARIO = loUsuario.NOMBRE_USUARIO,
                                NOMBRE = loUsuario.NOMBRE,
                                APELLIDO = loUsuario.APELLIDO,
                                ROL = loUsuario.Rol.DESCRIPCION
                            };

                            lstUsuarioListado.Add(oUsuarioListado);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstUsuarioListado;
        }

        public Usuario ObtenerUsuario(string pUsarioHash)
        {
            Usuario oUsuario = null;

            try
            {
                using (var rep = new Repository<Usuario>())
                {
                    oUsuario = rep.Find(p => p.RECUPERAR_CONTRASENIA == pUsarioHash && p.ID_ROL == RolUsuario.Cliente);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oUsuario;
        }

        public Usuario ObtenerUsuarioKiosco(string pUsarioHash)
        {
            Usuario oUsuario = null;

            try
            {
                using (var rep = new Repository<Usuario>())
                {
                    oUsuario = rep.Find(p => p.RECUPERAR_CONTRASENIA == pUsarioHash && p.ID_ROL != RolUsuario.Cliente);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oUsuario;
        }

        public List<UsuarioListado> ObtenerUsuarios()
        {
            List<UsuarioListado> lstUsuarioListado = null;
            List<Usuario> lstUsuarios = null;

            try
            {
                using (var rep = new Repository<Usuario>())
                {
                    lstUsuarios = rep.Search(p => p.FECHA_BAJA == null && p.COD_ESTADO == 1 && !p.NOMBRE_USUARIO.ToUpper().Equals("ADMIN"));
                    lstUsuarios.Sort((x, y) => y.FECHA_ALTA.CompareTo(x.FECHA_ALTA));

                    UsuarioListado oUsuarioListado;
                    lstUsuarioListado = new List<UsuarioListado>();

                    if (lstUsuarios != null)
                    {
                        foreach (var loUsuario in lstUsuarios)
                        {
                            oUsuarioListado = new UsuarioListado
                            {
                                ID_USUARIO = loUsuario.ID_USUARIO,
                                NOMBRE_USUARIO = loUsuario.NOMBRE_USUARIO,
                                NOMBRE = loUsuario.NOMBRE,
                                APELLIDO = loUsuario.APELLIDO,
                                ROL = loUsuario.Rol.DESCRIPCION
                            };

                            lstUsuarioListado.Add(oUsuarioListado);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstUsuarioListado;
        }

        public bool AltaUsuario(Usuario oUsuario)
        {
            var bRes = false;

            try
            {
                using (var rep = new Repository<Usuario>())
                {
                    oUsuario.CONTRASENIA = BCrypt.Net.BCrypt.HashPassword(oUsuario.CONTRASENIA);

                    bRes = rep.Create(oUsuario) != null;

                    if (bRes)
                    {
                        if (oUsuario.ID_USUARIO > 0)
                            oUsuario.RECUPERAR_CONTRASENIA = BCrypt.Net.BCrypt.HashPassword(oUsuario.ID_USUARIO.ToString());
                        bRes = rep.Update(oUsuario);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public Usuario AltaUsuarioReturnUsuario(Usuario oUsuario)
        {
            Usuario bRes = null;

            try
            {
                using (var rep = new Repository<Usuario>())
                {
                    if (!string.IsNullOrEmpty(oUsuario.RECUPERAR_CONTRASENIA))
                        oUsuario.RECUPERAR_CONTRASENIA = BCrypt.Net.BCrypt.HashPassword(oUsuario.RECUPERAR_CONTRASENIA);

                    oUsuario.CONTRASENIA = BCrypt.Net.BCrypt.HashPassword(oUsuario.CONTRASENIA);

                    bRes = rep.Create(oUsuario);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public bool BajaUsuario(long idUsuario)
        {
            var bRes = false;
            try
            {
                using (var rep = new Repository<Usuario>())
                {
                    Usuario oUsuario = rep.Find(p => p.ID_USUARIO == idUsuario);
                    oUsuario.FECHA_BAJA = DateTime.Now;
                    oUsuario.COD_ESTADO = 2;
                    bRes = rep.Update(oUsuario);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public bool ModificarUsuarioCambioContrasenia(Usuario oUsuario)
        {
            var bRes = false;
            try
            {
                using (var rep = new Repository<Usuario>())
                {
                    if (!ConsultarEsIgualContraseniaCambioContrasenia(oUsuario))
                    {
                        oUsuario.CONTRASENIA = BCrypt.Net.BCrypt.HashPassword(oUsuario.CONTRASENIA);
                        bRes = rep.Update(oUsuario);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public bool ModificarUsuario(Usuario oUsuario)
        {
            var bRes = false;
            try
            {
                using (var rep = new Repository<Usuario>())
                {
                    if (!ConsultarEsIgualContrasenia(oUsuario.CONTRASENIA))
                        oUsuario.CONTRASENIA = BCrypt.Net.BCrypt.HashPassword(oUsuario.CONTRASENIA);
                    bRes = rep.Update(oUsuario);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public bool ConsultarExistenciaNombreUsuario(string pNombreUsuario)
        {
            bool bEsNuevoNombreUsuario = false;

            try
            {
                using (var rep = new Repository<Usuario>())
                {
                    var oUsuario = rep.Find(p => p.NOMBRE_USUARIO == pNombreUsuario && p.COD_ESTADO == 1 && p.FECHA_BAJA == null);
                    bEsNuevoNombreUsuario = oUsuario == null;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bEsNuevoNombreUsuario;
        }

        public bool ConsultarEsIgualContrasenia(string pContrasenia)
        {
            bool bEsIgual = false;

            try
            {
                using (var rep = new Repository<Usuario>())
                {
                    var oUsuario = rep.Find(p => p.CONTRASENIA == pContrasenia && p.COD_ESTADO == 1 && p.FECHA_BAJA == null);
                    bEsIgual = oUsuario != null;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bEsIgual;
        }

        public bool ConsultarEsIgualContraseniaCambioContrasenia(Usuario pUsuario)
        {
            bool bEsIgual = false;

            try
            {
                using (var rep = new Repository<Usuario>())
                {
                    var oUsuario = rep.Find(p => p.ID_USUARIO == pUsuario.ID_USUARIO && p.COD_ESTADO == 1 && p.FECHA_BAJA == null);

                    bEsIgual = (BCrypt.Net.BCrypt.Verify(pUsuario.CONTRASENIA, oUsuario.CONTRASENIA));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bEsIgual;
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

        public string ConsultarExistenciaUsuarioDeKiosco(string email)
        {
            string bExiste = string.Empty;

            try
            {
                using (var rep = new Repository<Usuario>())
                {
                    var oUsuario = rep.Find(p => p.EMAIL.Equals(email) && p.FECHA_BAJA == null && p.COD_ESTADO == 1 && p.ID_ROL != 3);

                    if (oUsuario != null)
                        bExiste = rep.Find(x => x.ID_USUARIO == oUsuario.ID_USUARIO).RECUPERAR_CONTRASENIA;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bExiste;
        }

        #endregion
    }

    #region Clases

    public class UsuarioListado
    {
        public int ID_USUARIO { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public string NOMBRE_USUARIO { get; set; }
        public string ROL { get; set; }
    }

    #endregion
}
