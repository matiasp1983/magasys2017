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
            int lvMarcados = 0;

            try
            {
                if (Session[Enums.Session.DevolucionProducto.ToString()] != null)
                {
                    if (!SePuedeDevolver())
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeDevolucionStockInsuficiente));
                        return;
                    }

                    var loDevolucionProducto = (BLL.DetalleDevolucion)Session[Enums.Session.DevolucionProducto.ToString()];

                    if (loDevolucionProducto.CANTIDAD > 0) // Se actualizan las cantidades y estado del producto.
                    {
                        loResutado = new BLL.ProductoEdicionBLL().ActualizarCantidadDisponible(loDevolucionProducto.COD_PRODUCTO_EDICION, loDevolucionProducto.CANTIDAD, DateTime.Now);
                        if (!loResutado)
                            return;
                    }

                    foreach (var loItem in lsvReservaEdicion.Items)
                    {
                        if (((HtmlInputCheckBox)loItem.Controls[1]).Checked)
                        {
                            // Se Anulan las reservasEdicion marcadas
                            int codReserva = Convert.ToInt32(((Label)loItem.Controls[11]).Text);
                            BLL.DAL.ReservaEdicion oReservaEdicion = new BLL.ReservaEdicionBLL().ObtenerReservaEdicion(codReserva);
                            oReservaEdicion.COD_ESTADO = 12;
                            new BLL.ReservaEdicionBLL().ModificarReservaEdidion(oReservaEdicion);

                            BLL.DAL.Reserva oReserva = new BLL.ReservaBLL().ObtenerReserva(Convert.ToInt32(((Label)loItem.Controls[3]).Text));
                            if (oReserva.COD_TIPO_RESERVA == 1) // Se actualiza el estado cuando se trata de una reserva Única
                            {
                                oReserva.COD_ESTADO = 9; // Reserva Anulada   
                                loResutado = new BLL.ReservaBLL().ModificarReserva(oReserva);
                                if (!loResutado)
                                    break;
                            }

                            lvMarcados++;

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

                    BLL.DAL.ProductoDevolucion oProductoDevolucion = new BLL.DAL.ProductoDevolucion()
                    {
                        FECHA = DateTime.Now,
                        COD_ESTADO = 1,
                    };

                    List<BLL.DAL.DetalleProductoDevolucion> lstDetalleProductoDevolucion = new List<BLL.DAL.DetalleProductoDevolucion>();

                    BLL.DAL.DetalleProductoDevolucion oDetalleProductoDevolucion = new BLL.DAL.DetalleProductoDevolucion
                    {
                        COD_PRODUCTO_EDICION = loDevolucionProducto.COD_PRODUCTO_EDICION,
                        CANTIDAD = loDevolucionProducto.CANTIDAD + lvMarcados
                    };

                    lstDetalleProductoDevolucion.Add(oDetalleProductoDevolucion);
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

        private bool SePuedeDevolver()
        {
            bool lvSePuedeDevolver = false;
            int lvMarcados = 0;
            int lvNoMarcados = 0;

            foreach (var loItem in lsvReservaEdicion.Items)
            {
                if (((HtmlInputCheckBox)loItem.Controls[1]).Checked) // Las reservas seleccionadas se van a anular, por lo tanto no se les guarda stock
                    lvMarcados = lvMarcados + 1;
            }

            lvNoMarcados = lsvReservaEdicion.Items.Count - lvMarcados; // Contar Reservas no seleccionadas.

            var loDevolucionProducto = (BLL.DetalleDevolucion)Session[Enums.Session.DevolucionProducto.ToString()];
            // loDevolucionProducto.CANTIDAD --> es la cantidad que se quiere devolver

            // Solo se puede devolver la cantidad indicada en loDevolucionProducto.CANTIDAD, no puede ser ni más ni menos.
            if (lvMarcados > loDevolucionProducto.CANTIDAD)
            {
                return lvSePuedeDevolver;
            }

            if (loDevolucionProducto.STOCK >= loDevolucionProducto.CANTIDAD)
                lvSePuedeDevolver = true;

            else
            if (((loDevolucionProducto.STOCK + loDevolucionProducto.CANTIDAD_RESERVAS) == loDevolucionProducto.CANTIDAD) && (lvMarcados == lsvReservaEdicion.Items.Count))
            {   // Cuando la suma del Stock y las cantidades reservadas sea igual a la Cantidad que se desea devolver.
                // Se puede continuar cuando todas las reservas se encuentren seleccionadas para anular.               
                lvSePuedeDevolver = true;
            }
            else
            if (loDevolucionProducto.CANTIDAD > loDevolucionProducto.STOCK)
            {   // Cuando la Cantidad a devolver es mayor al Stock.
                var lvCantidadMaximaQueSePuedeReservar = loDevolucionProducto.STOCK + loDevolucionProducto.CANTIDAD_RESERVAS - loDevolucionProducto.CANTIDAD;
                if (lvNoMarcados <= lvCantidadMaximaQueSePuedeReservar)
                    lvSePuedeDevolver = true;
            }

            if (lvSePuedeDevolver == true)
            {
                loDevolucionProducto.CANTIDAD = loDevolucionProducto.CANTIDAD - lvMarcados;
                Session[Enums.Session.DevolucionProducto.ToString()] = loDevolucionProducto;
            }

            return lvSePuedeDevolver;
        }

        #endregion
    }
}
