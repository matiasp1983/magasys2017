using BLL.Common;
using BLL.Filters;
using NLog;
using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class ClienteListado : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                CargarTiposDocumento();
                CargarGrillaClientes();
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            lsvClientes.DataSource = null;
            lsvClientes.DataBind();
            CargarGrillaClientes();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cliente.aspx", false);
        }

        protected void LsvClientes_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loIdCliente = ((BLL.DAL.Cliente)e.Item.DataItem).ID_CLIENTE.ToString();

                HtmlButton btnVisualizar = ((HtmlButton)e.Item.FindControl("btnVisualizar"));
                btnVisualizar.Attributes.Add("value", loIdCliente);

                HtmlButton btnModificar = ((HtmlButton)e.Item.FindControl("btnModificar"));
                btnModificar.Attributes.Add("value", loIdCliente);
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
                Session.Add(Enums.Session.IdCliente.ToString(), Convert.ToInt64(((HtmlButton)sender).Attributes["value"]));
                //Response.Redirect("DetalleProductoIngresos.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Add(Enums.Session.IdCliente.ToString(), Convert.ToInt64(((HtmlButton)sender).Attributes["value"]));
                //Response.Redirect("DetalleProductoIngresosEditar.aspx", false);
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
            dvMensajeLsvClientes.Visible = false;
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

        private ClienteFiltro CargarClienteFiltro()
        {
            ClienteFiltro oClienteFiltro = new ClienteFiltro();

            if (!String.IsNullOrEmpty(txtAlias.Text))
                oClienteFiltro.Alias = txtAlias.Text;

            if (!String.IsNullOrEmpty(txtCodigo.Text))
                oClienteFiltro.Id_cliente = Convert.ToInt32(txtCodigo.Text);

            if (!String.IsNullOrEmpty(ddlTipoDocumento.SelectedValue) && !String.IsNullOrEmpty(txtNroDocumento.Text))
            {
                oClienteFiltro.Tipo_documento = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
                oClienteFiltro.Nro_documento = Convert.ToInt32(txtNroDocumento.Text);
            }

            if (!String.IsNullOrEmpty(txtApellido.Text))
                oClienteFiltro.Apellido = txtApellido.Text;

            if (!String.IsNullOrEmpty(txtNombre.Text))
                oClienteFiltro.Nombre = txtNombre.Text;

            return oClienteFiltro;
        }

        private void CargarGrillaClientes()
        {
            try
            {
                var oClienteFiltro = CargarClienteFiltro();

                if (oClienteFiltro != null)
                {
                    var lstCliente = new BLL.ClienteBLL().ObtenerClientes(oClienteFiltro);

                    if (lstCliente != null && lstCliente.Count > 0)
                        lsvClientes.DataSource = lstCliente;
                    else
                    {
                        dvMensajeLsvClientes.InnerHtml = MessageManager.Info(dvMensajeLsvClientes, Message.MsjeListadoClienteFiltrarTotalSinResultados, false);
                        dvMensajeLsvClientes.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lsvClientes.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            lsvClientes.DataBind();
        }

        private void LimpiarCampos()
        {
            FormClienteListado.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            FormClienteListado.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            lsvClientes.Visible = false;
            CargarGrillaClientes();
        }

        #endregion

    }
}