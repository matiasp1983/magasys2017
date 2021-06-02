using System;
using NLog;
using BLL.Filters;
using BLL;
using BLL.Common;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace PL.AdminDashboard
{
    public partial class ProductoDevolucion : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                CargarTiposProducto();
                CargarProveedores();
            }
        }

        protected void BtnBuscarProducto_Click(object sender, EventArgs e)
        {
            CargarGrillaDevoluciones();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarSeleccionProductos();
        }

        protected void BtnDevolucionesDiarias_Click(object sender, EventArgs e)
        {
            try
            {
                var lstDevouciones = new ProductoDevolucionBLL().ObtenerDevolucionesDiarias();
                if (lstDevouciones != null && lstDevouciones.Count > 0)
                {
                    lsvDevolucion.Visible = true;
                    lsvDevolucion.DataSource = lstDevouciones;
                }
                else
                {
                    dvMensajeLsvDevolucion.InnerHtml = MessageManager.Info(dvMensajeLsvDevolucion, Message.MsjeListadoDevolucionSinResultados, false);
                    dvMensajeLsvDevolucion.Visible = true;
                }

                lsvDevolucion.DataBind();
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void LsvDevolucion_ItemDataBound(object sender, ListViewItemEventArgs e)
        {  // El evento OnItemDataBound se llama cuando se setea la fuente de datos (DataSource) de la grilla

            DateTime loFechaDia = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));

            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return; //Controla que los registros a procesas correspondan al cuerpo de la grilla (ItemTemplate)

                var loCodProducto = ((Devolucion)e.Item.DataItem).COD_PRODUCTO.ToString();
                var loEdicion = ((Devolucion)e.Item.DataItem).EDICION.ToString();

                HtmlButton btnAgregar = ((HtmlButton)e.Item.FindControl("btnAgregar"));

                var loCodigo = String.Format("{0}-{1}", loCodProducto, loEdicion);

                btnAgregar.Attributes.Add("value", loCodigo);

                TextBox txtFechaDevolucion = ((TextBox)e.Item.FindControl("txtFechaDevolucion"));
                txtFechaDevolucion.Enabled = false;

                // Cuando la fecha propuesta sea menor a la fecha del día, se debe resaltar el campo en rojo.
                if (Convert.ToDateTime(((Devolucion)e.Item.DataItem).FECHA_DEVOLUCION) < loFechaDia)
                {
                    // Borde rojo para el campo FECHA_DEVOLUCION                    
                    txtFechaDevolucion.BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnAgregarItem_Click(object sender, EventArgs e)
        {
            try
            {
                var loProducto = ((HtmlButton)sender).Attributes["value"];

                var loProductoItem = loProducto.Split('-');
                var loCodigoProducto = loProductoItem[0];
                var loEdicion = loProductoItem[1];

                foreach (var loItem in lsvDevolucion.Items)
                {
                    if (((Label)loItem.Controls[7]).Text.ToString() == loCodigoProducto && ((Label)loItem.Controls[3]).Text.ToString() == loEdicion)
                    {
                        if (!String.IsNullOrEmpty(((TextBox)loItem.Controls[17]).Text.ToString()))
                        {
                            if (Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) > 0 && Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) <= Convert.ToInt32(((Label)loItem.Controls[13]).Text.ToString()) + Convert.ToInt32(((Label)loItem.Controls[15]).Text.ToString())) //"Cantidad" debe ser mayor a 0 y, menor o igual al "Sotck + Reservas Confirmadas".
                            {

                                var oProducto = new ProductoBLL().ObtenerProductoPorCodigo(Convert.ToInt32(((Label)loItem.Controls[7]).Text.ToString()));

                                DetalleDevolucion oDetalleDevolucion = new DetalleDevolucion
                                {
                                    COD_PRODUCTO = Convert.ToInt32(((Label)loItem.Controls[7]).Text.ToString()),
                                    COD_PRODUCTO_EDICION = Convert.ToInt32(((Label)loItem.Controls[21]).Text.ToString()),
                                    NOMBRE = ((Label)loItem.Controls[1]).Text.ToString(),
                                    TIPO_PRODUCTO = ((Label)loItem.Controls[5]).Text.ToString(),
                                    EDICION = ((Label)loItem.Controls[3]).Text.ToString(),
                                    STOCK = Convert.ToInt32(((Label)loItem.Controls[13]).Text.ToString()),
                                    CANTIDAD = Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()),
                                    CANTIDAD_RESERVAS = Convert.ToInt32(((Label)loItem.Controls[15]).Text.ToString())
                                };

                                if (oProducto.COD_TIPO_PRODUCTO == 1)
                                    oDetalleDevolucion.NOMBRE = oProducto.NOMBRE + " - " + oProducto.DESCRIPCION;

                                Session.Add(Enums.Session.DevolucionProducto.ToString(), oDetalleDevolucion);
                                Response.Redirect("RegistrarReservadasConfirmadas.aspx", false);
                            }
                            else
                            {
                                if (Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) == 0)
                                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaCampoCantidadObligatorio));
                                else if (Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) < 0)
                                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaCantidadInvalida));
                                else
                                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaStockInsuficiente));
                            }
                        }
                        else
                            Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaCampoCantidadObligatorio));

                        // Borde rojo para el campo CANTIDAD_DISPONIBLE
                        ((TextBox)loItem.Controls[17]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            bool loResutado = false;
            bool loHayReservaConfirmada = false;
            List<BLL.DAL.DetalleProductoDevolucion> lstDetalleProductoDevolucion = new List<BLL.DAL.DetalleProductoDevolucion>();


            BLL.DAL.ProductoDevolucion oProductoDevolucion = new BLL.DAL.ProductoDevolucion()
            {
                FECHA = DateTime.Now,
                COD_ESTADO = 1,
            };

            if (loResutado)
                loResutado = new ProductoDevolucionBLL().AltaDevolucion(oProductoDevolucion, lstDetalleProductoDevolucion);

            if (loResutado)
            {
                LimpiarPantalla();
                if (loHayReservaConfirmada) // Redireccionar a la nueva pantalla: RegistrarReservadasConfirmadas.aspx
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeDevolucionSuccessAlta, "", "RegistrarReservadasConfirmadas.aspx"));
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeDevolucionSuccessAlta));
            }
            else
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeDevolucionFailure));
        }

        #endregion

        #region Métodos Privados

        private void OcultarDivsMensajes()
        {
            dvMensajeLsvDevolucion.Visible = false;
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

            if (!String.IsNullOrEmpty(txtEdicion.Text))
                oProductoFiltro.NombreEdicion = txtEdicion.Text;

            if (!String.IsNullOrEmpty(txtDescripcionEdicion.Text))
                oProductoFiltro.DescripcionEdicion = txtDescripcionEdicion.Text;

            return oProductoFiltro;
        }

        private void CargarGrillaDevoluciones()
        {
            try
            {
                var oProductoFiltro = CargarProductoFiltro();

                var lstDevouciones = new ProductoDevolucionBLL().ObtenerProductos(oProductoFiltro);
                if (lstDevouciones != null && lstDevouciones.Count > 0)
                {
                    lsvDevolucion.Visible = true;
                    lsvDevolucion.DataSource = lstDevouciones;
                }
                else
                {
                    dvMensajeLsvDevolucion.InnerHtml = MessageManager.Info(dvMensajeLsvDevolucion, Message.MsjeListadoDevolucionSinResultados, false);
                    dvMensajeLsvDevolucion.Visible = true;
                }

                lsvDevolucion.DataBind();
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
            txtEdicion.Text = String.Empty;
            txtDescripcionEdicion.Text = String.Empty;
            txtNombreProducto.Text = String.Empty;
            txtDescripcionProducto.Text = String.Empty;
            lsvDevolucion.Visible = false;
        }

        private void LimpiarPantalla()
        {
            LimpiarSeleccionProductos();
        }

        #endregion
    }
}