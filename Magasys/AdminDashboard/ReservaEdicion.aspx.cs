using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Métodos Privados

        private void CargarGrilla() // EL SIGUIENTE CÓDIGO ES DE EJEMPLO PARA PODER VISUALIZAR LA GRILLA!!
        {
            List<ReservaClienteListado> lstReservaClienteListado = new List<ReservaClienteListado>();
            ReservaClienteListado oReservaListado = new ReservaClienteListado();
            oReservaListado.ID_RESERVA = 1;
            oReservaListado.CLIENTE = "Maria";
            oReservaListado.PRODUCTO = "Revista Gente";
            lstReservaClienteListado.Add(oReservaListado);
            ReservaClienteListado oReservaListado2 = new ReservaClienteListado();
            oReservaListado2.ID_RESERVA = 2;
            oReservaListado2.CLIENTE = "Maria";
            oReservaListado2.PRODUCTO = "Revista Gente";
            lstReservaClienteListado.Add(oReservaListado2);
            ReservaClienteListado oReservaListado3 = new ReservaClienteListado();
            oReservaListado3.ID_RESERVA = 3;
            oReservaListado3.CLIENTE = "Maria";
            oReservaListado3.PRODUCTO = "Revista Gente";
            lstReservaClienteListado.Add(oReservaListado3);
            lsvReservaEdicion.DataSource = lstReservaClienteListado;
            lsvReservaEdicion.DataBind();
        }

        #endregion
    }
}