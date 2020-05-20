using BLL;
using System;
using System.Collections.Generic;
using System.Web.UI;
using BLL.Common;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using NLog;
using BLL.Filters;

namespace PL.AdminDashboard
{
	public partial class ConfirmarReservas : System.Web.UI.Page
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
                        BLL.DAL.Reserva oReservaConfirmada = new BLL.ReservaBLL().ObtenerReserva(Convert.ToInt32(((Label)loItem.Controls[3]).Text));
                        oReservaConfirmada.COD_ESTADO = 7;

                        // loResutado = new BLL.ReservaBLL().ModificarReserva(oReservaConfirmada);
                        if (oReservaConfirmada.COD_TIPO_RESERVA == 1)
                        {
                            BLL.DAL.ReservaEdicion oReservaEdicion = new BLL.ReservaEdicionBLL().ObtenerReservaEdicion(oReservaConfirmada.ID_RESERVA);
                        }
                        // loResutado = new ReservaEdicionBLL().AltaReservaEdicion(oReservaConfirmada);
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
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductoIngreso.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarGrilla() // EL SIGUIENTE CÓDIGO ES DE EJEMPLO PARA PODER VISUALIZAR LA GRILLA!!
        {
            //ListView lsvReservas = (ListView)Session[Enums.Session.ListadoReservaConfirmar.ToString()];
            ReservaFiltro oReservaFiltro = new ReservaFiltro();
            oReservaFiltro.COD_ESTADO = 16;

            var lstReservasConfirmar = new BLL.ReservaBLL().ObtenerReservas(oReservaFiltro);

            lsvReservaEdicion.DataSource = lstReservasConfirmar;
            lsvReservaEdicion.DataBind();
            //Session.Remove(Enums.Session.ListadoReservaConfirmar.ToString());
        }

        private List<ReservaClienteListado> MapListViewToListObject(ListView pListView)
        {
            List<ReservaClienteListado> lstReservas = (List<ReservaClienteListado>)pListView.DataSource;

            return lstReservas;
        }

        #endregion
    }
}