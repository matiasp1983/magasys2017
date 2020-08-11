using BLL;
using BLL.Common;
using BLL.DAL;
using BLL.Filters;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class RegistrarEntregaProducto : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            divMensajeEntregaProducto.Visible = false; // Oculto el mensaje

            if (!Page.IsPostBack)
            {
                CargarTiposDocumento();
                lblTotal.Text = "0";
                divEntregaProducto.Visible = false;
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
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnGuardarEntrega_Click(object sender, EventArgs e)
        {
            bool loResutado = false;
            List<DetalleVenta> lstDetalleVenta = new List<DetalleVenta>();
            BLL.DAL.Venta oVenta = new BLL.DAL.Venta()
            {
                FECHA = DateTime.Now,
                TOTAL = 0,
                COD_FORMA_PAGO = 2,
                COD_ESTADO = 4 // A Cuenta
            };
            foreach (var loItem in lsvReservas.Items)
            {
                if (((HtmlInputCheckBox)loItem.Controls[1]).Checked)
                {
                    try
                    {
                        BLL.DAL.ReservaEdicion oReservaEdicion = new BLL.ReservaEdicionBLL().ObtenerReservaEdicion(Convert.ToInt32(((Label)loItem.Controls[13]).Text));
                        BLL.DAL.Reserva oReserva = new BLL.ReservaBLL().ObtenerReserva(oReservaEdicion.COD_RESERVA);
                        if (oVenta.COD_CLIENTE == null)
                        {
                            oVenta.COD_CLIENTE = oReserva.COD_CLIENTE;
                        }

                        DetalleVenta oDetalleVenta = new DetalleVenta
                        {
                            CANTIDAD = 1,
                            COD_PRODUCTO_EDICION = oReservaEdicion.COD_PROD_EDICION,
                            PRECIO_UNIDAD = Convert.ToInt32(((Label)loItem.Controls[11]).Text),
                            SUBTOTAL = Convert.ToInt32(((Label)loItem.Controls[11]).Text)
                        };

                        lstDetalleVenta.Add(oDetalleVenta);
                        oVenta.TOTAL += oDetalleVenta.SUBTOTAL;

                        // Actualizar Stock
                        loResutado = new ProductoEdicionBLL().ActualizarCantidadDisponible(oDetalleVenta.COD_PRODUCTO_EDICION, oDetalleVenta.CANTIDAD);

                        // Actualizar Estado de Reserva Edicion
                        oReservaEdicion.COD_ESTADO = 11; //Entregada 
                        loResutado = new ReservaEdicionBLL().ModificarReservaEdidion(oReservaEdicion);

                        // Si es Reservar Unica hay que cambiar el estado a Finalizado
                        if (oReserva.COD_TIPO_RESERVA == 1)
                        {
                            oReserva.COD_ESTADO = 8; //Finalizada
                            loResutado = new ReservaBLL().ModificarReserva(oReserva);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            // Registrar la Venta

            if (loResutado)
                loResutado = new VentaBLL().AltaVenta(oVenta, lstDetalleVenta);

            if (loResutado)
            {
                LimpiarCampos();
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeEntregalSuccess, "Entrega Registrada"));
            }
            else
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeEntregaFailure));
        }

        protected void BtnCancelarEntrega_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Métodos Privados        

        private void CargarTiposDocumento()
        {
            var oTipoDocumento = new BLL.TipoDocumentoBLL();

            try
            {
                ddlTipoDocumento.DataSource = oTipoDocumento.ObtenerTiposDocumento();
                ddlTipoDocumento.DataTextField = "DESCRIPCION";
                ddlTipoDocumento.DataValueField = "ID_TIPO_DOCUMENTO";
                ddlTipoDocumento.DataBind();
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private ClienteFiltro CargarClienteReservaFiltro()
        {
            ClienteFiltro oClienteFiltro = null;

            oClienteFiltro = new ClienteFiltro();

            if (!String.IsNullOrEmpty(ddlTipoDocumento.SelectedValue) && !String.IsNullOrEmpty(txtNroDocumento.Text))
            {
                oClienteFiltro.Tipo_documento = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
                oClienteFiltro.Nro_documento = Convert.ToInt32(txtNroDocumento.Text);
            }

            return oClienteFiltro;
        }

        private void CargarGrilla()
        {
            try
            {
                var loCliente = new ClienteBLL().ObtenerCliente(Convert.ToInt32(ddlTipoDocumento.SelectedValue), Convert.ToInt32(txtNroDocumento.Text));

                if (loCliente != null)
                {
                    txtNombre.Text = loCliente.NOMBRE;
                    txtApellido.Text = loCliente.APELLIDO;

                    ClienteFiltro oClienteFiltro = new ClienteFiltro();
                    oClienteFiltro.Id_cliente = loCliente.ID_CLIENTE;

                    List<ReservaEdicionListado> lstReservaEdicion = new ReservaEdicionBLL().ObtenerReservaEdicionPorCliente(oClienteFiltro);

                    if (lstReservaEdicion != null && lstReservaEdicion.Count > 0)
                    {
                        lsvReservas.DataSource = lstReservaEdicion;
                        divEntregaProducto.Visible = true;
                        divMensajeEntregaProducto.Visible = false;
                    }
                    else
                    {
                        dvMensajeLsvEntregaProducto.InnerHtml = MessageManager.Info(dvMensajeLsvEntregaProducto, Message.MsjeEntregaProductoFiltroSinResultados, false);
                        divMensajeEntregaProducto.Visible = true;
                        divEntregaProducto.Visible = false;
                    }
                }
                else
                {
                    dvMensajeLsvEntregaProducto.InnerHtml = MessageManager.Info(dvMensajeLsvEntregaProducto, Message.MsjeEntregaProductoFiltroSinResultados, false);
                    divMensajeEntregaProducto.Visible = true;
                    divEntregaProducto.Visible = false;
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
            FormRegistrarEntregaProducto.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            FormRegistrarEntregaProducto.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            divEntregaProducto.Visible = false;
        }

        #endregion        
    }
}