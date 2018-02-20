using BLL.DAL;
using BLL.Filters;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class ProveedorBLL
    {
        #region Métodos Públicos

        public Proveedor ObtenerProveedor(long idProveedor)
        {
            Proveedor oProveedor = null;

            try
            {
                using (var rep = new Repository<Proveedor>())
                {
                    oProveedor = rep.Find(p => p.ID_PROVEEDOR == idProveedor);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oProveedor;
        }

        public List<Proveedor> ObtenerProveedores(ProveedorFiltro oProveedorFiltro)
        {
            List<Proveedor> lstProveedores = null;

            try
            {
                using (var rep = new Repository<Proveedor>())
                {
                    lstProveedores = rep.Search(p => p.FECHA_BAJA == null);

                    lstProveedores.Sort((x, y) => y.FECHA_ALTA.CompareTo(x.FECHA_ALTA));

                    if (lstProveedores.Count > 0)
                    {
                        if (oProveedorFiltro.IdProveedor == -1)
                            lstProveedores = lstProveedores.FindAll(p => p.ID_PROVEEDOR == oProveedorFiltro.IdProveedor);
                        else if (oProveedorFiltro.IdProveedor > 0)
                            lstProveedores = lstProveedores.FindAll(p => p.ID_PROVEEDOR == oProveedorFiltro.IdProveedor);

                        if (!String.IsNullOrEmpty(oProveedorFiltro.Cuit))
                            lstProveedores = lstProveedores.FindAll(p => p.CUIT == oProveedorFiltro.Cuit);

                        if (oProveedorFiltro.FechaAltaDesde != null && oProveedorFiltro.FechaAltaHasta != null)
                            lstProveedores = lstProveedores.FindAll(p => p.FECHA_ALTA >= oProveedorFiltro.FechaAltaDesde && p.FECHA_ALTA <= oProveedorFiltro.FechaAltaHasta);
                        else if (oProveedorFiltro.FechaAltaDesde != null && oProveedorFiltro.FechaAltaHasta == null)
                            lstProveedores = lstProveedores.FindAll(p => p.FECHA_ALTA >= oProveedorFiltro.FechaAltaDesde && oProveedorFiltro.FechaAltaHasta == null);
                        else if (oProveedorFiltro.FechaAltaDesde == null && oProveedorFiltro.FechaAltaHasta != null)
                            lstProveedores = lstProveedores.FindAll(p => p.FECHA_ALTA <= oProveedorFiltro.FechaAltaHasta && oProveedorFiltro.FechaAltaDesde == null);

                        if (!String.IsNullOrEmpty(oProveedorFiltro.RazonSocial))
                            lstProveedores = lstProveedores.FindAll(p => p.RAZON_SOCIAL.ToUpper().Contains(oProveedorFiltro.RazonSocial.ToUpper()));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstProveedores;
        }

        public List<Proveedor> ObtenerProveedores()
        {
            List<Proveedor> lstProveedores = null;

            try
            {
                using (var rep = new Repository<Proveedor>())
                {
                    lstProveedores = rep.Search(p => p.FECHA_BAJA == null);                    
                    lstProveedores.Sort((x, y) => String.Compare(x.RAZON_SOCIAL, y.RAZON_SOCIAL));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstProveedores;
        }

        public bool AltaProveedor(Proveedor oProveedor)
        {
            var bRes = false;

            try
            {
                using (var rep = new Repository<Proveedor>())
                {
                    bRes = rep.Create(oProveedor) != null;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public bool BajaProveedor(long idProveedor)
        {
            var bRes = false;
            try
            {
                using (var rep = new Repository<Proveedor>())
                {
                    Proveedor oProveedor = rep.Find(p => p.ID_PROVEEDOR == idProveedor);
                    oProveedor.FECHA_BAJA = DateTime.Now;
                    bRes = rep.Update(oProveedor);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public bool ModificarProveedor(Proveedor oProveedor)
        {
            var bRes = false;
            try
            {
                using (var rep = new Repository<Proveedor>())
                {
                    bRes = rep.Update(oProveedor);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public bool ConsultarExistenciaCuit(string cuit)
        {
            bool bEsNuevoCuit = false;

            try
            {
                using (var res = new Repository<Proveedor>())
                {
                    var oProveedor = res.Find(p => p.CUIT == cuit && p.FECHA_BAJA == null);
                    bEsNuevoCuit = oProveedor == null;
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