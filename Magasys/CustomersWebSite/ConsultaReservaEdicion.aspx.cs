using BLL;
using BLL.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

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

        protected void LsvReservas_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loIdReservaEdicion = ((BLL.ReservaEdicionListado)e.Item.DataItem).ID_RESERVA_EDICION.ToString();

                HiddenField hdIdReservaEdicion = ((HiddenField)e.Item.FindControl("hdIdReservaEdicion"));
                hdIdReservaEdicion.Value = loIdReservaEdicion.ToString();
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnAnular_Click(object sender, EventArgs e)
        {
            bool loResultado = false;

            try
            {
                if (!String.IsNullOrEmpty(hdIdReservaEdicionAnular.Value))
                {
                    var loIdReservaEdicion = Convert.ToInt32(hdIdReservaEdicionAnular.Value);
                    var oReservaEdicion = new ReservaEdicionBLL().ObtenerReservaEdicion(Convert.ToInt32(loIdReservaEdicion));
                    var oProductoEdicion = new ProductoEdicionBLL().ObtenerEdicion(oReservaEdicion.COD_PROD_EDICION);

                    if (oReservaEdicion.COD_ESTADO == 11) // Reserva Edición se encuentra Entregada               
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal("La edición " + oProductoEdicion.EDICION + "' fue entregada, no es posible anular."));
                        CargarReservasEdicion();
                        return;
                    }
                    else if (oReservaEdicion.COD_ESTADO == 17) // Reserva Edición se encuentra En reparto
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal("La edición " + oProductoEdicion.EDICION + "' se encuentra en reparto, no es posible anular."));
                        CargarReservasEdicion();
                        return;
                    }
                    else if (oReservaEdicion.COD_ESTADO == 12)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal("La edición " + oProductoEdicion.EDICION + "' ya se encuentra Anulada."));
                        CargarReservasEdicion();
                        return;
                    }

                    if (oReservaEdicion.COD_ESTADO == 15) // Reserva Edición Confirmada, se debe devolver el producto por lo tanto sumar 1 al stock
                    {
                        oProductoEdicion.CANTIDAD_DISPONIBLE++;
                        oProductoEdicion.COD_ESTADO = 1; // Se indica 1 por las dudas que el estado sea 2 (por falta de stock).
                        loResultado = new ProductoEdicionBLL().ModificarProductoEdicion(oProductoEdicion);
                    }

                    oReservaEdicion.COD_ESTADO = 12; // Estado Anulada para la reserva edición
                    if (loResultado)
                        loResultado = new ReservaEdicionBLL().ModificarReservaEdidion(oReservaEdicion);
                    else if (oReservaEdicion.COD_ESTADO != 15)
                        loResultado = new ReservaEdicionBLL().ModificarReservaEdidion(oReservaEdicion);

                    if (loResultado)
                    {
                        BLL.DAL.Reserva oReserva = new ReservaBLL().ObtenerReserva(oReservaEdicion.COD_RESERVA);
                        if (oReserva.COD_TIPO_RESERVA == 1) // Solo anulamos la Reserva cuando es única
                        {
                            oReserva.COD_ESTADO = 9; // Estado Anulada
                            loResultado = new ReservaBLL().ModificarReserva(oReserva);
                        }

                        if (loResultado)
                        {
                            var oUsuario = (BLL.DAL.Usuario)Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()];
                            var oClienteSession = new ClienteBLL().ObtenerClientePorUsuario(oUsuario.ID_USUARIO);

                            // Informar al Cliente que la reserva fue anulada.
                            BLL.DAL.Mensaje oMensaje = new BLL.DAL.Mensaje()
                            {
                                COD_CLIENTE = oClienteSession.ID_CLIENTE,
                                DESCRIPCION = "Se anuló la reserva de la edición " + oProductoEdicion.EDICION + " del producto '" + txtNombreProducto.Text + "'.",
                                TIPO_MENSAJE = "warning-element",
                                FECHA_REGISTRO_MENSAJE = DateTime.Now
                            };

                            loResultado = new MensajeBLL().AltaMensaje(oMensaje);
                        }
                    }
                }

                if (loResultado)
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeReservaAnularOk));
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaAnularFailure));

                CargarReservasEdicion();
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        #endregion

        #region Métodos Privados

        private void CargarReservasEdicion()
        {
            int loCodigoReserva = 0;
            List<ReservaEdicionListado> lstReservaEdicionListado = new List<ReservaEdicionListado>();

            if (Convert.ToInt32(Session[Enums.Session.CodigoReserva.ToString()]) > 0)
            {
                loCodigoReserva = Convert.ToInt32(Session[Enums.Session.CodigoReserva.ToString()]);               
                var lstReservaEdicion = new ReservaEdicionBLL().ObtenerReservasEdicionPorReserva(loCodigoReserva);

                if (lstReservaEdicion.Count > 0)
                {
                    lsvReservaEdicion.DataSource = lstReservaEdicion;
                    lsvReservaEdicion.DataBind();
                }

                var oReserva = new ReservaBLL().ObtenerInfoReserva(loCodigoReserva);
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