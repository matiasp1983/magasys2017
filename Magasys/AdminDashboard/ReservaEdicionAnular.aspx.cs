using BLL;
using BLL.Common;
using BLL.Filters;
using NLog;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class ReservaEdicionAnular : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                CargarEstados();
                CargarTiposProducto();
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

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            bool loResultado = false;
            bool loHayParaProcesar = false;

            try
            {
                foreach (var loItem in lsvReservas.Items)
                {
                    if (((HtmlInputCheckBox)loItem.Controls[1]).Checked)
                    {
                        loHayParaProcesar = true;
                        var oReservaEdicion = new ReservaEdicionBLL().ObtenerReservaEdicion(Convert.ToInt32(((Label)loItem.Controls[11]).Text));
                        var oProductoEdicion = new ProductoEdicionBLL().ObtenerEdicion(oReservaEdicion.COD_PROD_EDICION);

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

                        if (!loResultado)
                            break;

                        BLL.DAL.Reserva oReserva = new ReservaBLL().ObtenerReserva(oReservaEdicion.COD_RESERVA);
                        if (oReserva.COD_TIPO_RESERVA == 1) // Solo anulamos la Reserva cuando es única
                        {
                            oReserva.COD_ESTADO = 9; // Estado Anulada
                            loResultado = new ReservaBLL().ModificarReserva(oReserva);
                            if (!loResultado)
                                break;
                        }

                        // Informar al Cliente que la reserva fue anulada.
                        BLL.DAL.Mensaje oMensaje = new BLL.DAL.Mensaje()
                        {
                            COD_CLIENTE = Convert.ToInt32(((Label)loItem.Controls[13]).Text),
                            DESCRIPCION = "Se anuló la reserva de la edición " + oProductoEdicion.EDICION + " del producto '" + ((Label)loItem.Controls[7]).Text + "'.",
                            TIPO_MENSAJE = "warning-element",
                            FECHA_REGISTRO_MENSAJE = DateTime.Now
                        };

                        loResultado = new MensajeBLL().AltaMensaje(oMensaje);
                        if (!loResultado)
                            break;
                    }
                }

                if (loHayParaProcesar)
                {
                    if (loResultado)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeReservaAnulacion));
                        CargarGrilla();
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaAnulacionFailure));
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeRepartoInfo));
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaAnulacionFailure));

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

        private void CargarEstados()
        {
            var oEstado = new BLL.EstadoBLL();

            try
            {
                ddlEstado.DataSource = oEstado.ObtenerEstados("RESERVA_EDICION").Where(x => x.ID_ESTADO == 10 || x.ID_ESTADO == 15 || x.ID_ESTADO == 18);
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

        private void CargarTiposProducto()
        {
            var oTipoProducto = new BLL.TipoProductoBLL();

            try
            {
                ddlTipoProducto.DataSource = oTipoProducto.ObtenerTiposProducto();
                ddlTipoProducto.DataTextField = "DESCRIPCION";
                ddlTipoProducto.DataValueField = "ID_TIPO_PRODUCTO";
                ddlTipoProducto.DataBind();
                ddlTipoProducto.Items.Insert(0, new ListItem(String.Empty, String.Empty));
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

        private ReservaFiltro CargarReservaFiltro()
        {
            ReservaFiltro oReservaFiltro = null;

            oReservaFiltro = new ReservaFiltro();

            oReservaFiltro.COD_ESTADO = Convert.ToInt32(ddlEstado.SelectedValue);

            if (!String.IsNullOrEmpty(txtEdicion.Text))
                oReservaFiltro.EDICION = txtEdicion.Text;

            if (!String.IsNullOrEmpty(txtNombreProducto.Text))
                oReservaFiltro.NOMBRE_PRODUCTO = txtNombreProducto.Text;

            if (!String.IsNullOrEmpty(ddlTipoProducto.SelectedValue))
                oReservaFiltro.COD_TIPO_PRODUCTO = Convert.ToInt32(ddlTipoProducto.SelectedValue);

            if (!String.IsNullOrEmpty(txtNombreProducto.Text))
                oReservaFiltro.NOMBRE_PRODUCTO = txtNombreProducto.Text;

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
                    var lstReserva = new ReservaEdicionBLL().ObtenerReservaEdicion(oReservaFiltro);

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
            FormReservaEdicionAnular.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            FormReservaEdicionAnular.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            lsvReservas.Visible = false;
        }

        #endregion
    }
}