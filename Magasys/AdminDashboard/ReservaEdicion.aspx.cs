using BLL;
using System;
using System.Collections.Generic;
using System.Web.UI;
using BLL.Common;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using NLog;

namespace PL.AdminDashboard
{
    public partial class ReservaEdicion : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                CargarGrilla();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            bool loResutado = false;
            List<ReservaClienteListado> lstReservasConfirmar = (List<ReservaClienteListado>)lsvReservaEdicion.DataSource;
            try
            {
                foreach (var loItem in lsvReservaEdicion.Items)
                {
                    if (((HtmlInputCheckBox)loItem.Controls[1]).Checked)
                    {
                        BLL.DAL.ReservaEdicion oReservaConfirmada = new BLL.DAL.ReservaEdicion()
                        {
                            ID_RESERVA_EDICION = new ReservaEdicionBLL().ObtenerProximaReserva(),
                            COD_RESERVA = Convert.ToInt32(((Label)loItem.Controls[3]).Text),
                            COD_PROD_EDICION = Convert.ToInt32(((Label)loItem.Controls[9]).Text),
                            FECHA = DateTime.Now,
                            COD_ESTADO = 15
                        };

                        loResutado = new ReservaEdicionBLL().AltaReservaEdicion(oReservaConfirmada);
                        if (!loResutado)
                            break;
                    }
                }

                if (loResutado)
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeProductoIngresoSuccessAlta, "Alta de Ingreso de productos", "Index.aspx"));
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaFailure));
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            Session.Remove(Enums.Session.ListadoReservaConfirmar.ToString());
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.ListadoReservaConfirmar.ToString());
            Response.Redirect("ProductoIngreso.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarGrilla() // EL SIGUIENTE CÓDIGO ES DE EJEMPLO PARA PODER VISUALIZAR LA GRILLA!!
        {
            if (Session[Enums.Session.ListadoReservaConfirmar.ToString()] != null)
            {
                ListView lsvReservas = (ListView)Session[Enums.Session.ListadoReservaConfirmar.ToString()];
                List<ReservaClienteListado> lstReservasConfirmar = MapListViewToListObject(lsvReservas);
                lblCantidadIngresada.Text = "40";
                lblReservasSeleccionadas.Text = "10";
                lblReservasTotales.Text = "120";
                lsvReservaEdicion.DataSource = lstReservasConfirmar;
                lsvReservaEdicion.DataBind();
            }
        }

        private List<ReservaClienteListado> MapListViewToListObject(ListView pListView)
        {
            List<ReservaClienteListado> lstReservas = (List<ReservaClienteListado>)pListView.DataSource;

            return lstReservas;
        }

        #endregion
    }
}