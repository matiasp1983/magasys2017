using BLL.DAL;
using System;
using BLL.Filters;
using System.Collections.Generic;

namespace BLL
{
    public class GeneroBLL
    {
        #region Métodos Públicos

        public Genero ObtenerGenero(long idGenero)
        {
            Genero oGenero = null;

            try
            {
                using (var rep = new Repository<Genero>())
                {
                    oGenero = rep.Find(p => p.ID_GENERO == idGenero);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oGenero;
        }

        public List<Genero> ObtenerGeneros(GeneroFiltro oGeneroFiltro)
        {
            List<Genero> lstGeneros = null;

            try
            {
                using (var rep = new Repository<Genero>())
                {
                    lstGeneros = rep.FindAll();

                    if (lstGeneros.Count > 0)
                    {
                        if (oGeneroFiltro.Id_Genero == -1)
                            lstGeneros = lstGeneros.FindAll(p => p.ID_GENERO == oGeneroFiltro.Id_Genero);
                        else if (oGeneroFiltro.Id_Genero > 0)
                            lstGeneros = lstGeneros.FindAll(p => p.ID_GENERO == oGeneroFiltro.Id_Genero);

                        if (!String.IsNullOrEmpty(oGeneroFiltro.Nombre) && lstGeneros.Count > 0)
                            lstGeneros = lstGeneros.FindAll(p => p.NOMBRE.ToUpper().Contains(oGeneroFiltro.Nombre.ToUpper()));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstGeneros;
        }

        public bool AltaGenero(Genero oGenero)
        {
            var bRes = false;

            try
            {
                using (var rep = new Repository<Genero>())
                {
                    bRes = rep.Create(oGenero) != null;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public List<Genero> ObtenerGeneros()
        {
            List<Genero> lstGeneros = null;

            try
            {
                using (var rep = new Repository<Genero>())
                {
                    lstGeneros = rep.FindAll();
                    lstGeneros.Sort((x, y) => String.Compare(x.NOMBRE, y.NOMBRE));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstGeneros;
        }

        public bool ConsultarExistenciaGenero(string nombre)
        {
            bool bEsNuevoNombre = false;

            try
            {
                using (var rep = new Repository<Genero>())
                {
                    var oGenero = rep.Find(p => p.NOMBRE == nombre);
                    bEsNuevoNombre = oGenero == null;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bEsNuevoNombre;
        }

        public bool ModificarGenero(Genero oGenero)
        {
            var bRes = false;
            try
            {
                using (var rep = new Repository<Genero>())
                {
                    bRes = rep.Update(oGenero);
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