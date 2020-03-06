using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using BLL.Common;
using BLL.DAL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace PL.AdminDashboard
{
    public partial class ReservaEdicion : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                CargarGrilla();
            }
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
                            ID_RESERVA_EDICION = new BLL.ReservaEdicionBLL().ObtenerProximaReserva(),
                            COD_RESERVA = Convert.ToInt32(((Label)loItem.Controls[3]).Text),
                            COD_PROD_EDICION = Convert.ToInt32(((Label)loItem.Controls[9]).Text),
                            FECHA = DateTime.Now,
                            COD_ESTADO = 10

                        };

                        loResutado = new BLL.ReservaEdicionBLL().AltaReservaEdicion(oReservaConfirmada);
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeProductoIngresoSuccessAlta, "Alta de Ingreso de productos"));
                        Response.Redirect("Index.aspx", false);

                    }
                }
            }

            catch (Exception)
            {
                throw;
            }

        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Métodos Privados

        private void CargarGrilla() // EL SIGUIENTE CÓDIGO ES DE EJEMPLO PARA PODER VISUALIZAR LA GRILLA!!
        {
            ListView lsvReservas = (ListView)Session[Enums.Session.ListadoReservaConfirmar.ToString()];
            List<ReservaClienteListado> lstReservasConfirmar = MapListViewToListObject(lsvReservas);

            lsvReservaEdicion.DataSource = lstReservasConfirmar;
            lsvReservaEdicion.DataBind();
            Session.Remove(Enums.Session.ListadoReservaConfirmar.ToString());
        }

        private List<ReservaClienteListado> MapListViewToListObject(ListView pListView)
        {
            bool loResultado = false;

            List<ReservaClienteListado> lstReservas = (List<ReservaClienteListado>)pListView.DataSource;

            if (loResultado)
                lstReservas = null;

            return lstReservas;
        }

        #endregion
    }
}