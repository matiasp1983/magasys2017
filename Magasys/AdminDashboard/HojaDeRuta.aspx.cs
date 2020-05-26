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
                var oNegocio = new BLL.NegocioBLL().ObtenerNegocio();
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
                                TIPO_DOCUMENTO = new BLL.TipoDocumentoBLL().ObtenerTipoDocumento(item.DESTINO.CLIENTE.TIPO_DOCUMENTO).DESCRIPCION
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

        #endregion
    }

    #region Clases

    public class RepartoListado
    {
        public string ICONO { get; set; }
        public string DIRECCION_ORIGEN { get; set; }
        public string DIRECCION_DESTINO { get; set; }
        public string CLIENTE { get; set; }
        public string TIPO_DOCUMENTO { get; set; }
        public string NRO_DOCUMENTO { get; set; }
        public string TELEFONO_MOVIL { get; set; }
    }

    #endregion
}