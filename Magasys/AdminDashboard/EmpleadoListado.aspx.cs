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
    public partial class EmpleadoListado : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            dvMensajeLsvEmpleados.Visible = false;
            if (!Page.IsPostBack)
            {
                CargarTiposDocumento();
                CargarGrillaEmpleados();
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            lsvEmpleados.DataSource = null;
            lsvEmpleados.DataBind();
            CargarGrillaEmpleados();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Empleado.aspx", false);
        }

        protected void LsvEmpleados_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loIdEmpleado = ((BLL.EmpleadoListado)e.Item.DataItem).ID_EMPLEADO.ToString();

                HtmlButton btnVisualizar = ((HtmlButton)e.Item.FindControl("btnVisualizar"));
                btnVisualizar.Attributes.Add("value", loIdEmpleado);

                HtmlButton btnModificar = ((HtmlButton)e.Item.FindControl("btnModificar"));
                btnModificar.Attributes.Add("value", loIdEmpleado);
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
                var oEmpleado = Convert.ToInt32(((HtmlButton)sender).Attributes["value"]);
                Session.Add(Enums.Session.IdEmpleado.ToString(), oEmpleado);
                Response.Redirect("EmpleadoVisualizar.aspx", false);
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
                var oEmpleado = Convert.ToInt32(((HtmlButton)sender).Attributes["value"]);
                Session.Add(Enums.Session.IdEmpleado.ToString(), oEmpleado);
                Response.Redirect("EmpleadoEditar.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
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

        private EmpleadoFiltro CargarEmpleadoFiltro()
        {
            EmpleadoFiltro oEmpleadoFiltro = new EmpleadoFiltro();

            if (!String.IsNullOrEmpty(ddlTipoDocumento.SelectedValue) && !String.IsNullOrEmpty(txtNroDocumento.Text))
            {
                oEmpleadoFiltro.Tipo_documento = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
                oEmpleadoFiltro.Nro_documento = Convert.ToInt32(txtNroDocumento.Text);
            }

            if (!String.IsNullOrEmpty(txtApellido.Text))
                oEmpleadoFiltro.Apellido = txtApellido.Text;

            if (!String.IsNullOrEmpty(txtNombre.Text))
                oEmpleadoFiltro.Nombre = txtNombre.Text;

            return oEmpleadoFiltro;
        }

        private void CargarGrillaEmpleados()
        {
            try
            {
                var oEmpleadoFiltro = CargarEmpleadoFiltro();

                if (oEmpleadoFiltro != null)
                {
                    var lstEmpleado = new BLL.EmpleadoBLL().ObtenerEmpleados(oEmpleadoFiltro);

                    if (lstEmpleado != null && lstEmpleado.Count > 0)
                        lsvEmpleados.DataSource = lstEmpleado;
                    else
                    {
                        dvMensajeLsvEmpleados.InnerHtml = MessageManager.Info(dvMensajeLsvEmpleados, Message.MsjeListadoEmpleadoFiltrarTotalSinResultados, false);
                        dvMensajeLsvEmpleados.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lsvEmpleados.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            lsvEmpleados.DataBind();
        }

        private void LimpiarCampos()
        {
            FormEmpleadoListado.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            FormEmpleadoListado.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            CargarGrillaEmpleados();
        }

        #endregion
    }
}