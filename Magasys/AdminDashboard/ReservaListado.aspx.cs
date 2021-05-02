using BLL.Common;
using BLL.DAL;
using BLL.Filters;
using NLog;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class ReservaListado : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                CargarTipoReserva();
                CargarEstados();
                CargarFormaDeEntrega();
                CargarTiposDocumento();
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

                HtmlButton btnModificar = ((HtmlButton)e.Item.FindControl("btnModificar"));
                btnModificar.Attributes.Add("value", loIdReserva);

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
                Session.Add(Enums.Session.IdReserva.ToString(), ((HtmlButton)sender).Attributes["value"]);
                Response.Redirect("ReservaVisualizar.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlEstado.SelectedValue) == 8)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaFinalizadaNoEditar)); //"La reserva Finalizada no se puede modificar."
                return;
            }
            else if (Convert.ToInt32(ddlEstado.SelectedValue) == 9)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaAnuladaNoEditar)); //"La reserva Anulada no se puede modificar."
                return;
            }

            try
            {
                Session.Add(Enums.Session.IdReserva.ToString(), ((HtmlButton)sender).Attributes["value"]);
                Response.Redirect("ReservaEditar.aspx", false);
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
                if (!String.IsNullOrEmpty(hdIdReservaAnular.Value))
                {
                    BLL.DAL.Reserva oReserva = new BLL.ReservaBLL().ObtenerReserva(Convert.ToInt32(hdIdReservaAnular.Value));
                    BLL.DAL.Producto oProducto = new BLL.ProductoBLL().ObtenerProductoPorCodigo(oReserva.COD_PRODUCTO);

                    if (oReserva.COD_ESTADO == 8)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaNoSePuedeAnular)); //"La reserva está Finalizada, no se puede Anular."
                        return;
                    }
                    else if (oReserva.COD_ESTADO == 9)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaAnulada)); //"La reserva ya se encuentra Anulada."
                        return;
                    }

                    if (oReserva.COD_TIPO_RESERVA == 2)
                    {
                        // La reserva Periódica se anula directamente sin controlar las resevasEdicion. Si hay resevasEdicion deberán continuar su curso hasta finalizar.

                        oReserva.COD_ESTADO = 9; // Estado Anulada
                        loResultado = new BLL.ReservaBLL().ModificarReserva(oReserva);
                        if (!loResultado)
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaAnularFailure));
                            return;
                        }

                        // Informar al Cliente que la reserva fue anulada.
                        Mensaje oMensaje = new Mensaje()
                        {
                            COD_CLIENTE = oReserva.COD_CLIENTE,
                            DESCRIPCION = "Se anuló la reserva del producto '" + oProducto.NOMBRE + "'.",
                            TIPO_MENSAJE = "warning-element",
                            FECHA_REGISTRO_MENSAJE = DateTime.Now
                        };

                        loResultado = new BLL.MensajeBLL().AltaMensaje(oMensaje);
                    }
                    else
                    {
                        // RESERVA ÚNICA
                        // Controlar el estado de la reservasEdicion
                        // Si existe una reservaEdición que no se encuentre en condiciones de anular, no se continúa con la anulación.

                        // Controlar el estado de la reservasEdicion
                        var oReservaEdicion = new BLL.ReservaEdicionBLL().ObtenerReservaEdicionDeReservaUnica(oReserva.ID_RESERVA);
                        var oProductoEdicion = new BLL.ProductoEdicionBLL().ObtenerEdicion(oReservaEdicion.COD_PROD_EDICION);
                        if (oReservaEdicion.COD_ESTADO == 11) // Reserva Edición se encuentra Entregada               
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal("La edición " + oProductoEdicion.EDICION + " del producto '" + oProducto.NOMBRE + "' fue entregado, no es posible anular."));
                            return;
                        }
                        else if (oReservaEdicion.COD_ESTADO == 17) // Reserva Edición se encuentra En reparto
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal("La edición " + oProductoEdicion.EDICION + " del producto '" + oProducto.NOMBRE + "' se encuentra en reparto, no es posible anular."));
                            return;
                        }
                        else if (oReservaEdicion.COD_ESTADO == 12)
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal("La edición " + oProductoEdicion.EDICION + " del producto '" + oProducto.NOMBRE + "' ya se encuentra Anulada."));
                            return;
                        }

                        if (oReservaEdicion.COD_ESTADO == 15) // Reserva Edición Confirmada, se debe devolver el producto por lo tanto sumar 1 al stock
                        {
                            oProductoEdicion.CANTIDAD_DISPONIBLE++;
                            oProductoEdicion.COD_ESTADO = 1; // Se indica 1 por las dudas que el estado sea 2 (por falta de stock).
                            loResultado = new BLL.ProductoEdicionBLL().ModificarProductoEdicion(oProductoEdicion);
                        }

                        oReservaEdicion.COD_ESTADO = 12; // Estado Anulada para la reserva edición
                        if (loResultado)
                            loResultado = new BLL.ReservaEdicionBLL().ModificarReservaEdidion(oReservaEdicion);
                        else if (oReservaEdicion.COD_ESTADO != 15)
                            loResultado = new BLL.ReservaEdicionBLL().ModificarReservaEdidion(oReservaEdicion);

                        if (loResultado)
                        {
                            oReserva.COD_ESTADO = 9; // Estado Anulada
                            loResultado = new BLL.ReservaBLL().ModificarReserva(oReserva);

                            if (loResultado)
                            {
                                // Informar al Cliente que la reserva fue anulada.
                                Mensaje oMensaje = new Mensaje()
                                {
                                    COD_CLIENTE = oReserva.COD_CLIENTE,
                                    DESCRIPCION = "Se anuló la reserva de la edición " + oProductoEdicion.EDICION + " del producto '" + oProducto.NOMBRE + "'.",
                                    TIPO_MENSAJE = "warning-element",
                                    FECHA_REGISTRO_MENSAJE = DateTime.Now
                                };

                                loResultado = new BLL.MensajeBLL().AltaMensaje(oMensaje);
                            }
                        }
                    }
                }
                if (loResultado)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeReservaAnularOk));
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

        private void CargarTiposDocumento()
        {
            var oTipoDocumento = new BLL.TipoDocumentoBLL();

            try
            {
                ddlTipoDocumento.DataSource = oTipoDocumento.ObtenerTiposDocumento();
                ddlTipoDocumento.DataTextField = "DESCRIPCION";
                ddlTipoDocumento.DataValueField = "ID_TIPO_DOCUMENTO";
                ddlTipoDocumento.DataBind();
                ddlTipoDocumento.Items.Insert(0, new ListItem(String.Empty, String.Empty));
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

            if (!String.IsNullOrEmpty(txtFechaIniReservaDesde.Text) && !String.IsNullOrEmpty(txtFechaIniReservaHasta.Text) && (Convert.ToDateTime(txtFechaIniReservaDesde.Text) > Convert.ToDateTime(txtFechaIniReservaHasta.Text)))
                return oReservaFiltro;

            if (!String.IsNullOrEmpty(txtFechaFinReservaDesde.Text) && !String.IsNullOrEmpty(txtFechaFinReservaHasta.Text) && (Convert.ToDateTime(txtFechaFinReservaDesde.Text) > Convert.ToDateTime(txtFechaFinReservaHasta.Text)))
                return oReservaFiltro;

            oReservaFiltro = new ReservaFiltro();

            if (!String.IsNullOrEmpty(txtFechaIniReservaDesde.Text))
                oReservaFiltro.FECHAINICIORESERVADESDE = Convert.ToDateTime(txtFechaIniReservaDesde.Text);

            if (!String.IsNullOrEmpty(txtFechaIniReservaHasta.Text))
                oReservaFiltro.FECHAINICIORESERVAHASTA = Convert.ToDateTime(txtFechaIniReservaHasta.Text);

            if (!String.IsNullOrEmpty(txtFechaFinReservaDesde.Text))
                oReservaFiltro.FECHAFINRESERVADESDE = Convert.ToDateTime(txtFechaFinReservaDesde.Text);

            if (!String.IsNullOrEmpty(txtFechaFinReservaHasta.Text))
                oReservaFiltro.FECHAFINRESERVAHASTA = Convert.ToDateTime(txtFechaFinReservaHasta.Text);

            if (!String.IsNullOrEmpty(ddlTipoReserva.SelectedValue))
                oReservaFiltro.COD_TIPO_RESERVA = Convert.ToInt32(ddlTipoReserva.SelectedValue);

            if (!String.IsNullOrEmpty(txtNombreProducto.Text))
                oReservaFiltro.NOMBRE_PRODUCTO = txtNombreProducto.Text;

            if (!String.IsNullOrEmpty(ddlFormaEntrega.SelectedValue))
                oReservaFiltro.COD_FORMA_ENTREGA = ddlFormaEntrega.SelectedValue;

            if (!String.IsNullOrEmpty(ddlEstado.SelectedValue))
                oReservaFiltro.COD_ESTADO = Convert.ToInt32(ddlEstado.SelectedValue);

            if (!String.IsNullOrEmpty(ddlTipoDocumento.SelectedValue) && !String.IsNullOrEmpty(txtNroDocumento.Text))
            {
                oReservaFiltro.TIPO_DOCUMENTO = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
                oReservaFiltro.NRO_DOCUMENTO = Convert.ToInt32(txtNroDocumento.Text);
            }

            if (!String.IsNullOrEmpty(txtNombre.Text))
                oReservaFiltro.NOMBRE = txtNombre.Text;

            if (!String.IsNullOrEmpty(txtApellido.Text))
                oReservaFiltro.APELLIDO = txtApellido.Text;

            if (!String.IsNullOrEmpty(txtAlias.Text))
                oReservaFiltro.ALIAS = txtAlias.Text;

            return oReservaFiltro;
        }

        private void CargarGrilla()
        {
            try
            {
                var oReservaFiltro = CargarReservaFiltro();

                if (oReservaFiltro != null)
                {
                    var lstReserva = new BLL.ReservaBLL().ObtenerReservas(oReservaFiltro);

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
                else
                {
                    dvMensajeLsvReservas.InnerHtml = MessageManager.Info(dvMensajeLsvReservas, Message.MsjeListadoFechaDesdeMayorQueFechaHasta, false);
                    dvMensajeLsvReservas.Visible = true;
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
            FormReservaListado.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            FormReservaListado.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            lsvReservas.Visible = false;
        }

        #endregion
    }
}