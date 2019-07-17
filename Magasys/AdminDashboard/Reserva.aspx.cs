using BLL;
using BLL.Common;
using BLL.Filters;
using NLog;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class Reserva : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                txtFechaReserva.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtCodigoReserva.Text = new ReservaBLL().ObtenerProximaReserva().ToString();
                rdbRetiraEnLocal.Checked = true;
                CargarTiposDocumento();
                CargarProveedores();
                CargarTiposProducto();
                MostrarOcultarDivsProductoListado();

                if (Session[Enums.Session.Cliente.ToString()] != null)
                {
                    var oCliente = (BLL.DAL.Cliente)Session[Enums.Session.Cliente.ToString()];
                    ddlTipoDocumento.SelectedValue = oCliente.TIPO_DOCUMENTO.ToString();
                    txtNroDocumento.Text = oCliente.NRO_DOCUMENTO.ToString();
                    txtApellido.Text = oCliente.APELLIDO;
                    txtNombre.Text = oCliente.NOMBRE;
                }

                Session.Remove(Enums.Session.AltaReservaAltaCliente.ToString());
            }
        }

        protected void BtnBuscarCliente_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNroDocumento.Text))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeClienteBuscarClienteSinNroDocumento));
                return;
            }
            var loCliente = new BLL.ClienteBLL().ObtenerCliente(Convert.ToInt32(ddlTipoDocumento.SelectedValue), Convert.ToInt32(txtNroDocumento.Text));

            if (loCliente == null)
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeClienteBuscarClienteSinResultados));
            else
            {
                txtApellido.Text = loCliente.APELLIDO;
                txtNombre.Text = loCliente.NOMBRE;
                Session.Add(Enums.Session.Cliente.ToString(), loCliente);
            }
        }

        protected void BtnNuevoCliente_Click(object sender, EventArgs e)
        {
            Session[Enums.Session.AltaReservaAltaCliente.ToString()] = "AltaReservaAltaCliente";
            Response.Redirect("Cliente.aspx", false);
        }

        protected void BtnBuscarProducto_Click(object sender, EventArgs e)
        {
            CargarGrillaProductos();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarSeleccionProductos();
        }

        protected void BtnGuardarReserva_Click(object sender, EventArgs e)
        {
            int lnumeroDocumento = 0;
            bool loResutado = false;
            var lCodigoProducto = hfCodigoProducto.Value;
            hfCodigoProducto.Value = String.Empty;

            if (!String.IsNullOrEmpty(txtFechaFin.Text) && Convert.ToDateTime(txtFechaFin.Text) < Convert.ToDateTime(txtFechaInicio.Text))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaFechaInicioMayorQueFechaFin)); //"La Fecha de fin debe ser mayor que la Fecha de inicio."
                return;
            }

            if (String.IsNullOrEmpty(lCodigoProducto))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaSeleccionDeProducto)); //"Debe seleccionar un producto."
                return;
            }

            if (String.IsNullOrEmpty(txtNroDocumento.Text))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeClienteObligatorio)); //"Debe completar el Cliente."
                return;
            }

            lnumeroDocumento = Convert.ToInt32(txtNroDocumento.Text);
            var loCliente = new ClienteBLL().ObtenerCliente(Convert.ToInt32(ddlTipoDocumento.SelectedValue), lnumeroDocumento);
            if (loCliente == null)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaValidaClienteCtaCorriente)); //"Los datos del cliente no son válidos"
                return;
            }
            else
            {
                if (!loCliente.NOMBRE.ToUpper().Equals(txtNombre.Text.ToUpper()) || !loCliente.APELLIDO.ToUpper().Equals(txtApellido.Text.ToUpper()))
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaValidaClienteCtaCorriente)); //"Los datos del cliente no son válidos"
                    return;
                }
            }

            var oCliente = (BLL.DAL.Cliente)Session[Enums.Session.Cliente.ToString()];

            if (rdbEnvioDomicilio.Checked == true && String.IsNullOrEmpty(oCliente.DIRECCION_MAPS))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaDireccionRequerida)); //"La forma de entrega “Envío a Domicilio” requiere que el cliente complete los datos de la dirección."
                return;
            }

            BLL.DAL.Reserva loReserva = new BLL.DAL.Reserva()
            {
                FECHA = DateTime.Now,
                COD_ESTADO = 7, //Confirmada
                COD_CLIENTE = oCliente.ID_CLIENTE,
                FECHA_INICIO = Convert.ToDateTime(txtFechaInicio.Text),
                COD_TIPO_RESERVA = 2, //Periódica
                COD_PRODUCTO = Convert.ToInt32(lCodigoProducto),
            };

            if (!String.IsNullOrEmpty(txtFechaFin.Text))
                loReserva.FECHA_FIN = Convert.ToDateTime(txtFechaFin.Text);

            if (rdbEnvioDomicilio.Checked == true)
                loReserva.ENVIO_DOMICILIO = "X"; // Se indica que la forma de entrega es “Envío a Domicilio”

            loResutado = new BLL.ReservaBLL().AltaReserva(loReserva);

            if (loResutado)
            {
                LimpiarPantalla();
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeReservaSuccessAlta, "Alta Reserva"));
            }
            else
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaFailure));
        }

        protected void BtnCancelarResera_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #endregion

        #region Métodos Privados

        private void OcultarDivsMensajes()
        {
            dvMensajeLsvProductos.Visible = false;
        }

        /// <summary>
        /// Muestra u oculta el div de productos según el parámetro que se le pase. Por defecto es falso. 
        /// </summary>
        /// <param name="pAcccion"></param>
        private void MostrarOcultarDivsProductoListado(bool pAcccion = false)
        {
            divProductoListado.Visible = pAcccion;
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

        private void CargarProveedores()
        {
            var oProveedor = new BLL.ProveedorBLL();

            try
            {
                ddlProveedor.DataSource = oProveedor.ObtenerProveedores();
                ddlProveedor.DataTextField = "RAZON_SOCIAL";
                ddlProveedor.DataValueField = "ID_PROVEEDOR";
                ddlProveedor.DataBind();
                ddlProveedor.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private ProductoFiltro CargarProductoFiltro()
        {
            var oProductoFiltro = new ProductoFiltro();

            if (!String.IsNullOrEmpty(ddlTipoProducto.SelectedValue))
                oProductoFiltro.CodTipoProducto = Convert.ToInt32(ddlTipoProducto.SelectedValue);

            if (!String.IsNullOrEmpty(ddlProveedor.SelectedValue))
                oProductoFiltro.CodProveedor = Convert.ToInt32(ddlProveedor.SelectedValue);

            if (!String.IsNullOrEmpty(txtNombreProducto.Text))
                oProductoFiltro.NombreProducto = txtNombreProducto.Text;

            if (!String.IsNullOrEmpty(txtDescripcionProducto.Text))
                oProductoFiltro.DescripcionProducto = txtDescripcionProducto.Text;

            return oProductoFiltro;
        }

        private void CargarGrillaProductos()
        {
            try
            {
                var oProductoFiltro = CargarProductoFiltro();
                var lstProductos = new BLL.ProductoBLL().ObtenerProductosPorTipoProducto(oProductoFiltro);

                if (lstProductos != null && lstProductos.Count > 0)
                {
                    lsvProductos.DataSource = lstProductos;
                    MostrarOcultarDivsProductoListado(true);
                }
                else
                {
                    dvMensajeLsvProductos.InnerHtml = MessageManager.Info(dvMensajeLsvProductos, Message.MsjeListadoProductoFiltrarTotalSinResultados, false);
                    dvMensajeLsvProductos.Visible = true;
                }

                lsvProductos.DataBind();
                lsvProductos.Visible = true;
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void LimpiarSeleccionProductos()
        {
            ddlProveedor.SelectedIndex = -1;
            ddlTipoProducto.SelectedIndex = -1;
            txtNombreProducto.Text = String.Empty;
            txtDescripcionProducto.Text = String.Empty;
            lsvProductos.Visible = false;
            MostrarOcultarDivsProductoListado();
        }

        private void LimpiarPantalla()
        {
            txtCodigoReserva.Text = new ReservaBLL().ObtenerProximaReserva().ToString();
            txtFechaInicio.Text = String.Empty;
            txtFechaFin.Text = String.Empty;
            rdbRetiraEnLocal.Checked = true;
            rdbEnvioDomicilio.Checked = false;
            ddlTipoDocumento.SelectedIndex = 0;
            txtNroDocumento.Text = String.Empty;
            txtNombre.Text = String.Empty;
            txtApellido.Text = String.Empty;
            LimpiarSeleccionProductos();
            Session.Remove(Enums.Session.Cliente.ToString());
        }

        #endregion        
    }
}