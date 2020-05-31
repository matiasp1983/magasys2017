using BLL.Common;
using System;
using System.Collections.Generic;

namespace PL.CustomersWebSite
{
    public partial class ConsultaReservaEdicion : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarReservasEdicion();
        }

        #endregion

        #region Métodos Privados

        private void CargarReservasEdicion()
        {
            int loCodigoReserva = 0;
            List<BLL.ReservaEdicionListado> lstReservaEdicionListado = new List<BLL.ReservaEdicionListado>();

            if (Convert.ToInt32(Session[Enums.Session.CodigoReserva.ToString()]) > 0)
            {
                loCodigoReserva = Convert.ToInt32(Session[Enums.Session.CodigoReserva.ToString()]);
                Session.Remove(Enums.Session.CodigoReserva.ToString());
                var lstReservaEdicion = new BLL.ReservaEdicionBLL().ObtenerReservasEdicionPorReserva(loCodigoReserva);

                if (lstReservaEdicion.Count > 0)
                {
                    lsvReservaEdicion.DataSource = lstReservaEdicion;
                    lsvReservaEdicion.DataBind();
                }

                var oReserva = new BLL.ReservaBLL().ObtenerInfoReserva(loCodigoReserva);
                txtCodigo.Text = oReserva.ID_RESERVA.ToString();
                txtEstado.Text = oReserva.ESTADO;
                if (oReserva.FECHA_INICIO != null)
                    txtFechaIni.Text = oReserva.FECHA_INICIO.Value.ToShortDateString();
                if (oReserva.FECHA_FIN != null)
                    txtFechaFin.Text = oReserva.FECHA_FIN.Value.ToShortDateString();
                txtNombreProducto.Text = oReserva.NOMBRE_PRODUCTO;
                txtDescripcionProducto.Text = oReserva.DESCRIPCION_PRODUCTO;
                txtTipoReserva.Text = oReserva.TIPO_RESERVA;
                txtFormaEntrega.Text = oReserva.FORMA_ENTREGA;
            }
        }

        #endregion
    }
}