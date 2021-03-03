using BLL;
using BLL.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace PL.AdminDashboard
{
    public partial class HojaDeRutaMaps : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var oNegocio = new NegocioBLL().ObtenerNegocio();
                if (oNegocio != null)
                    txtPutoDePartida.Text = oNegocio.DIRECCION_MAPS;
            }
        }

        protected void BtnConfirmarRuta_Click(object sender, EventArgs e)
        {
            bool loModificarReservaEdidion = false;
            List<BLL.DAL.Reparto> lstReparto = new List<BLL.DAL.Reparto>();
            var lstReservaEdicionReparto = (List<ReservaEdicionReparto>)(Session[Enums.Session.ReservasHojaDeRuta.ToString()]);
            List<RepartoListado> lstRepartoListado = (List<RepartoListado>)Session[Enums.Session.HojaDeRuta.ToString()];

            foreach (var item in lstReservaEdicionReparto)
            {
                var loReservaEdicion = new ReservaEdicionBLL().ObtenerReservaEdicion(item.ID_RESERVA_EDICION);
                loReservaEdicion.COD_ESTADO = 17; // Estado: En reparto
                loModificarReservaEdidion = new ReservaEdicionBLL().ModificarReservaEdidion(loReservaEdicion);
                if (!loModificarReservaEdidion)
                    break;
            }

            if (loModificarReservaEdidion)
            {
                foreach (var itemHojaDeRuta in lstRepartoListado)
                {
                    foreach (var item in lstReservaEdicionReparto.Where(p => p.CODIGO_CLIENTE == itemHojaDeRuta.CODIGO_CLIENTE))
                    {
                        BLL.DAL.Reparto oReparto = new BLL.DAL.Reparto();

                        var loExiste = lstReparto.Exists(p => p.COD_CLIENTE == Convert.ToInt32(item.CODIGO_CLIENTE) && p.COD_EDICION == Convert.ToInt32(item.CODIGO_EDICION));

                        if (loExiste)
                        {
                            var loIndex = lstReparto.FindIndex(p => p.COD_CLIENTE == Convert.ToInt32(item.CODIGO_CLIENTE) && p.COD_EDICION == Convert.ToInt32(item.CODIGO_EDICION));
                            var loReserva = lstReparto.Find(p => p.COD_CLIENTE == Convert.ToInt32(item.CODIGO_CLIENTE) && p.COD_EDICION == Convert.ToInt32(item.CODIGO_EDICION));
                            loReserva.CANTIDAD = loReserva.CANTIDAD + 1;
                            lstReparto[loIndex] = loReserva;
                        }
                        else
                        {
                            oReparto.COD_CLIENTE = Convert.ToInt32(item.CODIGO_CLIENTE);
                            oReparto.COD_EDICION = Convert.ToInt32(item.CODIGO_EDICION);
                            oReparto.CLIENTE = item.CLIENTE;
                            oReparto.ORIGEN = itemHojaDeRuta.DIRECCION_ORIGEN;
                            oReparto.DESTINO = itemHojaDeRuta.DIRECCION_DESTINO;
                            oReparto.EDICION = item.EDICION;
                            oReparto.PRODUCTO = item.PRODUCTO;
                            oReparto.CANTIDAD = 1;
                            lstReparto.Add(oReparto);
                        }
                    }
                }
                //Eliminar Reparto:
                var oEliminarReparto = new RepartoBLL().BorrarReparto();
                foreach (var item in lstReparto)
                {
                    loModificarReservaEdidion = new RepartoBLL().AltaReparto(item);
                    if (!loModificarReservaEdidion) break;
                }
            }

            if (loModificarReservaEdidion)
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeRepartoConfirmacion, "Hoja de ruta"));
            else
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeHojaRutaFailure));
        }

        protected void BtnExportarExcel_Click(object sender, EventArgs e)
        {
            ExportarExcel();
        }

        #endregion

        #region Métodos Privados

        private void ExportarExcel()
        {
            List<ReservaEdicionReparto> lstReservaEdicionReparto = new List<ReservaEdicionReparto>();
            List<ReservaEdicionReparto> lstReservaEdicionRepartoAux = new List<ReservaEdicionReparto>();
            List<RepartoExcel> lstRepartoExcel = new List<RepartoExcel>();
            string loNombreArchivo = "attachment;filename=Reporte_Reparto_" + DateTime.Now.Date.Day + "." + DateTime.Now.Date.Month + "." + DateTime.Now.Date.Year + ".xls";

            if (Session[Enums.Session.ReservasHojaDeRuta.ToString()] != null)
            {
                lstReservaEdicionReparto = (List<ReservaEdicionReparto>)Session[Enums.Session.ReservasHojaDeRuta.ToString()];
                lstReservaEdicionReparto.OrderBy(p => p.CODIGO_CLIENTE).ThenBy(x => x.CODIGO_EDICION);
                List<RepartoListado> lstRepartoListado = (List<RepartoListado>)Session[Enums.Session.HojaDeRuta.ToString()];

                foreach (var itemHojaDeRuta in lstRepartoListado)
                {
                    foreach (var item in lstReservaEdicionReparto.Where(p => p.CODIGO_CLIENTE == itemHojaDeRuta.CODIGO_CLIENTE))
                    {
                        ReservaEdicionReparto oReservaEdicionReparto = new ReservaEdicionReparto();

                        var loExiste = lstReservaEdicionRepartoAux.Exists(p => p.CODIGO_CLIENTE == item.CODIGO_CLIENTE && p.CODIGO_EDICION == item.CODIGO_EDICION);

                        if (loExiste)
                        {
                            var loIndex = lstReservaEdicionRepartoAux.FindIndex(p => p.CODIGO_CLIENTE == item.CODIGO_CLIENTE && p.CODIGO_EDICION == item.CODIGO_EDICION);
                            var loReserva = lstReservaEdicionRepartoAux.Find(p => p.CODIGO_CLIENTE == item.CODIGO_CLIENTE && p.CODIGO_EDICION == item.CODIGO_EDICION);
                            loReserva.CANTIDAD = loReserva.CANTIDAD + 1;
                            lstReservaEdicionRepartoAux[loIndex] = loReserva;
                        }
                        else
                        {
                            oReservaEdicionReparto = item;
                            oReservaEdicionReparto.DIRECCION_MAPS_ORIGEN = itemHojaDeRuta.DIRECCION_ORIGEN;
                            oReservaEdicionReparto.CANTIDAD = 1;
                            lstReservaEdicionRepartoAux.Add(oReservaEdicionReparto); // Listado para Exportar a Excel
                        }
                    }
                }

                #region [IMPORTAR A EXCEL]

                if (lstReservaEdicionRepartoAux.Count > 0)
                {
                    foreach (var item in lstReservaEdicionRepartoAux)
                    {
                        RepartoExcel oRepartoExcel = new RepartoExcel()
                        {
                            CLIENTE = item.CLIENTE,
                            ORIGEN = item.DIRECCION_MAPS_ORIGEN,
                            DESTINO = item.DIRECCION_MAPS,
                            EDICION = item.EDICION,
                            PRODUCTO = item.PRODUCTO,
                            CANTIDAD = item.CANTIDAD
                        };

                        lstRepartoExcel.Add(oRepartoExcel);
                    }

                    Response.ClearContent();
                    Response.AddHeader("content-disposition", loNombreArchivo);
                    Response.AddHeader("Content-Type", "application/vnd.ms-excel");
                    Response.ContentEncoding = System.Text.Encoding.Unicode;
                    Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                    GenericListOutput.WriteTsv(lstRepartoExcel, Response.Output);
                    Response.End();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeExportarHoraDeRura, "Exportar Hoja de ruta"));
                }

                #endregion
            }
        }

        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reparto.aspx", false);
        }

        #endregion

        [WebMethod]
        public static List<object> EstrategiaFuerzaBruta(string pModoTransporte)
        {
            string loIcono = "";
            List<object> loLatLng = new List<object>();

            try
            {
                if (HttpContext.Current.Session[Enums.Session.ClientesHojaDeRuta.ToString()] != null && HttpContext.Current.Session[Enums.Session.ReservasHojaDeRuta.ToString()] != null && HttpContext.Current.Session[Enums.Session.RegresarAlKiosco.ToString()] != null)
                {
                    if (((List<BLL.DAL.Cliente>)(HttpContext.Current.Session[Enums.Session.ClientesHojaDeRuta.ToString()])).Count > 0)
                    {
                        List<RepartoListado> lstRepartoListado = new List<RepartoListado>();
                        BLL.FuerzaBruta.EstrategiaFuerzaBruta oEstrategiaFuerzaBruta = new BLL.FuerzaBruta.EstrategiaFuerzaBruta(pModoTransporte, (bool)HttpContext.Current.Session[Enums.Session.RegresarAlKiosco.ToString()]);

                        switch (pModoTransporte)
                        {
                            case "Driving":
                                loIcono = new HtmlGenericControl().InnerHtml.Insert(0, "<i class='fas fa-car'></i>");
                                break;
                            case "Walking":
                                loIcono = new HtmlGenericControl().InnerHtml.Insert(0, "<i class='fas fa-walking'></i>");
                                break;
                            case "Bicycling":
                                loIcono = new HtmlGenericControl().InnerHtml.Insert(0, "<i class='fas fa-biking'></i>");
                                break;
                        }

                        var loFilaMatrizFB = oEstrategiaFuerzaBruta.Ejecutar((List<BLL.DAL.Cliente>)(HttpContext.Current.Session[Enums.Session.ClientesHojaDeRuta.ToString()]));

                        double loDistanciaTotal = Convert.ToDouble(loFilaMatrizFB.COSTO_TOTAL_DISTANCIA.ToString()) / 1000;
                        int lowaypoint = 0;
                        double? loLatDestino = 0;
                        double? loLongDestino = 0;

                        foreach (var item in loFilaMatrizFB.NODOS_DISTANCIA)
                        {
                            if (lowaypoint + 1 != loFilaMatrizFB.NODOS_DISTANCIA.Count || !(bool)HttpContext.Current.Session[Enums.Session.RegresarAlKiosco.ToString()])
                            {
                                RepartoListado oRepartoListado = new RepartoListado
                                {
                                    ICONO = loIcono,
                                    DIRECCION_ORIGEN = item.ORIGEN.CLIENTE.DIRECCION_MAPS,
                                    DIRECCION_DESTINO = item.DESTINO.CLIENTE.DIRECCION_MAPS,
                                    CLIENTE = item.DESTINO.CLIENTE.NOMBRE + " " + item.DESTINO.CLIENTE.APELLIDO,
                                    TELEFONO_MOVIL = item.DESTINO.CLIENTE.TELEFONO_MOVIL,
                                    NRO_DOCUMENTO = item.DESTINO.CLIENTE.NRO_DOCUMENTO.ToString(),
                                    TIPO_DOCUMENTO = new TipoDocumentoBLL().ObtenerTipoDocumento(item.DESTINO.CLIENTE.TIPO_DOCUMENTO).DESCRIPCION,
                                    CODIGO_CLIENTE = item.DESTINO.CLIENTE.ID_CLIENTE.ToString()
                                };
                                lstRepartoListado.Add(oRepartoListado);
                            }

                            loLatLng.Add(new object[2]);
                            ((object[])loLatLng[lowaypoint])[0] = item.ORIGEN.CLIENTE.LATITUD; // latitud
                            ((object[])loLatLng[lowaypoint])[1] = item.ORIGEN.CLIENTE.LONGITUD; // longitud

                            if (lowaypoint == 0 && (bool)HttpContext.Current.Session[Enums.Session.RegresarAlKiosco.ToString()]) // Cuando es True debe regresar al kiosco, se guardar la latitud y la longitud de la dire del Kiosco
                            {
                                loLatDestino = item.ORIGEN.CLIENTE.LATITUD; // latitud
                                loLongDestino = item.ORIGEN.CLIENTE.LONGITUD; // longitud
                            }

                            if (lowaypoint + 1 == loFilaMatrizFB.NODOS_DISTANCIA.Count && !(bool)HttpContext.Current.Session[Enums.Session.RegresarAlKiosco.ToString()])
                            {
                                lowaypoint++;
                                loLatLng.Add(new object[2]);
                                ((object[])loLatLng[lowaypoint])[0] = item.DESTINO.CLIENTE.LATITUD; // latitud
                                ((object[])loLatLng[lowaypoint])[1] = item.DESTINO.CLIENTE.LONGITUD; // longitudkd
                            }

                            lowaypoint++;
                        }

                        if ((bool)HttpContext.Current.Session[Enums.Session.RegresarAlKiosco.ToString()]) // Cuando es True debe regresar al kiosco
                        {
                            loLatLng.Add(new object[2]);
                            ((object[])loLatLng[lowaypoint])[0] = loLatDestino; // latitud
                            ((object[])loLatLng[lowaypoint])[1] = loLongDestino; // longitud
                            lowaypoint++;
                        }

                        loLatLng.Add(new object[2]);
                        ((object[])loLatLng[lowaypoint])[0] = "ModoTransporte";
                        ((object[])loLatLng[lowaypoint])[1] = pModoTransporte;

                        lowaypoint++;
                        loLatLng.Add(new object[2]);
                        ((object[])loLatLng[lowaypoint])[0] = "DistanciaTotal";
                        ((object[])loLatLng[lowaypoint])[1] = string.Format(System.Globalization.CultureInfo.GetCultureInfo("de-DE"), "{0:0.00} km", loDistanciaTotal);

                        HttpContext.Current.Session.Remove(Enums.Session.HojaDeRuta.ToString());
                        HttpContext.Current.Session.Add(Enums.Session.HojaDeRuta.ToString(), lstRepartoListado);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            return loLatLng;
        }
    }
}