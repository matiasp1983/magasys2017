﻿using BLL.DAL;
using BLL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class UsuarioBLL
    {
        #region Métodos Públicos

        //public Proveedor ObtenerProveedor(long idProveedor)
        //{
        //    Proveedor oProveedor = null;

        //    try
        //    {
        //        using (var rep = new Repository<Proveedor>())
        //        {
        //            oProveedor = rep.Find(p => p.ID_PROVEEDOR == idProveedor);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return oProveedor;
        //}

        //public List<Proveedor> ObtenerProveedores(ProveedorFiltro oProveedorFiltro)
        //{
        //    List<Proveedor> lstProveedores = null;

        //    try
        //    {
        //        using (var rep = new Repository<Proveedor>())
        //        {
        //            lstProveedores = rep.Search(p => p.FECHA_BAJA == null && p.COD_ESTADO == 1);

        //            lstProveedores.Sort((x, y) => y.FECHA_ALTA.CompareTo(x.FECHA_ALTA));

        //            if (lstProveedores.Count > 0)
        //            {
        //                if (oProveedorFiltro.IdProveedor == -1)
        //                    lstProveedores = lstProveedores.FindAll(p => p.ID_PROVEEDOR == oProveedorFiltro.IdProveedor);
        //                else if (oProveedorFiltro.IdProveedor > 0)
        //                    lstProveedores = lstProveedores.FindAll(p => p.ID_PROVEEDOR == oProveedorFiltro.IdProveedor);

        //                if (!String.IsNullOrEmpty(oProveedorFiltro.Cuit) && lstProveedores.Count > 0)
        //                    lstProveedores = lstProveedores.FindAll(p => p.CUIT == oProveedorFiltro.Cuit);

        //                if (oProveedorFiltro.FechaAltaDesde != null && oProveedorFiltro.FechaAltaHasta != null && lstProveedores.Count > 0)
        //                    lstProveedores = lstProveedores.FindAll(p => p.FECHA_ALTA.Date >= oProveedorFiltro.FechaAltaDesde && p.FECHA_ALTA.Date <= oProveedorFiltro.FechaAltaHasta);
        //                else if (oProveedorFiltro.FechaAltaDesde != null && oProveedorFiltro.FechaAltaHasta == null && lstProveedores.Count > 0)
        //                    lstProveedores = lstProveedores.FindAll(p => p.FECHA_ALTA.Date >= oProveedorFiltro.FechaAltaDesde && oProveedorFiltro.FechaAltaHasta == null);
        //                else if (oProveedorFiltro.FechaAltaDesde == null && oProveedorFiltro.FechaAltaHasta != null && lstProveedores.Count > 0)
        //                    lstProveedores = lstProveedores.FindAll(p => p.FECHA_ALTA.Date <= oProveedorFiltro.FechaAltaHasta && oProveedorFiltro.FechaAltaDesde == null);

        //                if (!String.IsNullOrEmpty(oProveedorFiltro.RazonSocial) && lstProveedores.Count > 0)
        //                    lstProveedores = lstProveedores.FindAll(p => p.RAZON_SOCIAL.ToUpper().Contains(oProveedorFiltro.RazonSocial.ToUpper()));
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return lstProveedores;
        //}

        //public List<Proveedor> ObtenerProveedores()
        //{
        //    List<Proveedor> lstProveedores = null;

        //    try
        //    {
        //        using (var rep = new Repository<Proveedor>())
        //        {
        //            lstProveedores = rep.Search(p => p.FECHA_BAJA == null && p.COD_ESTADO == 1);
        //            lstProveedores.Sort((x, y) => String.Compare(x.RAZON_SOCIAL, y.RAZON_SOCIAL));
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return lstProveedores;
        //}

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

        //public bool BajaProveedor(long idProveedor)
        //{
        //    var bRes = false;
        //    try
        //    {
        //        using (var rep = new Repository<Proveedor>())
        //        {
        //            Proveedor oProveedor = rep.Find(p => p.ID_PROVEEDOR == idProveedor);
        //            oProveedor.FECHA_BAJA = DateTime.Now;
        //            oProveedor.COD_ESTADO = 2;
        //            bRes = rep.Update(oProveedor);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return bRes;
        //}

        //public bool ModificarProveedor(Proveedor oProveedor)
        //{
        //    var bRes = false;
        //    try
        //    {
        //        using (var rep = new Repository<Proveedor>())
        //        {
        //            bRes = rep.Update(oProveedor);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return bRes;
        //}
        
        #endregion
    }
}
