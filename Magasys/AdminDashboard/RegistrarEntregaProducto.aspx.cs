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
            if (!Page.IsPostBack)
            {
                CargarTiposDocumento();
                //CargarGrilla();
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
            //List<BLL.ReservaEdicionListado> lstReservas = new List<BLL.ReservaEdicionListado>();
            //lstReservas = (List <BLL.ReservaEdicionListado> )lsvReservas.DataSource;
            BLL.DAL.Venta oVenta = new BLL.DAL.Venta()
            {
                FECHA = DateTime.Now,
                //TOTAL = Convert.ToDouble(lblTotal.Text),
                COD_FORMA_PAGO = 2,
                COD_ESTADO = 4 // A Cuenta
            };
            foreach (var loItem in lsvReservas.Items)
            {
                if (((HtmlInputCheckBox)loItem.Controls[1]).Checked)
                {
                    try
                    {
                        BLL.DAL.ReservaEdicion oReservaEdicion = new BLL.ReservaEdicionBLL().ObtenerReservaEdicion(Convert.ToInt32(((Label)loItem.Controls[3]).Text));
                        BLL.DAL.Reserva oReserva = new BLL.ReservaBLL().ObtenerReserva(oReservaEdicion.COD_RESERVA);
                        // BLL.DAL.Cliente oCliente = new BLL.ClienteBLL().ObtenerCliente(oReserva.COD_CLIENTE);
                        if (oVenta.COD_CLIENTE == null)
                        {
                            oVenta.COD_CLIENTE = oReserva.COD_CLIENTE;
                        }

                        DetalleVenta oDetalleVenta = new DetalleVenta
                        {
                             CANTIDAD = 1,
                             COD_PRODUCTO_EDICION = oReservaEdicion.COD_PROD_EDICION,
                             PRECIO_UNIDAD = Convert.ToInt32(((Label)loItem.Controls[9]).Text),
                             SUBTOTAL = Convert.ToInt32(((Label)loItem.Controls[9]).Text)

                        };
                        lstDetalleVenta.Add(oDetalleVenta);

                    }
                    catch (Exception ex)
                    {

                    }


                }
            }

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
                ddlTipoDocumento.Items.Insert(0, new ListItem(String.Empty, String.Empty));
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

            if (!String.IsNullOrEmpty(txtNombre.Text))
                oClienteFiltro.Nombre = txtNombre.Text;

            if (!String.IsNullOrEmpty(txtApellido.Text))
                oClienteFiltro.Apellido = txtApellido.Text;

            if (!String.IsNullOrEmpty(txtAlias.Text))
                oClienteFiltro.Alias = txtAlias.Text;

            return oClienteFiltro;
        }

        private void CargarGrilla()
        {
            try
            {
                var oClienteFiltro = CargarClienteReservaFiltro();
                List<BLL.ReservaEdicionListado> lstReservaEdicion = new BLL.ReservaEdicionBLL().ObtenerReservaEdicionPorCliente(oClienteFiltro);
                lsvReservas.DataSource = lstReservaEdicion;
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
            lsvReservas.Visible = false;
        }

        #endregion        
    }
}