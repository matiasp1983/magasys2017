using BLL.Common;
using BLL.Filters;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class Cobro : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                CargarTiposDocumento();
                MostrarOcultarDivCliente();
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrillaClientes();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            FormCobro.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            FormCobro.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            MostrarOcultarDivCliente();
        }

        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                var lCodigoCliente = hfCodigoCliente.Value;
                if (!String.IsNullOrEmpty(lCodigoCliente))
                {
                    Session.Add(Enums.Session.IdClienteCobro.ToString(), lCodigoCliente);
                    Response.Redirect("CobroCliente.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        #endregion

        #region Métodos Privados

        private void OcultarDivsMensajes(bool pAcccion = false)
        {
            dvMensajeLsvClientes.Visible = pAcccion;
            if (pAcccion == true)
                btnContinuar.Visible = false;
            else
                btnContinuar.Visible = true;
        }

        private void MostrarOcultarDivCliente(bool pAcccion = false)
        {
            divClienteListado.Visible = pAcccion;
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

            if (!String.IsNullOrEmpty(ddlTipoDocumento.SelectedValue) && !String.IsNullOrEmpty(txtNroDocumento.Text))
            {
                oClienteFiltro.Tipo_documento = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
                oClienteFiltro.Nro_documento = Convert.ToInt32(txtNroDocumento.Text);
            }

            if (!String.IsNullOrEmpty(txtApellido.Text))
                oClienteFiltro.Apellido = txtApellido.Text;

            if (!String.IsNullOrEmpty(txtNombre.Text))
                oClienteFiltro.Nombre = txtNombre.Text;

            if (!String.IsNullOrEmpty(txtAlias.Text))
                oClienteFiltro.Alias = txtAlias.Text;

            return oClienteFiltro;
        }

        private void CargarGrillaClientes()
        {
            try
            {
                var oClienteFiltro = CargarClienteFiltro();

                if (oClienteFiltro != null)
                {
                    var lstCliente = new BLL.ClienteBLL().ObtenerClientesGrilla(oClienteFiltro);

                    if (lstCliente != null && lstCliente.Count > 0)
                    {
                        lsvClientes.DataSource = lstCliente;
                        OcultarDivsMensajes();
                    }
                    else
                    {
                        dvMensajeLsvClientes.InnerHtml = MessageManager.Info(dvMensajeLsvClientes, Message.MsjeListadoClienteFiltrarTotalSinResultados, false);
                        OcultarDivsMensajes(true);
                    }
                }
                lsvClientes.DataBind();
                MostrarOcultarDivCliente(true);
            }
            catch (Exception ex)
            {
                lsvClientes.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        #endregion
    }
}