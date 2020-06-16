using BLL;
using BLL.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace PL.AdminDashboard
{
    public partial class HojaDeRuta : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var oNegocio = new NegocioBLL().ObtenerNegocio();
                if (oNegocio != null)
                    txtPutoDePartida.Text = oNegocio.DIRECCION_MAPS;
                EstrategiaFuerzaBruta("Driving");
            }
        }

        protected void btnDriving_ServerClick(object sender, EventArgs e)
        {
            if (!btnDriving.Attributes["class"].Contains("active"))
            {
                btnDriving.Attributes.Add("class", "btn btn-outline btn-success dim active");
                btnWalking.Attributes.Add("class", "btn btn-outline btn-success dim");
                btnBiking.Attributes.Add("class", "btn btn-outline btn-success dim");
                EstrategiaFuerzaBruta("Driving");
            }
        }

        protected void btnWalking_ServerClick(object sender, EventArgs e)
        {
            if (!btnWalking.Attributes["class"].Contains("active"))
            {
                btnWalking.Attributes.Add("class", "btn btn-outline btn-success dim active");
                btnDriving.Attributes.Add("class", "btn btn-outline btn-success dim");
                btnBiking.Attributes.Add("class", "btn btn-outline btn-success dim");
                EstrategiaFuerzaBruta("Walking");
            }
        }

        protected void btnBiking_ServerClick(object sender, EventArgs e)
        {
            if (!btnBiking.Attributes["class"].Contains("active"))
            {
                btnBiking.Attributes.Add("class", "btn btn-outline btn-success dim active");
                btnDriving.Attributes.Add("class", "btn btn-outline btn-success dim");
                btnWalking.Attributes.Add("class", "btn btn-outline btn-success dim");
                EstrategiaFuerzaBruta("Bicycling");
            }
        }

        protected void BtnConfirmarRuta_Click(object sender, EventArgs e)
        {
            bool loModificarReservaEdidion = false;
            var lstReservaEdicionReparto = (List<ReservaEdicionReparto>)(Session[Enums.Session.ReservasHojaDeRuta.ToString()]);

            foreach (var item in lstReservaEdicionReparto)
            {
                var loReservaEdicion = new ReservaEdicionBLL().ObtenerReservaEdicion(item.ID_RESERVA_EDICION);
                loReservaEdicion.COD_ESTADO = 17; // Estado: En reparto
                loModificarReservaEdidion = new ReservaEdicionBLL().ModificarReservaEdidion(loReservaEdicion);
                if (!loModificarReservaEdidion)
                    break;
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

        private void EstrategiaFuerzaBruta(string modoTransporte)
        {
            string loIcono = "";

            try
            {
                if (Session[Enums.Session.ClientesHojaDeRuta.ToString()] != null && Session[Enums.Session.ReservasHojaDeRuta.ToString()] != null)
                {
                    List<RepartoListado> lstRepartoListado = new List<RepartoListado>();

                    if (((List<BLL.DAL.Cliente>)(Session[Enums.Session.ClientesHojaDeRuta.ToString()])).Count > 0)
                    {
                        BLL.FuerzaBruta.EstrategiaFuerzaBruta oEstrategiaFuerzaBruta = new BLL.FuerzaBruta.EstrategiaFuerzaBruta(modoTransporte);

                        switch (modoTransporte)
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

                        var loFilaMatrizFB = oEstrategiaFuerzaBruta.Ejecutar((List<BLL.DAL.Cliente>)(Session[Enums.Session.ClientesHojaDeRuta.ToString()]));

                        loFilaMatrizFB.NODOS_DISTANCIA.Remove(loFilaMatrizFB.NODOS_DISTANCIA.Last());

                        double loDistanciaTotal = Convert.ToDouble(loFilaMatrizFB.COSTO_TOTAL_DISTANCIA.ToString()) / 1000;
                        txtDistanciaTotal.Text = string.Format(System.Globalization.CultureInfo.GetCultureInfo("de-DE"), "{0:0.00} km", loDistanciaTotal);

                        foreach (var item in loFilaMatrizFB.NODOS_DISTANCIA)
                        {
                            RepartoListado oRepartoListado = new RepartoListado
                            {
                                ICONO = loIcono,
                                DIRECCION_ORIGEN = item.ORIGEN.CLIENTE.DIRECCION_MAPS,
                                DIRECCION_DESTINO = item.DESTINO.CLIENTE.DIRECCION_MAPS,
                                CLIENTE = item.DESTINO.CLIENTE.NOMBRE + " " + item.DESTINO.CLIENTE.APELLIDO,
                                TELEFONO_MOVIL = item.DESTINO.CLIENTE.TELEFONO_MOVIL,
                                NRO_DOCUMENTO = item.DESTINO.CLIENTE.NRO_DOCUMENTO.ToString(),
                                TIPO_DOCUMENTO = new BLL.TipoDocumentoBLL().ObtenerTipoDocumento(item.DESTINO.CLIENTE.TIPO_DOCUMENTO).DESCRIPCION,
                                CODIGO_CLIENTE = item.DESTINO.CLIENTE.ID_CLIENTE.ToString()
                            };
                            lstRepartoListado.Add(oRepartoListado);
                        }

                        lstRepartoListado[0].ICONO = new HtmlGenericControl().InnerHtml.Insert(0, "<i class='fas fa-home'></i>");
                        lstRepartoListado[lstRepartoListado.Count - 1].ICONO = new HtmlGenericControl().InnerHtml.Insert(0, "<i class='fas fa-home'></i>");
                    }

                    lsvReparto.DataSource = lstRepartoListado;
                }
            }
            catch (Exception ex)
            {
                lsvReparto.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            lsvReparto.DataBind();
        }

        private void ExportarExcel()
        {
            List<ReservaEdicionReparto> lstReservaEdicionReparto = new List<ReservaEdicionReparto>();
            List<ReservaEdicionReparto> lstReservaEdicionRepartoAux = new List<ReservaEdicionReparto>();

            if (Session[Enums.Session.ReservasHojaDeRuta.ToString()] != null)
            {
                lstReservaEdicionReparto = (List<ReservaEdicionReparto>)Session[Enums.Session.ReservasHojaDeRuta.ToString()];
                lstReservaEdicionReparto.OrderBy(p => p.CODIGO_CLIENTE).ThenBy(x => x.CODIGO_EDICION);
                List<RepartoListado> lstRepartoListado = (List<RepartoListado>)lsvReparto.DataSource;

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
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", "attachment;filename=Reporte_Usuarios.xls");
                    Response.AddHeader("Content-Type", "application/vnd.ms-excel");
                    Response.ContentEncoding = System.Text.Encoding.Unicode;
                    Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                    GenericListOutput.WriteTsv(lstReservaEdicionRepartoAux, Response.Output);
                    Response.End();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeExportarHoraDeRura, "Exportar Hoja de ruta"));
                }

                #endregion
            }
        }

        #endregion
    }

    #region Clases

    public class RepartoListado
    {
        public string ICONO { get; set; }
        public string DIRECCION_ORIGEN { get; set; }
        public string DIRECCION_DESTINO { get; set; }
        public string CODIGO_CLIENTE { get; set; }
        public string CLIENTE { get; set; }
        public string TIPO_DOCUMENTO { get; set; }
        public string NRO_DOCUMENTO { get; set; }
        public string TELEFONO_MOVIL { get; set; }
    }

    #endregion
}