using BLL.DAL;
using BLL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class ReservaBLL
    {
        #region Métodos Públicos

        public int ObtenerProximaReserva()
        {
            int loIdReserva = 0;

            try
            {
                using (var rep = new Repository<Reserva>())
                {
                    var loExiste = rep.FindAll().Any();

                    if (loExiste)
                    {
                        // Devuelve el mayor valor del campo ID_VENTA
                        loIdReserva = rep.FindAll().Max(p => p.ID_RESERVA);

                        if (loIdReserva > 0)
                            loIdReserva = loIdReserva + 1;
                    }
                    else
                        loIdReserva = 1;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return loIdReserva;
        }

        public Reserva ObtenerReserva(long idReserva)
        {
            Reserva oReserva = null;

            try
            {
                using (var rep = new Repository<Reserva>())
                {
                    oReserva = rep.Find(p => p.ID_RESERVA == idReserva);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oReserva;
        }

        public bool AltaReserva(Reserva oReserva)
        {
            var bRes = false;

            try
            {
                using (var rep = new Repository<Reserva>())
                {
                    bRes = rep.Create(oReserva) != null;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public bool ModificarReserva(Reserva oReserva)
        {
            var bRes = false;
            try
            {
                using (var rep = new Repository<Reserva>())
                {
                    bRes = rep.Update(oReserva);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public bool FinalizarReservas()
        {
            var bRes = false;
            DateTime lv_fecha = DateTime.Now.Date;

            try
            {
                using (var loRepReserva = new Repository<Reserva>())
                {   // Buscar reservas Confirmadas con Fecha Fin menor a la fecha del día.
                    var lstReserva = loRepReserva.Search(p => p.COD_ESTADO == 7 && p.FECHA_FIN < lv_fecha);

                    if (lstReserva.Count > 0)
                    {
                        foreach (var loReserva in lstReserva)
                        {
                            loReserva.COD_ESTADO = 8;
                            bRes = loRepReserva.Update(loReserva);
                            if (!bRes)
                                break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public List<TipoReserva> ObtenerTipoReserva()
        {
            List<TipoReserva> lstTipoReserva = null;

            try
            {
                using (var rep = new Repository<TipoReserva>())
                {
                    lstTipoReserva = rep.FindAll();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstTipoReserva;
        }

        public List<ReservaListado> ObtenerReservas(ReservaFiltro oReservaFiltro)
        {
            List<ReservaListado> lstReservaListado = null;
            List<Reserva> lstReserva = null;

            try
            {
                using (var loRepReserva = new Repository<Reserva>())
                {
                    lstReserva = loRepReserva.Search(p => p.COD_ESTADO == oReservaFiltro.COD_ESTADO).OrderByDescending(p => p.ID_RESERVA).ToList();

                    if (lstReserva.Count > 0)
                    {
                        if (oReservaFiltro.FECHAINICIORESERVADESDE != null && oReservaFiltro.FECHAINICIORESERVAHASTA != null)
                            lstReserva = lstReserva.FindAll(p => p.FECHA_INICIO >= oReservaFiltro.FECHAINICIORESERVADESDE && p.FECHA_INICIO <= oReservaFiltro.FECHAINICIORESERVAHASTA);
                        else if (oReservaFiltro.FECHAINICIORESERVADESDE != null && oReservaFiltro.FECHAINICIORESERVAHASTA == null)
                            lstReserva = lstReserva.FindAll(p => p.FECHA_INICIO >= oReservaFiltro.FECHAINICIORESERVADESDE);
                        else if (oReservaFiltro.FECHAINICIORESERVADESDE == null && oReservaFiltro.FECHAINICIORESERVAHASTA != null)
                            lstReserva = lstReserva.FindAll(p => p.FECHA_INICIO <= oReservaFiltro.FECHAINICIORESERVAHASTA);

                        if (oReservaFiltro.FECHAFINRESERVADESDE != null && oReservaFiltro.FECHAFINRESERVAHASTA != null)
                            lstReserva = lstReserva.FindAll(p => p.FECHA_FIN >= oReservaFiltro.FECHAFINRESERVADESDE && p.FECHA_FIN <= oReservaFiltro.FECHAFINRESERVAHASTA);
                        else if (oReservaFiltro.FECHAFINRESERVADESDE != null && oReservaFiltro.FECHAFINRESERVAHASTA == null)
                            lstReserva = lstReserva.FindAll(p => p.FECHA_FIN >= oReservaFiltro.FECHAFINRESERVADESDE);
                        else if (oReservaFiltro.FECHAFINRESERVADESDE == null && oReservaFiltro.FECHAFINRESERVAHASTA != null)
                            lstReserva = lstReserva.FindAll(p => p.FECHA_FIN <= oReservaFiltro.FECHAFINRESERVAHASTA);

                        if (lstReserva.Count > 0 && oReservaFiltro.COD_TIPO_RESERVA != 0)
                            lstReserva = lstReserva.FindAll(p => p.COD_TIPO_RESERVA == oReservaFiltro.COD_TIPO_RESERVA);

                        if (lstReserva.Count > 0 && !String.IsNullOrEmpty(oReservaFiltro.NOMBRE_PRODUCTO))
                            lstReserva = lstReserva.FindAll(p => p.Producto.NOMBRE.ToUpper().Contains(oReservaFiltro.NOMBRE_PRODUCTO.ToUpper()));

                        if (lstReserva.Count > 0 && oReservaFiltro.COD_FORMA_ENTREGA == "Retira en Local")
                            lstReserva = lstReserva.FindAll(p => p.ENVIO_DOMICILIO == null);
                        else if (lstReserva.Count > 0 && oReservaFiltro.COD_FORMA_ENTREGA == "Envío a Domicilio")
                            lstReserva = lstReserva.FindAll(p => p.ENVIO_DOMICILIO != null);

                        if (lstReserva.Count > 0 && oReservaFiltro.TIPO_DOCUMENTO != 0 && oReservaFiltro.NRO_DOCUMENTO != 0)
                            lstReserva = lstReserva.FindAll(p => p.Cliente.TIPO_DOCUMENTO == oReservaFiltro.TIPO_DOCUMENTO && p.Cliente.NRO_DOCUMENTO == oReservaFiltro.NRO_DOCUMENTO);

                        if (lstReserva.Count > 0 && !String.IsNullOrEmpty(oReservaFiltro.NOMBRE))
                            lstReserva = lstReserva.FindAll(p => p.Cliente.NOMBRE.ToUpper().Contains(oReservaFiltro.NOMBRE.ToUpper()));

                        if (lstReserva.Count > 0 && !String.IsNullOrEmpty(oReservaFiltro.APELLIDO))
                            lstReserva = lstReserva.FindAll(p => p.Cliente.APELLIDO.ToUpper().Contains(oReservaFiltro.APELLIDO.ToUpper()));

                        if (lstReserva.Count > 0 && !String.IsNullOrEmpty(oReservaFiltro.ALIAS))
                            lstReserva = lstReserva.FindAll(p => p.Cliente.ALIAS != null && p.Cliente.ALIAS.ToUpper().Contains(oReservaFiltro.ALIAS.ToUpper()));
                    }

                    ReservaListado oReservaListado;
                    lstReservaListado = new List<ReservaListado>();

                    foreach (var loReserva in lstReserva)
                    {
                        oReservaListado = new ReservaListado
                        {
                            ID_RESERVA = loReserva.ID_RESERVA,
                            NOMBRE_CLIENTE = loReserva.Cliente.APELLIDO + " " + loReserva.Cliente.NOMBRE,
                            NOMBRE_PRODUCTO = loReserva.Producto.NOMBRE,
                            COD_CLIENTE = loReserva.COD_CLIENTE,
                            TIPO_RESERVA = loReserva.TipoReserva.DESCRIPCION
                        };

                        if (loReserva.ENVIO_DOMICILIO == null)
                            oReservaListado.FORMA_ENTREGA = "Retira en Local";
                        else
                            oReservaListado.FORMA_ENTREGA = "Envío a Domicilio";

                        lstReservaListado.Add(oReservaListado);
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return lstReservaListado;
        }

        public List<ReservaListado> ObtenerReservasPorCliente(ReservaFiltro oReservaFiltro)
        {
            List<ReservaListado> lstReservaListado = null;
            List<Reserva> lstReserva = null;
            ReservaEdicion oReservaEdicion = null;


            try
            {
                using (var loRepReserva = new Repository<Reserva>())
                {
                    lstReserva = loRepReserva.Search(p => p.COD_ESTADO == oReservaFiltro.COD_ESTADO && p.COD_CLIENTE == oReservaFiltro.COD_CLIENTE).OrderByDescending(p => p.ID_RESERVA).ToList();

                    if (lstReserva.Count > 0)
                    {
                        if (lstReserva.Count > 0 && oReservaFiltro.COD_TIPO_RESERVA != 0)
                            lstReserva = lstReserva.FindAll(p => p.COD_TIPO_RESERVA == oReservaFiltro.COD_TIPO_RESERVA);

                        if (lstReserva.Count > 0 && !String.IsNullOrEmpty(oReservaFiltro.NOMBRE_PRODUCTO))
                            lstReserva = lstReserva.FindAll(p => p.Producto.NOMBRE.ToUpper().Contains(oReservaFiltro.NOMBRE_PRODUCTO.ToUpper()));

                        if (oReservaFiltro.FECHAINICIORESERVADESDE != null)
                            lstReserva = lstReserva.FindAll(p => p.FECHA_INICIO == oReservaFiltro.FECHAINICIORESERVADESDE);

                        if (oReservaFiltro.FECHAFINRESERVADESDE != null)
                            lstReserva = lstReserva.FindAll(p => p.FECHA_FIN == oReservaFiltro.FECHAFINRESERVADESDE);

                        if (lstReserva.Count > 0 && oReservaFiltro.COD_FORMA_ENTREGA == "Retira en Local")
                            lstReserva = lstReserva.FindAll(p => p.ENVIO_DOMICILIO == null);
                        else if (lstReserva.Count > 0 && oReservaFiltro.COD_FORMA_ENTREGA == "Envío a Domicilio")
                            lstReserva = lstReserva.FindAll(p => p.ENVIO_DOMICILIO != null);
                    }

                    ReservaListado oReservaListado;
                    lstReservaListado = new List<ReservaListado>();

                    foreach (var loReserva in lstReserva)
                    {
                        oReservaListado = new ReservaListado
                        {
                            ID_RESERVA = loReserva.ID_RESERVA,
                            FECHA = loReserva.FECHA,
                            TIPO_RESERVA = loReserva.TipoReserva.DESCRIPCION,
                            NOMBRE_PRODUCTO = loReserva.Producto.NOMBRE
                        };

                        if (loReserva.ReservaEdicion.Count > 0)
                            oReservaListado.ESTADO = loReserva.ReservaEdicion.SingleOrDefault().Estado.NOMBRE; // Estado de la reserva Edición
                        else
                            oReservaListado.ESTADO = loReserva.Estado.NOMBRE; // Estado de la reserva

                        if (loReserva.FECHA_INICIO != null)
                            oReservaListado.FECHA_INICIO = Convert.ToDateTime(loReserva.FECHA_INICIO);

                        if (loReserva.FECHA_FIN != null)
                            oReservaListado.FECHA_FIN = Convert.ToDateTime(loReserva.FECHA_FIN);

                        if (loReserva.ENVIO_DOMICILIO == null)
                            oReservaListado.FORMA_ENTREGA = "Retira en Local";
                        else
                            oReservaListado.FORMA_ENTREGA = "Envío a Domicilio";

                        using (var loRepReservaEdicion = new Repository<ReservaEdicion>())
                        {
                            oReservaEdicion = loRepReservaEdicion.Find(p => p.COD_RESERVA == loReserva.ID_RESERVA);

                            if (oReservaEdicion != null)
                            {
                                oReservaListado.COD_EDICION = oReservaEdicion.COD_PROD_EDICION.ToString();
                                oReservaListado.EDICION = oReservaEdicion.ProductoEdicion.EDICION;
                                if (oReservaEdicion.ProductoEdicion.NOMBRE != null)
                                    oReservaListado.NOMBRE_EDICION = oReservaEdicion.ProductoEdicion.NOMBRE;
                                if (oReservaEdicion.ProductoEdicion.DESCRIPCION != null)
                                    oReservaListado.DESC_EDICION = oReservaEdicion.ProductoEdicion.DESCRIPCION;
                                oReservaListado.PRECIO_EDICION = oReservaEdicion.ProductoEdicion.PRECIO.ToString();
                            }
                        }

                        lstReservaListado.Add(oReservaListado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstReservaListado;
        }

        public int ObtenerCantidadReservasConfirmadasMensuales(long codCliente)
        {
            int lCantidadReservas = 0;

            try
            {
                using (var loRepReserva = new Repository<Reserva>())
                {
                    lCantidadReservas = loRepReserva.Search(p => p.COD_ESTADO == 7 && p.COD_CLIENTE == codCliente && ((p.COD_TIPO_RESERVA == 1 && p.FECHA.Year == DateTime.Now.Year && p.FECHA.Month == DateTime.Now.Month) || (p.COD_TIPO_RESERVA == 2 && p.FECHA_FIN != null && p.FECHA_FIN.Value >= DateTime.Now) || (p.COD_TIPO_RESERVA == 2 && p.FECHA_FIN != null && p.FECHA_FIN.Value.Year == DateTime.Now.Year && p.FECHA_FIN.Value.Month == DateTime.Now.Month) || (p.COD_TIPO_RESERVA == 2 && p.FECHA_FIN == null))).Count;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lCantidadReservas;
        }

        #endregion
    }

    #region Clases

    public class ReservaListado
    {
        public int ID_RESERVA { get; set; }
        public DateTime FECHA { get; set; }
        public String NOMBRE_CLIENTE { get; set; }
        public string NOMBRE_PRODUCTO { get; set; }
        public int? COD_CLIENTE { get; set; }
        public string TIPO_RESERVA { get; set; }
        public string FORMA_ENTREGA { get; set; }
        public DateTime? FECHA_INICIO { get; set; }
        public DateTime? FECHA_FIN { get; set; }
        public string ESTADO { get; set; }
        public string COD_EDICION { get; set; }
        public string EDICION { get; set; }
        public string NOMBRE_EDICION { get; set; }
        public string DESC_EDICION { get; set; }
        public string PRECIO_EDICION { get; set; }
    }

    public class ReservaClienteListado
    {
        public int ID_RESERVA { get; set; }
        public string CLIENTE { get; set; }
        public string PRODUCTO { get; set; }
        public int CODPRODUCTOEDICION { get; set; }
    }

    public class ReservaCustomerWebSite
    {
        public System.Web.UI.WebControls.Image IMAGEN { get; set; }
        public string COD_PRODUCTO { get; set; }
        public int COD_TIPO_PRODUCTO { get; set; }
        public string COD_PRODUCTO_EDICION { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string FECHA_EDICION { get; set; }
        public string PRECIO { get; set; }
        public int CANTIDAD { get; set; }
        public string SUBTOTAL { get; set; }
        public bool RETIRA_LOCAL { get; set; }
        public bool ENVIO_DOMICILIO { get; set; }
        public string PRODUCTO_EDICION { get; set; } // para concatenar el producto con la edición y poder identificar unívocamente el registro para eliminar.
    }

    #endregion
}