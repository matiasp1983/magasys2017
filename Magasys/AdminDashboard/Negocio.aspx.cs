using BLL.Common;
using NLog;
using System;
using System.Web.UI;

namespace PL.AdminDashboard
{
    public partial class Negocio : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                ObtenerNegocio();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            BLL.DAL.Negocio oNegocio = new BLL.DAL.Negocio();

            try
            {
                if (!String.IsNullOrEmpty(hdIdDireccionMaps.Value))
                {
                    oNegocio.DIRECCION_MAPS = hdIdDireccionMaps.Value;
                    oNegocio.LATITUD = Convert.ToDouble(hdLatitud.Value);
                    oNegocio.LONGITUD = Convert.ToDouble(hdLongitud.Value);
                    var loResultado = new BLL.NegocioBLL().ModificarNegocio(oNegocio);

                    if (!loResultado)
                    {
                        loResultado = new BLL.NegocioBLL().AltaNegocio(oNegocio);
                    }

                    if (loResultado)
                    {
                        lblDireccion.Text = oNegocio.DIRECCION_MAPS;
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeDatosDelNegocio, "Información"));
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeDatosDelNegocioError, "Información"));
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeDatosDireccion, "Información"));
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeDatosDelNegocioError));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            hdIdDireccionMaps.Value = string.Empty;
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void ObtenerNegocio()
        {
            try
            {
                var oNegocio = new BLL.NegocioBLL().ObtenerNegocio();

                if (oNegocio != null)
                    lblDireccion.Text = oNegocio.DIRECCION_MAPS;
                else
                    lblDireccion.Text = "La dirección del negocio no se encuentra cargada";
            }
            catch (Exception ex)
            {
                lblDireccion.Text = "La dirección del negocio no se encuentra cargada";

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        #endregion

    }
}