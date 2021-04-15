using BLL.Common;
using BLL.DAL;
using BLL.Filters;
using NLog;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PL.CustomersWebSite
{
    public partial class Index : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                CargarCantidadDePedidosDesdeSession();
                ObtenerTotalAPagar();
                ObtenerReservasConfirmadasMensuales();
                CargarTipoReserva();
                CargarEstados();
                CargarFormaDeEntrega();
                CargarGrilla();
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void LsvReservas_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loIdReserva = ((BLL.ReservaListado)e.Item.DataItem).ID_RESERVA.ToString();

                HtmlButton btnVisualizar = ((HtmlButton)e.Item.FindControl("btnVisualizar"));
                btnVisualizar.Attributes.Add("value", loIdReserva);

                HiddenField hdIdReserva = ((HiddenField)e.Item.FindControl("hdIdReserva"));
                hdIdReserva.Value = loIdReserva.ToString();
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnVisualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Add(Enums.Session.CodigoReserva.ToString(), Convert.ToInt64(((HtmlButton)sender).Attributes["value"]));
                Response.Redirect("ConsultaReservaEdicion.aspx", false);
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
            ReservaEdicion loReservaEdicion = null;

            try
            {
                if (!String.IsNullOrEmpty(hdIdReservaAnular.Value))
                {
                    var loIdReserva = Convert.ToInt32(hdIdReservaAnular.Value);
                    var loReserva = new BLL.ReservaBLL().ObtenerReserva(loIdReserva);

                    if (loReserva.COD_ESTADO == 8)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaNoSePuedeAnular)); //"La reserva está Finalizada, no se puede Anular."
                        return;
                    }
                    else if (loReserva.COD_ESTADO == 9)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaAnulada)); //"La reserva ya se encuentra Anulada."
                        return;
                    }

                    if (loReserva.COD_TIPO_RESERVA == 1) // Reserva Única
                    {
                        // Controlar el estado de la reservasEdicion

                        loReservaEdicion = new BLL.ReservaEdicionBLL().ObtenerReservaEdicionDeReservaUnica(loReserva.ID_RESERVA);

                        if (loReservaEdicion.COD_ESTADO == 11) // Reserva Edición se encuentra Entregada               
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaAnularReservaEntregada)); //"El producto fue entregado, no se puede anular."
                            return;
                        }
                        else if (loReservaEdicion.COD_ESTADO == 17) // Reserva Edición se encuentra En reparto
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaAnularReservaEnReparto)); //"El producto se encuentra en reparto, no se puede anular."
                            return;
                        }
                        else if (loReservaEdicion.COD_ESTADO == 12)
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaAnulada)); //"La reserva ya se encuentra Anulada."
                            return;
                        }
                        else if (loReservaEdicion.COD_ESTADO == 15) // Reserva Edición Confirmada, se debe devolver el producto por lo tanto sumar 1 al stock
                        {
                            loReservaEdicion.ProductoEdicion.CANTIDAD_DISPONIBLE++;
                            loResultado = new BLL.ProductoEdicionBLL().ModificarProductoEdicion(loReservaEdicion.ProductoEdicion);
                        }

                        loReservaEdicion.COD_ESTADO = 12; // Estado Anulada para la reserva edición
                        if (loResultado)
                            loResultado = new BLL.ReservaEdicionBLL().ModificarReservaEdidion(loReservaEdicion);
                        else if (loReservaEdicion.COD_ESTADO != 15)
                            loResultado = new BLL.ReservaEdicionBLL().ModificarReservaEdidion(loReservaEdicion);
                    }

                    // La reserva Periódica se anula directamente sin controlar las resevasEdicion. Si hay resevasEdicion deberán continuar su curso hasta finalizar.
                    loReserva.COD_ESTADO = 9; // Estado Anulada
                    if (loResultado)
                        loResultado = new BLL.ReservaBLL().ModificarReserva(loReserva);
                    else if (loReserva.COD_TIPO_RESERVA == 2)
                        loResultado = new BLL.ReservaBLL().ModificarReserva(loReserva);

                    if (loResultado)
                    {
                        var oUsuario = (BLL.DAL.Usuario)Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()];
                        var oClienteSession = new BLL.ClienteBLL().ObtenerClientePorUsuario(oUsuario.ID_USUARIO);

                        // Informar al Cliente que la entrega a domicilio de la edición fue cancelada
                        Mensaje oMensaje = new Mensaje()
                        {
                            COD_CLIENTE = oClienteSession.ID_CLIENTE,

                            TIPO_MENSAJE = "warning-element",
                            FECHA_REGISTRO_MENSAJE = DateTime.Now
                        };

                        var oProducto = new BLL.ProductoBLL().ObtenerProductoPorCodigo(loReserva.COD_PRODUCTO);

                        if (loReserva.COD_TIPO_RESERVA == 1)
                        {
                            var oProductoEdicion = new BLL.ProductoEdicionBLL().ObtenerEdicion(loReservaEdicion.COD_PROD_EDICION);
                            oMensaje.DESCRIPCION = "Se anuló la reserva de la edición " + oProductoEdicion.EDICION + " del producto '" + oProducto.NOMBRE + "'.";
                        }
                        else
                            oMensaje.DESCRIPCION = "Se anuló la reserva del producto '" + oProducto.NOMBRE + "'.";

                        loResultado = new BLL.MensajeBLL().AltaMensaje(oMensaje);
                    }
                }

                if (loResultado)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeReservaAnularOk));
                    ObtenerReservasConfirmadasMensuales();
                    CargarGrilla();
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaAnularFailure));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        #endregion

        #region Métodos Privados

        private void OcultarDivsMensajes()
        {
            dvMensajeLsvReservas.Visible = false;
        }

        private void ObtenerTotalAPagar()
        {
            var oUsuario = (BLL.DAL.Usuario)Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()];
            var oClienteSession = new BLL.ClienteBLL().ObtenerClientePorUsuario(oUsuario.ID_USUARIO);
            lblTotalAPagar.Text = "$" + new BLL.VentaBLL().ObtenerTotalAPagar(oClienteSession.ID_CLIENTE).ToString();
        }

        private void ObtenerReservasConfirmadasMensuales()
        {
            var oUsuario = (BLL.DAL.Usuario)Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()];
            var oClienteSession = new BLL.ClienteBLL().ObtenerClientePorUsuario(oUsuario.ID_USUARIO);
            lblReservasConfirmadas.Text = new BLL.ReservaBLL().ObtenerCantidadReservasConfirmadasMensuales(oClienteSession.ID_CLIENTE).ToString();
        }

        private void CargarTipoReserva()
        {
            var oReservaBLL = new BLL.ReservaBLL();

            try
            {
                ddlTipoReserva.DataSource = oReservaBLL.ObtenerTipoReserva();
                ddlTipoReserva.DataTextField = "DESCRIPCION";
                ddlTipoReserva.DataValueField = "ID_TIPO_RESERVA";
                ddlTipoReserva.DataBind();
                ddlTipoReserva.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarEstados()
        {
            var oEstado = new BLL.EstadoBLL();

            try
            {
                ddlEstado.DataSource = oEstado.ObtenerEstados("RESERVA");
                ddlEstado.DataTextField = "NOMBRE";
                ddlEstado.DataValueField = "ID_ESTADO";
                ddlEstado.DataBind();
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarFormaDeEntrega()
        {
            try
            {
                ddlFormaEntrega.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlFormaEntrega.Items.Insert(1, "Retira en Local");
                ddlFormaEntrega.Items.Insert(2, "Envío a Domicilio");
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private ReservaFiltro CargarReservaFiltro()
        {
            ReservaFiltro oReservaFiltro = null;

            oReservaFiltro = new ReservaFiltro();

            var oUsuario = (BLL.DAL.Usuario)Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()];
            var oClienteSession = new BLL.ClienteBLL().ObtenerClientePorUsuario(oUsuario.ID_USUARIO);

            if (oClienteSession != null)
            {
                oReservaFiltro.COD_CLIENTE = oClienteSession.ID_CLIENTE;

                if (!String.IsNullOrEmpty(ddlEstado.SelectedValue))
                    oReservaFiltro.COD_ESTADO = Convert.ToInt32(ddlEstado.SelectedValue);

                if (!String.IsNullOrEmpty(ddlTipoReserva.SelectedValue))
                    oReservaFiltro.COD_TIPO_RESERVA = Convert.ToInt32(ddlTipoReserva.SelectedValue);

                if (!String.IsNullOrEmpty(txtNombreProducto.Text))
                    oReservaFiltro.NOMBRE_PRODUCTO = txtNombreProducto.Text;

                if (!String.IsNullOrEmpty(txtFechaIniReserva.Text))
                    oReservaFiltro.FECHAINICIORESERVADESDE = Convert.ToDateTime(txtFechaIniReserva.Text);

                if (!String.IsNullOrEmpty(txtFechaFinReserva.Text))
                    oReservaFiltro.FECHAFINRESERVADESDE = Convert.ToDateTime(txtFechaFinReserva.Text);

                if (!String.IsNullOrEmpty(ddlFormaEntrega.SelectedValue))
                    oReservaFiltro.COD_FORMA_ENTREGA = ddlFormaEntrega.SelectedValue;
            }

            return oReservaFiltro;
        }

        private void CargarGrilla()
        {
            try
            {
                var oReservaFiltro = CargarReservaFiltro();

                if (oReservaFiltro != null)
                {
                    var lstReserva = new BLL.ReservaBLL().ObtenerReservasPorCliente(oReservaFiltro);

                    if (lstReserva != null && lstReserva.Count > 0)
                    {
                        lsvReservas.DataSource = lstReserva;
                        lsvReservas.Visible = true;
                    }
                    else
                    {
                        dvMensajeLsvReservas.InnerHtml = MessageManager.Info(dvMensajeLsvReservas, Message.MsjeListadoReservaFiltroSinResultados, false);
                        dvMensajeLsvReservas.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lsvReservas.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            lsvReservas.DataBind();
        }

        private void LimpiarCampos()
        {
            FormIndex.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            FormIndex.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
        }

        private void CargarCantidadDePedidosDesdeSession()
        {
            Master.CantidadDePedidos = Convert.ToInt32(Session[Enums.Session.CantidadDePedidos.ToString()]);
        }

        #endregion
    }
}