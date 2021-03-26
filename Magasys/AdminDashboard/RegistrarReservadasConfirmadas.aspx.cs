using BLL.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class RegistrarReservadasConfirmadas : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            dvMensajeLsvReservaEdicion.Visible = false;

            if (!IsPostBack)
                CargarProducto();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            bool loResutado = false;
            try
            {
                if (Session[Enums.Session.DevolucionProducto.ToString()] != null)
                {
                    if (!ActualizarCantidad())
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeDevolucionStockInsuficiente));
                        return;
                    }

                    var loDevolucionProducto = (BLL.DetalleDevolucion)Session[Enums.Session.DevolucionProducto.ToString()];
                    BLL.DAL.ProductoDevolucion oProductoDevolucion = new BLL.DAL.ProductoDevolucion()
                    {
                        FECHA = DateTime.Now,
                        COD_ESTADO = 1,
                    };
                    List<BLL.DAL.DetalleProductoDevolucion> lstDetalleProductoDevolucion = new List<BLL.DAL.DetalleProductoDevolucion>();

                    BLL.DAL.DetalleProductoDevolucion oDetalleProductoDevolucion = new BLL.DAL.DetalleProductoDevolucion
                    {
                        COD_PRODUCTO_EDICION = loDevolucionProducto.COD_PRODUCTO_EDICION
                    };

                    oDetalleProductoDevolucion.CANTIDAD = loDevolucionProducto.CANTIDAD;
                    lstDetalleProductoDevolucion.Add(oDetalleProductoDevolucion);

                    // Se actualizan las cantidades y estado del producto
                    loResutado = new BLL.ProductoEdicionBLL().ActualizarCantidadDisponible(oDetalleProductoDevolucion.COD_PRODUCTO_EDICION, loDevolucionProducto.CANTIDAD, DateTime.Now);

                    foreach (var loItem in lsvReservaEdicion.Items)
                    {
                        if (((HtmlInputCheckBox)loItem.Controls[1]).Checked)
                        {
                            // Se anulan las reservas marcadas
                            int codReserva = Convert.ToInt32(((Label)loItem.Controls[11]).Text);
                            BLL.DAL.ReservaEdicion oReservaEdicion = new BLL.ReservaEdicionBLL().ObtenerReservaEdicion(codReserva);
                            oReservaEdicion.COD_ESTADO = 12;
                            new BLL.ReservaEdicionBLL().ModificarReservaEdidion(oReservaEdicion);

                            BLL.DAL.Reserva oReserva = new BLL.ReservaBLL().ObtenerReserva(Convert.ToInt32(((Label)loItem.Controls[3]).Text));
                            if (oReserva.COD_TIPO_RESERVA == 1) // Se actualiza el estado cuando se trata de una reserva Única
                            {
                                oReserva.COD_ESTADO = 9; // Reserva Anulada   
                                loResutado = new BLL.ReservaBLL().ModificarReserva(oReserva);
                            }

                            if (!loResutado)
                                break;

                            // Informar al Cliente que la entrega a domicilio de la edición fue cancelada
                            BLL.DAL.Mensaje oMensaje = new BLL.DAL.Mensaje()
                            {
                                COD_CLIENTE = Convert.ToInt32(((Label)loItem.Controls[13]).Text),
                                DESCRIPCION = "Se anuló la reserva de la edición " + ((Label)loItem.Controls[9]).Text + " del producto '" + ((Label)loItem.Controls[7]).Text + "'.",
                                TIPO_MENSAJE = "warning-element",
                                FECHA_REGISTRO_MENSAJE = DateTime.Now
                            };

                            loResutado = new BLL.MensajeBLL().AltaMensaje(oMensaje);
                            if (!loResutado)
                                break;
                        }
                    }

                    if (loResutado)
                        loResutado = new BLL.ProductoDevolucionBLL().AltaDevolucion(oProductoDevolucion, lstDetalleProductoDevolucion);

                    if (loResutado)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeDevolucionSuccessAlta, "", "ProductoDevolucion.aspx"));
                        Session.Remove(Enums.Session.DevolucionProducto.ToString());
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeDevolucionFailure));
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductoDevolucion.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarProducto()
        {
            try
            {
                if (Session[Enums.Session.DevolucionProducto.ToString()] != null)
                {
                    var loDevolucionProducto = (BLL.DetalleDevolucion)Session[Enums.Session.DevolucionProducto.ToString()];
                    txtNombre.Text = loDevolucionProducto.NOMBRE;
                    txtTipoProducto.Text = loDevolucionProducto.TIPO_PRODUCTO;
                    txtEdicion.Text = loDevolucionProducto.EDICION;
                    txtStock.Text = loDevolucionProducto.STOCK.ToString();
                    lblCantidadProductosDevolver.Text = loDevolucionProducto.CANTIDAD.ToString();

                    //Aquí se debe cagar la grilla con las reservas: lsvReservaEdicion
                    List<BLL.ReservaEdicionListado> lstReservaListado = new BLL.ReservaEdicionBLL().ObtenerReservaEdicionPorProdEdicion((long)loDevolucionProducto.COD_PRODUCTO_EDICION);

                    if (lstReservaListado.Any())
                    {
                        lsvReservaEdicion.DataSource = lstReservaListado;
                        lsvReservaEdicion.DataBind();
                        lsvReservaEdicion.Visible = true;
                    }
                    else
                    {
                        // Las siguientes dos líneas se deben llamar o agregar cuando no hay registros para mostrar en lsvReservaEdicion:
                        dvMensajeLsvReservaEdicion.InnerHtml = MessageManager.Info(dvMensajeLsvReservaEdicion, Message.MsjeEntregaProductoFiltroSinResultados, false);
                        dvMensajeLsvReservaEdicion.Visible = true;
                    }
                }
                else
                    Response.Redirect("ProductoDevolucion.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private bool ActualizarCantidad()
        {
            bool lvSePuedeDevolver = false;
            int lvMarcados = 0; // Acumula la cantidad de stock que se necesita para las reservas confirmadas
            foreach (var loItem in lsvReservaEdicion.Items)
            {
                if (!((HtmlInputCheckBox)loItem.Controls[1]).Checked) // Las reservas seleccionadas se van a anular, por lo tanto no se les guarda stock
                    lvMarcados = lvMarcados + 1;
            }
            var loDevolucionProducto = (BLL.DetalleDevolucion)Session[Enums.Session.DevolucionProducto.ToString()];
            // loDevolucionProducto.CANTIDAD --> es la cantidad que se quiere devolver
            if ((loDevolucionProducto.STOCK - loDevolucionProducto.CANTIDAD) >= lvMarcados)
            {
                lvSePuedeDevolver = true;
                //loDevolucionProducto.CANTIDAD = loDevolucionProducto.CANTIDAD - lvMarcados;
            }

            return lvSePuedeDevolver;
        }

        #endregion
    }
}