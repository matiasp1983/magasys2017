using BLL;
using BLL.Common;
using BLL.Filters;
using NLog;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BLL.DAL;
using System.Collections.Generic;

namespace PL.AdminDashboard
{
    public partial class GestionReparto : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            dvMensajelsvReserva.Visible = false;

            if (!Page.IsPostBack)
            {
                CargarTipOperacion();
                CargarGrillaReservas();
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrillaReservas();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            bool loModificarReservaEdidion = false;

            if (ddlTipOperacion.SelectedValue == "Cancelar Entrega")
            {
                foreach (var loItem in lsvReserva.Items)
                {
                    if (((HtmlInputCheckBox)loItem.Controls[1]).Checked)
                    {
                        var loReservaEdicion = new ReservaEdicionBLL().ObtenerReservaEdicion(Convert.ToInt32(((Label)loItem.Controls[11]).Text));
                        loReservaEdicion.COD_ESTADO = 15; // Estado: Confirmada
                        loModificarReservaEdidion = new ReservaEdicionBLL().ModificarReservaEdidion(loReservaEdicion);
                        if (!loModificarReservaEdidion)
                            break;

                        // Informar al Cliente que la entrega a domicilio de la edición fue cancelada
                        Mensaje oMensaje = new Mensaje()
                        {
                            COD_CLIENTE = Convert.ToInt32(((Label)loItem.Controls[13]).Text),
                            DESCRIPCION = "Se canceló la entrega a domicilio de la edición " + ((Label)loItem.Controls[9]).Text + " del producto '" + ((Label)loItem.Controls[7]).Text + "'.",
                            TIPO_MENSAJE = "warning-element",
                            FECHA_REGISTRO_MENSAJE = DateTime.Now
                        };

                        loModificarReservaEdidion = new MensajeBLL().AltaMensaje(oMensaje);
                        if (!loModificarReservaEdidion)
                            break;
                    }
                }

                if (loModificarReservaEdidion)
                {
                    LimpiarCampos();
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeRepartoCancelacion, "Reparto"));
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeRepartoCancelacionError));
            }

            else if (ddlTipOperacion.SelectedValue == "Registrar Entrega")
            {
                foreach (var loItem in lsvReserva.Items)
                {
                    if (((HtmlInputCheckBox)loItem.Controls[1]).Checked)
                    {
                        // Actualizar Stock
                        loModificarReservaEdidion = new ProductoEdicionBLL().ActualizarCantidadDisponible(Convert.ToInt32(((Label)loItem.Controls[19]).Text), 1);
                        if (!loModificarReservaEdidion)
                            break;

                        // Actualizar Estado de Reserva Edicion
                        var oReservaEdicion = new ReservaEdicionBLL().ObtenerReservaEdicion(Convert.ToInt32(((Label)loItem.Controls[11]).Text));
                        var oReserva = new ReservaBLL().ObtenerReserva(oReservaEdicion.COD_RESERVA);
                        var oProductoEdicion = new ProductoEdicionBLL().ObtenerEdicion(oReservaEdicion.COD_PROD_EDICION);
                        oReservaEdicion.COD_ESTADO = 11;
                        loModificarReservaEdidion = new ReservaEdicionBLL().ModificarReservaEdidion(oReservaEdicion);
                        if (!loModificarReservaEdidion)
                            break;

                        // Informar al Cliente que la edición ya fue entregada.
                        Mensaje oMensaje = new Mensaje()
                        {
                            COD_CLIENTE = Convert.ToInt32(((Label)loItem.Controls[13]).Text),
                            DESCRIPCION = "La edición " + ((Label)loItem.Controls[9]).Text + " del producto '" + ((Label)loItem.Controls[7]).Text + "' ya fue entregada.",
                            TIPO_MENSAJE = "success-element",
                            FECHA_REGISTRO_MENSAJE = DateTime.Now
                        };

                        loModificarReservaEdidion = new MensajeBLL().AltaMensaje(oMensaje);
                        if (!loModificarReservaEdidion)
                            break;

                        // Si es Reservar Unica hay que cambiar el estado a Finalizado
                        if (oReserva.COD_TIPO_RESERVA == 1)
                        {
                            oReserva.COD_ESTADO = 8;
                            loModificarReservaEdidion = new ReservaBLL().ModificarReserva(oReserva);
                            if (!loModificarReservaEdidion)
                                break;
                        }

                        BLL.DAL.Venta oVenta = new BLL.DAL.Venta()
                        {
                            FECHA = DateTime.Now,
                            COD_ESTADO = 4, // A Cuenta
                            TOTAL = oProductoEdicion.PRECIO,
                            COD_CLIENTE = Convert.ToInt32(((Label)loItem.Controls[13]).Text),
                            COD_FORMA_PAGO = 2
                        };

                        List<DetalleVenta> lstDetalleVenta = new List<DetalleVenta>();
                        DetalleVenta oDetalleVenta = new DetalleVenta
                        {
                            COD_PRODUCTO_EDICION = oReservaEdicion.COD_PROD_EDICION,
                            PRECIO_UNIDAD = oProductoEdicion.PRECIO,
                            CANTIDAD = 1,
                            SUBTOTAL = oProductoEdicion.PRECIO
                        };
                        lstDetalleVenta.Add(oDetalleVenta);

                        loModificarReservaEdidion = new VentaBLL().AltaVenta(oVenta, lstDetalleVenta);
                        if (!loModificarReservaEdidion)
                            break;
                    }
                }

                if (loModificarReservaEdidion)
                {
                    LimpiarCampos();
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeEntregalSuccess, "Entrega Registrada"));
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeEntregaFailure));
            }
        }

        #endregion

        #region Métodos Privados

        private void CargarTipOperacion()
        {
            try
            {
                ddlTipOperacion.Items.Insert(0, "Registrar Entrega");
                ddlTipOperacion.Items.Insert(1, "Cancelar Entrega");
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private ReservaFiltro CargarReservaFiltro()
        {
            ReservaFiltro oReservaFiltro = new ReservaFiltro();

            if (!String.IsNullOrEmpty(txtProducto.Text))
                oReservaFiltro.NOMBRE_PRODUCTO = txtProducto.Text;

            if (!String.IsNullOrEmpty(txtEdicion.Text))
                oReservaFiltro.EDICION = txtEdicion.Text;

            if (!String.IsNullOrEmpty(txtNombreEdicion.Text))
                oReservaFiltro.NOMBRE_EDICION = txtNombreEdicion.Text;

            return oReservaFiltro;
        }

        private void CargarGrillaReservas()
        {
            try
            {
                var oReservaFiltro = CargarReservaFiltro();

                var lstReserva = new ReservaEdicionBLL().ObtenerReservaEdicionEnReparto(oReservaFiltro);

                if (lstReserva != null && lstReserva.Count > 0)
                    lsvReserva.DataSource = lstReserva;
                else
                {
                    dvMensajelsvReserva.InnerHtml = MessageManager.Info(dvMensajelsvReserva, Message.MsjeListadoReservaFiltroSinResultados, false);
                    dvMensajelsvReserva.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lsvReserva.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            lsvReserva.DataBind();
        }

        private void LimpiarCampos()
        {
            FormGestionReparto.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            FormGestionReparto.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            CargarGrillaReservas();
        }

        #endregion
    }
}