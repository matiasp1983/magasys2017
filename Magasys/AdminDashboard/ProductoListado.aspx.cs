using System;
using System.Web.UI.WebControls;
using BLL.Common;
using BLL.Filters;
using System.Web.UI.HtmlControls;
using NLog;
using System.Linq;

namespace PL.AdminDashboard
{
    public partial class ProductoListado : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {                
                CargarTiposProducto();
                CargarEstados();
                CargarProveedores();
                CargarGrillaProductos();
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrillaProductos();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void LsvProductos_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loIdProducto = ((BLL.ProductoListado)e.Item.DataItem).ID_PRODUCTO.ToString();
                var loDescTipoProducto = ((BLL.ProductoListado)e.Item.DataItem).DESC_TIPO_PRODUCTO.ToString();

                HtmlButton btnVisualizar = ((HtmlButton)e.Item.FindControl("btnVisualizar"));
                btnVisualizar.Attributes.Add("value", string.Format("{0},{1}", loIdProducto, loDescTipoProducto));

                HtmlButton btnModificar = ((HtmlButton)e.Item.FindControl("btnModificar"));
                btnModificar.Attributes.Add("value", string.Format("{0},{1}", loIdProducto, loDescTipoProducto));

                HiddenField hdIdCuitProveedorBaja = ((HiddenField)e.Item.FindControl("hdIdCuitProveedorBaja"));
                hdIdCuitProveedorBaja.Value = loIdProducto.ToString();
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Producto.aspx", false);
        }

        protected void BtnVisualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var loIdProducto = (((HtmlButton)sender).Attributes["value"]).ToString().Split(',')[0];
                var loDescTipoProducto = (((HtmlButton)sender).Attributes["value"]).ToString().Split(',')[1];

                switch (loDescTipoProducto)
                {
                    case "Revista":
                        Session[Enums.Session.ProductoRevista.ToString()] = new BLL.RevistaBLL().ObtenerRevista(Convert.ToInt64(loIdProducto));
                        Response.Redirect("ProductoRevistaVisualizar.aspx", false);
                        break;
                    case "Colección":
                        Session[Enums.Session.ProductoColeccion.ToString()] = new BLL.ColeccionBLL().ObtenerColeccion(Convert.ToInt64(loIdProducto));
                        Response.Redirect("ProductoColeccionVisualizar.aspx", false);
                        break;
                    case "Libro":
                        Session[Enums.Session.ProductoLibro.ToString()] = new BLL.LibroBLL().ObtenerLibro(Convert.ToInt64(loIdProducto));
                        Response.Redirect("ProductoLibroVisualizar.aspx", false);
                        break;
                    case "Suplemento":
                        Session[Enums.Session.ProductoSuplemento.ToString()] = new BLL.SuplementoBLL().ObtenerSuplemento(Convert.ToInt64(loIdProducto));
                        Response.Redirect("ProductoSuplementoVisualizar.aspx", false);
                        break;
                    case "Película":
                        Session[Enums.Session.ProductoPelicula.ToString()] = new BLL.PeliculaBLL().ObtenerPelicula(Convert.ToInt64(loIdProducto));
                        Response.Redirect("ProductoPeliculaVisualizar.aspx", false);
                        break;
                    default:
                        Session[Enums.Session.ProductoDiario.ToString()] = new BLL.DiarioBLL().ObtenerDiario(Convert.ToInt64(loIdProducto));
                        Response.Redirect("ProductoDiarioVisualizar.aspx", false);
                        break;
                }
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
                var loIdProducto = (((HtmlButton)sender).Attributes["value"]).ToString().Split(',')[0];
                var loDescTipoProducto = (((HtmlButton)sender).Attributes["value"]).ToString().Split(',')[1];

                switch (loDescTipoProducto)
                {
                    case "Revista":
                        Session[Enums.Session.ProductoRevista.ToString()] = new BLL.RevistaBLL().ObtenerRevista(Convert.ToInt64(loIdProducto));
                        Response.Redirect("ProductoRevistaEditar.aspx", false);
                        break;
                    case "Colección":
                        Session[Enums.Session.ProductoColeccion.ToString()] = new BLL.ColeccionBLL().ObtenerColeccion(Convert.ToInt64(loIdProducto));
                        Response.Redirect("ProductoColeccionEditar.aspx", false);
                        break;
                    case "Libro":
                        Session[Enums.Session.ProductoLibro.ToString()] = new BLL.LibroBLL().ObtenerLibro(Convert.ToInt64(loIdProducto));
                        Response.Redirect("ProductoLibroEditar.aspx", false);
                        break;
                    case "Suplemento":
                        Session[Enums.Session.ProductoSuplemento.ToString()] = new BLL.SuplementoBLL().ObtenerSuplemento(Convert.ToInt64(loIdProducto));
                        Response.Redirect("ProductoSuplementoEditar.aspx", false);
                        break;
                    case "Película":
                        Session[Enums.Session.ProductoPelicula.ToString()] = new BLL.PeliculaBLL().ObtenerPelicula(Convert.ToInt64(loIdProducto));
                        Response.Redirect("ProductoPeliculaEditar.aspx", false);
                        break;
                    default:
                        Session[Enums.Session.ProductoDiario.ToString()] = new BLL.DiarioBLL().ObtenerDiario(Convert.ToInt64(loIdProducto));
                        Response.Redirect("ProductoDiarioEditar.aspx", false);
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnBaja_Click(object sender, EventArgs e)
        {
            // SE ESPERAN DEFINICIONES DE LA FUNCIONALIDAD.
        }

        #endregion

        #region Métodos Privados

        private void OcultarDivsMensajes()
        {
            dvMensajeLsvProductos.Visible = false;
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

        private void CargarEstados()
        {
            var oEstado = new BLL.EstadoBLL();

            try
            {
                ddlEstado.DataSource = oEstado.ObtenerEstados();
                ddlEstado.DataTextField = "NOMBRE";
                ddlEstado.DataValueField = "ID_ESTADO";
                ddlEstado.DataBind();
                ddlEstado.Items.Insert(0, new ListItem(String.Empty, String.Empty));
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

            if (!String.IsNullOrEmpty(txtCodigo.Text))
                oProductoFiltro.IdProducto = Convert.ToInt32(txtCodigo.Text);

            if (!String.IsNullOrEmpty(txtNombre.Text))
                oProductoFiltro.NombreProducto = txtNombre.Text;

            if (!String.IsNullOrEmpty(ddlTipoProducto.SelectedValue))
                oProductoFiltro.CodTipoProducto = Convert.ToInt32(ddlTipoProducto.SelectedValue);

            if (!String.IsNullOrEmpty(ddlEstado.SelectedValue))
                oProductoFiltro.CodEstado = Convert.ToInt32(ddlEstado.SelectedValue);

            if (!String.IsNullOrEmpty(ddlProveedor.SelectedValue))
                oProductoFiltro.CodProveedor = Convert.ToInt32(ddlProveedor.SelectedValue);

            return oProductoFiltro;
        }

        private void CargarGrillaProductos()
        {
            try
            {
                var oProductoFiltro = CargarProductoFiltro();
                var lstProductos = new BLL.ProductoBLL().ObtenerProductos(oProductoFiltro);

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
            FormProductoListado.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            // Limpiar todos los combos
            FormProductoListado.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            //Session.Remove(Enums.Session.Proveedor.ToString());   VER cómo tratamos las sesiones de los distintos productos .. ver si acá estamos manejando sesiones...
            CargarGrillaProductos();
        }

        #endregion
    }
}