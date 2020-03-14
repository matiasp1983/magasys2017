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
                    oUsuario.CONTRASENIA = Eramake.eCryptography.Decrypt(oUsuario.CONTRASENIA);
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
                    lstUsuarios = rep.Search(p => p.FECHA_BAJA == null && p.COD_ESTADO == 1 && !p.NOMBRE_USUARIO.ToUpper().Equals("ADMIN"));

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
                    oUsuario.CONTRASENIA = Eramake.eCryptography.Encrypt(oUsuario.CONTRASENIA);
                    bRes = rep.Create(oUsuario) != null;
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

        public bool ModificarUsuario(Usuario oUsuario)
        {
            var bRes = false;
            try
            {
                using (var rep = new Repository<Usuario>())
                {
                    oUsuario.CONTRASENIA = Eramake.eCryptography.Encrypt(oUsuario.CONTRASENIA);
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
                using (var res = new Repository<Usuario>())
                {
                    var oUsuario = res.Find(p => p.NOMBRE_USUARIO == pNombreUsuario && p.COD_ESTADO == 1 && p.FECHA_BAJA == null);
                    bEsNuevoNombreUsuario = oUsuario == null;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bEsNuevoNombreUsuario;
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
