using BLL.Common;
using BLL.Filters;
using NLog;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class ConsultarStock : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            dvMensajeLsvProductos.Visible = false;

            if (!Page.IsPostBack)
                CargarTiposProducto();
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrillaProductos();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        #endregion

        #region Métodos Privados

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

        private ProductoFiltro CargarProductoFiltro()
        {
            var oProductoFiltro = new ProductoFiltro();

            if (!String.IsNullOrEmpty(txtCodigo.Text))
                oProductoFiltro.IdProducto = Convert.ToInt32(txtCodigo.Text);

            if (!String.IsNullOrEmpty(ddlTipoProducto.SelectedValue))
                oProductoFiltro.CodTipoProducto = Convert.ToInt32(ddlTipoProducto.SelectedValue);

            if (!String.IsNullOrEmpty(txtNombreProducto.Text))
                oProductoFiltro.NombreProducto = txtNombreProducto.Text;

            if (!String.IsNullOrEmpty(txtDescripcionProducto.Text))
                oProductoFiltro.DescripcionProducto = txtDescripcionProducto.Text;

            if (!String.IsNullOrEmpty(txtEdicion.Text))
                oProductoFiltro.NombreEdicion = txtEdicion.Text;

            if (!String.IsNullOrEmpty(txtDescripcionEdicion.Text))
                oProductoFiltro.DescripcionEdicion = txtDescripcionEdicion.Text;

            return oProductoFiltro;
        }

        private void CargarGrillaProductos()
        {
            try
            {
                var oProductoFiltro = CargarProductoFiltro();
                var lstProductos = new BLL.ProductoBLL().ObtenerProductosEdiciones(oProductoFiltro);

                if (lstProductos != null && lstProductos.Count > 0)
                    lsvProductos.DataSource = lstProductos;
                else
                {
                    dvMensajeLsvProductos.InnerHtml = MessageManager.Info(dvMensajeLsvProductos, Message.MsjeListadoProductoFiltrarTotalSinResultados, false);
                    dvMensajeLsvProductos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lsvProductos.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            lsvProductos.DataBind();
        }

        private void LimpiarCampos()
        {
            // Limpiar todos las cajas de texto.
            FormConsultarStock.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            // Limpiar todos los combos
            FormConsultarStock.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            lsvProductos.DataSource = null;
            lsvProductos.DataBind();
        }

        #endregion
    }
}