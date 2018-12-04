using System;
using NLog;
using BLL.Common;
using BLL.Filters;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BLL;
using BLL.DAL;
using System.Collections.Generic;
using System.Linq;

namespace PL.AdminDashboard
{
    public partial class Venta : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                txtFechaVenta.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtCodigoVenta.Text = new BLL.VentaBLL().ObtenerUltimaVenta().ToString();
                rdbPagadoNo.Checked = true;
                CargarFormaDePago();
                CargarTiposDocumento();
                if (Session[Enums.Session.Cliente.ToString()] != null)
                {
                    var oCliente = (BLL.DAL.Cliente)Session[Enums.Session.Cliente.ToString()];
                    txtApellido.Text = oCliente.APELLIDO;
                    txtNombre.Text = oCliente.NOMBRE;
                    Session.Remove(Enums.Session.Cliente.ToString());
                }

                CargarTiposProducto();
                CargarProveedores();
                MostrarOcultarDivsVentas();
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
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeClienteBuscarClienteSinResultados));
            else
            {
                txtApellido.Text = loCliente.APELLIDO;
                txtNombre.Text = loCliente.NOMBRE;
            }
        }

        protected void BtnBuscarProducto_Click(object sender, EventArgs e)
        {
            if (lsvVenta.Items.Count == 0)
            {
                lsvVenta.DataSource = null;
                lsvVenta.DataBind();
            }

            lsvDiarios.DataSource = lsvRevistas.DataSource = lsvColecciones.DataSource = lsvLibros.DataSource = lsvSuplementos.DataSource = lsvPeliculas.DataSource = null;

            lsvDiarios.DataBind();
            lsvRevistas.DataBind();
            lsvColecciones.DataBind();
            lsvLibros.DataBind();
            lsvSuplementos.DataBind();
            lsvPeliculas.DataBind();

            CargarGrillaProductos();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarSeleccionProductos();
        }

        protected void BtnNuevoCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cliente.aspx", false);
        }

        protected void BtnGuardarVenta_Click(object sender, EventArgs e)
        {
            bool loResutado = false;
            List<DetalleVenta> lstDetalleVenta = new List<DetalleVenta>();

            if (rdbPagadoSi.Checked && ddlFormaPago.SelectedValue == "1") //"Contado"
            {
                BLL.DAL.Venta oVenta = new BLL.DAL.Venta()
                {
                    FECHA = DateTime.Now,
                    COD_ESTADO = 1,
                    TOTAL = Convert.ToDouble(lblTotal.Text),
                    COD_FORMA_PAGO = Convert.ToInt32(ddlFormaPago.SelectedValue)
                    //COD_CLIENTE    Próxima iteración                    
                };

                foreach (var loItem in lsvVenta.Items)
                {
                    DetalleVenta oDetalleVenta = new DetalleVenta
                    {
                        COD_PRODUCTO_EDICION = Convert.ToInt32(((Label)loItem.Controls[17]).Text.ToString()),
                        PRECIO_UNIDAD = Convert.ToInt32(((Label)loItem.Controls[9]).Text.ToString()),
                        CANTIDAD = Convert.ToInt32(((Label)loItem.Controls[11]).Text.ToString()),
                        SUBTOTAL = Convert.ToInt32(((Label)loItem.Controls[13]).Text.ToString())
                    };

                    lstDetalleVenta.Add(oDetalleVenta);

                    // Actualizar Stock
                    loResutado = new ProductoEdicionBLL().ActualizarCantidadDisponible(oDetalleVenta.COD_PRODUCTO_EDICION, oDetalleVenta.CANTIDAD);
                }

                if (loResutado)
                    loResutado = new VentaBLL().AltaVenta(oVenta, lstDetalleVenta);

                if (loResutado)
                {
                    LimpiarPantalla();
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeVentaSuccessAlta));
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaFailure));
            }
            else
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaAviso));
        }

        protected void BtnCancelarVenta_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        protected void LsvDiarios_ItemDataBound(object sender, ListViewItemEventArgs e)
        {  // El evento OnItemDataBound se llama cuando se setea la fuente de datos (DataSource) de la grilla
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return; //Controla que los registros a procesas correspondan al cuerpo de la grilla (ItemTemplate)

                var loCodProducto = ((BLL.DiarioEdicion)e.Item.DataItem).COD_PRODUCTO.ToString();
                var loEdicion = ((BLL.DiarioEdicion)e.Item.DataItem).EDICION.ToString();

                HtmlButton btnAgregar = ((HtmlButton)e.Item.FindControl("btnAgregar"));

                var loCodigo = String.Format("{0}-{1}", loCodProducto, loEdicion);

                btnAgregar.Attributes.Add("value", loCodigo);

                if (((BLL.DiarioEdicion)e.Item.DataItem).CANTIDAD_DISPONIBLE <= 0)
                {
                    btnAgregar.Disabled = true;
                    btnAgregar.Style.Add("color", "#fff");
                    TextBox txtCantidad = ((TextBox)e.Item.FindControl("txtCantidad"));
                    txtCantidad.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void LsvRevistas_ItemDataBound(object sender, ListViewItemEventArgs e)
        {  // El evento OnItemDataBound se llama cuando se setea la fuente de datos (DataSource) de la grilla
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return; //Controla que los registros a procesas correspondan al cuerpo de la grilla (ItemTemplate)

                var loCodProducto = ((BLL.RevistaEdicion)e.Item.DataItem).COD_PRODUCTO.ToString();
                var loEdicion = ((BLL.RevistaEdicion)e.Item.DataItem).EDICION.ToString();

                HtmlButton btnAgregar = ((HtmlButton)e.Item.FindControl("btnAgregar"));

                var loCodigo = String.Format("{0}-{1}", loCodProducto, loEdicion);

                btnAgregar.Attributes.Add("value", loCodigo);

                if (((BLL.RevistaEdicion)e.Item.DataItem).CANTIDAD_DISPONIBLE <= 0)
                {
                    btnAgregar.Disabled = true;
                    btnAgregar.Style.Add("color", "#fff");
                    TextBox txtCantidad = ((TextBox)e.Item.FindControl("txtCantidad"));
                    txtCantidad.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void LsvColecciones_ItemDataBound(object sender, ListViewItemEventArgs e)
        {  // El evento OnItemDataBound se llama cuando se setea la fuente de datos (DataSource) de la grilla
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return; //Controla que los registros a procesas correspondan al cuerpo de la grilla (ItemTemplate)

                var loCodProducto = ((BLL.ColeccionEdicion)e.Item.DataItem).COD_PRODUCTO.ToString();
                var loEdicion = ((BLL.ColeccionEdicion)e.Item.DataItem).EDICION.ToString();

                HtmlButton btnAgregar = ((HtmlButton)e.Item.FindControl("btnAgregar"));

                var loCodigo = String.Format("{0}-{1}", loCodProducto, loEdicion);

                btnAgregar.Attributes.Add("value", loCodigo);

                if (((BLL.ColeccionEdicion)e.Item.DataItem).CANTIDAD_DISPONIBLE <= 0)
                {
                    btnAgregar.Disabled = true;
                    btnAgregar.Style.Add("color", "#fff");
                    TextBox txtCantidad = ((TextBox)e.Item.FindControl("txtCantidad"));
                    txtCantidad.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void LsvLibros_ItemDataBound(object sender, ListViewItemEventArgs e)
        {  // El evento OnItemDataBound se llama cuando se setea la fuente de datos (DataSource) de la grilla
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return; //Controla que los registros a procesas correspondan al cuerpo de la grilla (ItemTemplate)

                var loCodProducto = ((BLL.LibroEdicion)e.Item.DataItem).COD_PRODUCTO.ToString();
                var loEdicion = ((BLL.LibroEdicion)e.Item.DataItem).EDICION.ToString();

                HtmlButton btnAgregar = ((HtmlButton)e.Item.FindControl("btnAgregar"));

                var loCodigo = String.Format("{0}-{1}", loCodProducto, loEdicion);

                btnAgregar.Attributes.Add("value", loCodigo);

                if (((BLL.LibroEdicion)e.Item.DataItem).CANTIDAD_DISPONIBLE <= 0)
                {
                    btnAgregar.Disabled = true;
                    btnAgregar.Style.Add("color", "#fff");
                    TextBox txtCantidad = ((TextBox)e.Item.FindControl("txtCantidad"));
                    txtCantidad.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void LsvSuplementos_ItemDataBound(object sender, ListViewItemEventArgs e)
        {  // El evento OnItemDataBound se llama cuando se setea la fuente de datos (DataSource) de la grilla
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return; //Controla que los registros a procesas correspondan al cuerpo de la grilla (ItemTemplate)

                var loCodProducto = ((BLL.SuplementoEdicion)e.Item.DataItem).COD_PRODUCTO.ToString();
                var loEdicion = ((BLL.SuplementoEdicion)e.Item.DataItem).EDICION.ToString();

                HtmlButton btnAgregar = ((HtmlButton)e.Item.FindControl("btnAgregar"));

                var loCodigo = String.Format("{0}-{1}", loCodProducto, loEdicion);

                btnAgregar.Attributes.Add("value", loCodigo);

                if (((BLL.SuplementoEdicion)e.Item.DataItem).CANTIDAD_DISPONIBLE <= 0)
                {
                    btnAgregar.Disabled = true;
                    btnAgregar.Style.Add("color", "#fff");
                    TextBox txtCantidad = ((TextBox)e.Item.FindControl("txtCantidad"));
                    txtCantidad.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void LsvPeliculas_ItemDataBound(object sender, ListViewItemEventArgs e)
        {  // El evento OnItemDataBound se llama cuando se setea la fuente de datos (DataSource) de la grilla
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return; //Controla que los registros a procesas correspondan al cuerpo de la grilla (ItemTemplate)

                var loCodProducto = ((BLL.PeliculaEdicion)e.Item.DataItem).COD_PRODUCTO.ToString();
                var loEdicion = ((BLL.PeliculaEdicion)e.Item.DataItem).EDICION.ToString();

                HtmlButton btnAgregar = ((HtmlButton)e.Item.FindControl("btnAgregar"));

                var loCodigo = String.Format("{0}-{1}", loCodProducto, loEdicion);

                btnAgregar.Attributes.Add("value", loCodigo);

                if (((BLL.PeliculaEdicion)e.Item.DataItem).CANTIDAD_DISPONIBLE <= 0)
                {
                    btnAgregar.Disabled = true;
                    btnAgregar.Style.Add("color", "#fff");
                    TextBox txtCantidad = ((TextBox)e.Item.FindControl("txtCantidad"));
                    txtCantidad.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void LsvVenta_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return; //Controla que los registros a procesas correspondan al cuerpo de la grilla (ItemTemplate)

                var loCodProducto = ((BLL.VentaProductos)e.Item.DataItem).COD_PRODUCTO.ToString();
                var loEdicion = ((BLL.VentaProductos)e.Item.DataItem).EDICION.ToString();

                HtmlButton btnEliminar = ((HtmlButton)e.Item.FindControl("btnEliminar"));

                var loCodigo = String.Format("{0}-{1}", loCodProducto, loEdicion);

                btnEliminar.Attributes.Add("value", loCodigo);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnAgregarItem_Click(object sender, EventArgs e)
        {
            List<VentaProductos> lstVentaProductos = null;
            bool loResutado = false;
            double loValorTotal = 0;
            double loMontoTotal = 0;

            try
            {
                var loProducto = ((HtmlButton)sender).Attributes["value"];

                var loProductoItem = loProducto.Split('-');
                var loCodigoProducto = loProductoItem[0];
                var loEdicion = loProductoItem[1];

                if (!String.IsNullOrEmpty(lblTotal.Text))
                    loMontoTotal = Convert.ToDouble(lblTotal.Text);

                lsvVenta.Visible = true;
                lstVentaProductos = new List<VentaProductos>();

                switch (Convert.ToInt32(ddlTipoProducto.SelectedValue))
                {
                    case 1:
                        foreach (var loItem in lsvDiarios.Items)
                        {
                            if (((Label)loItem.Controls[1]).Text.ToString() == loCodigoProducto && ((Label)loItem.Controls[7]).Text.ToString() == loEdicion)
                            {
                                if (Convert.ToInt32(((TextBox)loItem.Controls[15]).Text.ToString()) > 0 && Convert.ToInt32(((TextBox)loItem.Controls[15]).Text.ToString()) <= Convert.ToInt32(((Label)loItem.Controls[13]).Text.ToString())) //CANTIDAD_DISPONIBLE debe ser mayor a 0 y menor o igual a al Sotck
                                {
                                    var loPrecioUnitario = ((Label)loItem.Controls[11]).Text.Split(' ').Last();
                                    loValorTotal = Convert.ToDouble(loPrecioUnitario) * Convert.ToInt32(((TextBox)loItem.Controls[15]).Text.ToString());

                                    VentaProductos oVentaProductos = new VentaProductos
                                    {
                                        COD_PRODUCTO = Convert.ToInt32(((Label)loItem.Controls[1]).Text.ToString()),
                                        COD_PRODUCTO_EDICION = Convert.ToInt32(((Label)loItem.Controls[19]).Text.ToString()),
                                        NOMBRE = ((Label)loItem.Controls[3]).Text.ToString(),
                                        TIPO_PRODUCTO = ((Label)loItem.Controls[5]).Text.ToString(),
                                        EDICION = ((Label)loItem.Controls[7]).Text.ToString(),
                                        PRECIO_UNITARIO = ((Label)loItem.Controls[11]).Text.ToString(),
                                        CANTIDAD = Convert.ToInt32(((TextBox)loItem.Controls[15]).Text.ToString()),
                                        VALOR_TOTAL = "$ " + loValorTotal.ToString()
                                    };

                                    lstVentaProductos.Add(oVentaProductos);
                                    loMontoTotal += loValorTotal;
                                    loResutado = true;
                                    // Eliminar borde rojo de las celda
                                    ((TextBox)loItem.Controls[15]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                                }
                                else
                                {
                                    if (Convert.ToInt32(((TextBox)loItem.Controls[15]).Text.ToString()) == 0)
                                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaCampoCantidadObligatorio));
                                    else if (Convert.ToInt32(((TextBox)loItem.Controls[15]).Text.ToString()) < 0)
                                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaCantidadInvalida));
                                    else
                                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaStockInsuficiente));

                                    // Borde rojo para el campo CANTIDAD_DISPONIBLE
                                    ((TextBox)loItem.Controls[15]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");
                                }

                                break;
                            }
                        }
                        break;

                    case 2:
                        foreach (var loItem in lsvRevistas.Items)
                        {
                            if (((Label)loItem.Controls[1]).Text.ToString() == loCodigoProducto && ((Label)loItem.Controls[7]).Text.ToString() == loEdicion)
                            {
                                if (Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) > 0 && Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) <= Convert.ToInt32(((Label)loItem.Controls[15]).Text.ToString())) //CANTIDAD_DISPONIBLE debe ser mayor a 0 y menor o igual a al Sotck
                                {
                                    var loPrecioUnitario = ((Label)loItem.Controls[13]).Text.Split(' ').Last();
                                    loValorTotal = Convert.ToDouble(loPrecioUnitario) * Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString());

                                    VentaProductos oVentaProductos = new VentaProductos
                                    {
                                        COD_PRODUCTO = Convert.ToInt32(((Label)loItem.Controls[1]).Text.ToString()),
                                        COD_PRODUCTO_EDICION = Convert.ToInt32(((Label)loItem.Controls[21]).Text.ToString()),
                                        NOMBRE = ((Label)loItem.Controls[3]).Text.ToString(),
                                        TIPO_PRODUCTO = ((Label)loItem.Controls[5]).Text.ToString(),
                                        EDICION = ((Label)loItem.Controls[7]).Text.ToString(),
                                        PRECIO_UNITARIO = ((Label)loItem.Controls[13]).Text.ToString(),
                                        CANTIDAD = Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()),
                                        VALOR_TOTAL = "$ " + loValorTotal.ToString()
                                    };

                                    lstVentaProductos.Add(oVentaProductos);
                                    loMontoTotal += loValorTotal;
                                    loResutado = true;
                                    // Eliminar borde rojo de las celda
                                    ((TextBox)loItem.Controls[17]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                                }
                                else
                                {
                                    if (Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) == 0)
                                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaCampoCantidadObligatorio));
                                    else if (Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) < 0)
                                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaCantidadInvalida));
                                    else
                                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaStockInsuficiente));

                                    // Borde rojo para el campo CANTIDAD_DISPONIBLE
                                    ((TextBox)loItem.Controls[17]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");
                                }

                                break;
                            }
                        }
                        break;

                    case 3:
                        foreach (var loItem in lsvColecciones.Items)
                        {
                            if (((Label)loItem.Controls[1]).Text.ToString() == loCodigoProducto && ((Label)loItem.Controls[7]).Text.ToString() == loEdicion)
                            {
                                if (Convert.ToInt32(((TextBox)loItem.Controls[15]).Text.ToString()) > 0 && Convert.ToInt32(((TextBox)loItem.Controls[15]).Text.ToString()) <= Convert.ToInt32(((Label)loItem.Controls[13]).Text.ToString())) //CANTIDAD_DISPONIBLE debe ser mayor a 0 y menor o igual a al Sotck
                                {
                                    var loPrecioUnitario = ((Label)loItem.Controls[11]).Text.Split(' ').Last();
                                    loValorTotal = Convert.ToDouble(loPrecioUnitario) * Convert.ToInt32(((TextBox)loItem.Controls[15]).Text.ToString());

                                    VentaProductos oVentaProductos = new VentaProductos
                                    {
                                        COD_PRODUCTO = Convert.ToInt32(((Label)loItem.Controls[1]).Text.ToString()),
                                        COD_PRODUCTO_EDICION = Convert.ToInt32(((Label)loItem.Controls[19]).Text.ToString()),
                                        NOMBRE = ((Label)loItem.Controls[3]).Text.ToString(),
                                        TIPO_PRODUCTO = ((Label)loItem.Controls[5]).Text.ToString(),
                                        EDICION = ((Label)loItem.Controls[7]).Text.ToString(),
                                        PRECIO_UNITARIO = ((Label)loItem.Controls[11]).Text.ToString(),
                                        CANTIDAD = Convert.ToInt32(((TextBox)loItem.Controls[15]).Text.ToString()),
                                        VALOR_TOTAL = "$ " + loValorTotal.ToString()
                                    };

                                    lstVentaProductos.Add(oVentaProductos);
                                    loMontoTotal += loValorTotal;
                                    loResutado = true;
                                    // Eliminar borde rojo de las celda
                                    ((TextBox)loItem.Controls[15]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                                }
                                else
                                {
                                    if (Convert.ToInt32(((TextBox)loItem.Controls[15]).Text.ToString()) == 0)
                                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaCampoCantidadObligatorio));
                                    else if (Convert.ToInt32(((TextBox)loItem.Controls[15]).Text.ToString()) < 0)
                                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaCantidadInvalida));
                                    else
                                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaStockInsuficiente));

                                    // Borde rojo para el campo CANTIDAD_DISPONIBLE
                                    ((TextBox)loItem.Controls[15]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");
                                }

                                break;
                            }
                        }
                        break;

                    case 4:
                        foreach (var loItem in lsvLibros.Items)
                        {
                            if (((Label)loItem.Controls[1]).Text.ToString() == loCodigoProducto && ((Label)loItem.Controls[7]).Text.ToString() == loEdicion)
                            {
                                if (Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) > 0 && Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) <= Convert.ToInt32(((Label)loItem.Controls[15]).Text.ToString())) //CANTIDAD_DISPONIBLE debe ser mayor a 0 y menor o igual a al Sotck
                                {
                                    var loPrecioUnitario = ((Label)loItem.Controls[13]).Text.Split(' ').Last();
                                    loValorTotal = Convert.ToDouble(loPrecioUnitario) * Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString());

                                    VentaProductos oVentaProductos = new VentaProductos
                                    {
                                        COD_PRODUCTO = Convert.ToInt32(((Label)loItem.Controls[1]).Text.ToString()),
                                        COD_PRODUCTO_EDICION = Convert.ToInt32(((Label)loItem.Controls[21]).Text.ToString()),
                                        NOMBRE = ((Label)loItem.Controls[3]).Text.ToString(),
                                        TIPO_PRODUCTO = ((Label)loItem.Controls[5]).Text.ToString(),
                                        EDICION = ((Label)loItem.Controls[7]).Text.ToString(),
                                        PRECIO_UNITARIO = ((Label)loItem.Controls[13]).Text.ToString(),
                                        CANTIDAD = Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()),
                                        VALOR_TOTAL = "$ " + loValorTotal.ToString()
                                    };

                                    lstVentaProductos.Add(oVentaProductos);
                                    loMontoTotal += loValorTotal;
                                    loResutado = true;
                                    // Eliminar borde rojo de las celda
                                    ((TextBox)loItem.Controls[17]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                                }
                                else
                                {
                                    if (Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) == 0)
                                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaCampoCantidadObligatorio));
                                    else if (Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) < 0)
                                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaCantidadInvalida));
                                    else
                                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaStockInsuficiente));

                                    // Borde rojo para el campo CANTIDAD_DISPONIBLE
                                    ((TextBox)loItem.Controls[17]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");
                                }

                                break;
                            }
                        }
                        break;

                    case 5:
                        foreach (var loItem in lsvSuplementos.Items)
                        {
                            if (((Label)loItem.Controls[1]).Text.ToString() == loCodigoProducto && ((Label)loItem.Controls[7]).Text.ToString() == loEdicion)
                            {
                                if (Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) > 0 && Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) <= Convert.ToInt32(((Label)loItem.Controls[15]).Text.ToString())) //CANTIDAD_DISPONIBLE debe ser mayor a 0 y menor o igual a al Sotck
                                {
                                    var loPrecioUnitario = ((Label)loItem.Controls[13]).Text.Split(' ').Last();
                                    loValorTotal = Convert.ToDouble(loPrecioUnitario) * Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString());

                                    VentaProductos oVentaProductos = new VentaProductos
                                    {
                                        COD_PRODUCTO = Convert.ToInt32(((Label)loItem.Controls[1]).Text.ToString()),
                                        COD_PRODUCTO_EDICION = Convert.ToInt32(((Label)loItem.Controls[21]).Text.ToString()),
                                        NOMBRE = ((Label)loItem.Controls[3]).Text.ToString(),
                                        TIPO_PRODUCTO = ((Label)loItem.Controls[5]).Text.ToString(),
                                        EDICION = ((Label)loItem.Controls[7]).Text.ToString(),
                                        PRECIO_UNITARIO = ((Label)loItem.Controls[13]).Text.ToString(),
                                        CANTIDAD = Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()),
                                        VALOR_TOTAL = "$ " + loValorTotal.ToString()
                                    };

                                    lstVentaProductos.Add(oVentaProductos);
                                    loMontoTotal += loValorTotal;
                                    loResutado = true;
                                    // Eliminar borde rojo de las celda
                                    ((TextBox)loItem.Controls[17]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                                }
                                else
                                {
                                    if (Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) == 0)
                                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaCampoCantidadObligatorio));
                                    else if (Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) < 0)
                                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaCantidadInvalida));
                                    else
                                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaStockInsuficiente));

                                    // Borde rojo para el campo CANTIDAD_DISPONIBLE
                                    ((TextBox)loItem.Controls[17]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");
                                }

                                break;
                            }
                        }
                        break;

                    case 6:
                        foreach (var loItem in lsvPeliculas.Items)
                        {
                            if (((Label)loItem.Controls[1]).Text.ToString() == loCodigoProducto && ((Label)loItem.Controls[7]).Text.ToString() == loEdicion)
                            {
                                if (Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) > 0 && Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) <= Convert.ToInt32(((Label)loItem.Controls[15]).Text.ToString())) //CANTIDAD_DISPONIBLE debe ser mayor a 0 y menor o igual a al Sotck
                                {
                                    var loPrecioUnitario = ((Label)loItem.Controls[13]).Text.Split(' ').Last();
                                    loValorTotal = Convert.ToDouble(loPrecioUnitario) * Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString());

                                    VentaProductos oVentaProductos = new VentaProductos
                                    {
                                        COD_PRODUCTO = Convert.ToInt32(((Label)loItem.Controls[1]).Text.ToString()),
                                        COD_PRODUCTO_EDICION = Convert.ToInt32(((Label)loItem.Controls[21]).Text.ToString()),
                                        NOMBRE = ((Label)loItem.Controls[3]).Text.ToString(),
                                        TIPO_PRODUCTO = ((Label)loItem.Controls[5]).Text.ToString(),
                                        EDICION = ((Label)loItem.Controls[7]).Text.ToString(),
                                        PRECIO_UNITARIO = ((Label)loItem.Controls[13]).Text.ToString(),
                                        CANTIDAD = Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()),
                                        VALOR_TOTAL = "$ " + loValorTotal.ToString()
                                    };

                                    lstVentaProductos.Add(oVentaProductos);
                                    loMontoTotal += loValorTotal;
                                    loResutado = true;
                                    // Eliminar borde rojo de las celda
                                    ((TextBox)loItem.Controls[17]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                                }
                                else
                                {
                                    if (Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) == 0)
                                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaCampoCantidadObligatorio));
                                    else if (Convert.ToInt32(((TextBox)loItem.Controls[17]).Text.ToString()) < 0)
                                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaCantidadInvalida));
                                    else
                                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeVentaStockInsuficiente));

                                    // Borde rojo para el campo CANTIDAD_DISPONIBLE
                                    ((TextBox)loItem.Controls[17]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");
                                }

                                break;
                            }
                        }
                        break;
                }

                if (loResutado)
                {
                    if (lsvVenta.Items.Count == 0)
                    {
                        lsvVenta.DataSource = lstVentaProductos;
                    }
                    else
                    {
                        var loItem = lsvVenta.Items.Where(x => ((Label)x.Controls[1]).Text.ToString().Equals(loCodigoProducto) && ((Label)x.Controls[7]).Text.ToString().Equals(loEdicion)).FirstOrDefault();

                        if (loItem != null)
                        { return; }

                        List<VentaProductos> listViewVenta = MapListViewToListObject(lsvVenta);
                        if (listViewVenta != null)
                        {
                            lstVentaProductos.ForEach(x => listViewVenta.Add(x));
                            lsvVenta.DataSource = listViewVenta;
                        }
                        else
                            return;
                    }

                    lsvVenta.DataBind();
                    MostrarOcultarDivsVentas(true);
                    if (loMontoTotal > 0)
                        lblTotal.Text = loMontoTotal.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            double loMontoTotal = 0;
            loMontoTotal = Convert.ToDouble(lblTotal.Text);

            var loProducto = ((HtmlButton)sender).Attributes["value"];

            var loProductoItem = loProducto.Split('-');
            var loCodigoProducto = loProductoItem[0];
            var loEdicion = loProductoItem[1];

            var loItem = lsvVenta.Items.Where(x => ((Label)x.Controls[1]).Text.ToString().Equals(loCodigoProducto)
            && ((Label)x.Controls[7]).Text.ToString().Equals(loEdicion)).First();
            lsvVenta.Items.Remove(loItem);

            var loMonto = ((Label)loItem.Controls[13]).Text.Split(' ').Last();
            loMontoTotal = loMontoTotal - Convert.ToDouble(loMonto);
            lblTotal.Text = loMontoTotal.ToString();
            lsvVenta.DataSource = MapListViewToListObject(lsvVenta);
            lsvVenta.DataBind();

            if (lsvVenta.Items.Count == 0)
                MostrarOcultarDivsVentas();
        }

        #endregion

        #region Métodos Privados

        private void OcultarDivsMensajes()
        {
            dvMensajeLsvProductos.Visible = false;
        }

        /// <summary>
        /// Muestra u oculta el div de ventas segun el parámetro que se le pase. Por defecto es falso. 
        /// </summary>
        /// <param name="pAcccion"></param>
        private void MostrarOcultarDivsVentas(bool pAcccion = false)
        {
            divVentaTotales.Visible = pAcccion;
        }

        private void CargarFormaDePago()
        {
            var oFormaPagoBLL = new BLL.FormaPagoBLL();

            try
            {
                ddlFormaPago.DataSource = oFormaPagoBLL.ObtenerFormasPago();
                ddlFormaPago.DataTextField = "DESCRIPCION";
                ddlFormaPago.DataValueField = "ID_FORMA_PAGO";
                ddlFormaPago.DataBind();
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

                switch (oProductoFiltro.CodTipoProducto)
                {
                    case 1:
                        lsvDiarios.Visible = true;
                        lsvRevistas.Visible = false;
                        lsvColecciones.Visible = false;
                        lsvLibros.Visible = false;
                        lsvSuplementos.Visible = false;
                        lsvPeliculas.Visible = false;

                        var lstDiarios = new BLL.DiarioBLL().ObtenerDiariosEdicion(oProductoFiltro);
                        if (lstDiarios != null && lstDiarios.Count > 0)
                            lsvDiarios.DataSource = lstDiarios;
                        else
                        {
                            dvMensajeLsvProductos.InnerHtml = MessageManager.Info(dvMensajeLsvProductos, Message.MsjeListadoProductoFiltrarTotalSinResultados, false);
                            dvMensajeLsvProductos.Visible = true;
                        }

                        lsvDiarios.DataBind();
                        break;

                    case 2:
                        lsvDiarios.Visible = false;
                        lsvRevistas.Visible = true;
                        lsvColecciones.Visible = false;
                        lsvLibros.Visible = false;
                        lsvSuplementos.Visible = false;
                        lsvPeliculas.Visible = false;

                        var lstRevistas = new BLL.RevistaBLL().ObtenerRevistasEdicion(oProductoFiltro);
                        if (lstRevistas != null && lstRevistas.Count > 0)
                            lsvRevistas.DataSource = lstRevistas;
                        else
                        {
                            dvMensajeLsvProductos.InnerHtml = MessageManager.Info(dvMensajeLsvProductos, Message.MsjeListadoProductoFiltrarTotalSinResultados, false);
                            dvMensajeLsvProductos.Visible = true;
                        }

                        lsvRevistas.DataBind();
                        break;

                    case 3:
                        lsvDiarios.Visible = false;
                        lsvRevistas.Visible = false;
                        lsvColecciones.Visible = true;
                        lsvLibros.Visible = false;
                        lsvSuplementos.Visible = false;
                        lsvPeliculas.Visible = false;

                        var lstColecciones = new BLL.ColeccionBLL().ObtenerColeccionesEdicion(oProductoFiltro);
                        if (lstColecciones != null && lstColecciones.Count > 0)
                            lsvColecciones.DataSource = lstColecciones;
                        else
                        {
                            dvMensajeLsvProductos.InnerHtml = MessageManager.Info(dvMensajeLsvProductos, Message.MsjeListadoProductoFiltrarTotalSinResultados, false);
                            dvMensajeLsvProductos.Visible = true;
                        }

                        lsvColecciones.DataBind();
                        break;

                    case 4:
                        lsvDiarios.Visible = false;
                        lsvRevistas.Visible = false;
                        lsvColecciones.Visible = false;
                        lsvLibros.Visible = true;
                        lsvSuplementos.Visible = false;
                        lsvPeliculas.Visible = false;

                        var lstLibros = new BLL.LibroBLL().ObtenerLibrosEdicion(oProductoFiltro);
                        if (lstLibros != null && lstLibros.Count > 0)
                            lsvLibros.DataSource = lstLibros;
                        else
                        {
                            dvMensajeLsvProductos.InnerHtml = MessageManager.Info(dvMensajeLsvProductos, Message.MsjeListadoProductoFiltrarTotalSinResultados, false);
                            dvMensajeLsvProductos.Visible = true;
                        }

                        lsvLibros.DataBind();
                        break;

                    case 5:
                        lsvDiarios.Visible = false;
                        lsvRevistas.Visible = false;
                        lsvColecciones.Visible = false;
                        lsvLibros.Visible = false;
                        lsvSuplementos.Visible = true;
                        lsvPeliculas.Visible = false;

                        var lstSuplementos = new BLL.SuplementoBLL().ObtenerSuplementosEdicion(oProductoFiltro);
                        if (lstSuplementos != null && lstSuplementos.Count > 0)
                            lsvSuplementos.DataSource = lstSuplementos;
                        else
                        {
                            dvMensajeLsvProductos.InnerHtml = MessageManager.Info(dvMensajeLsvProductos, Message.MsjeListadoProductoFiltrarTotalSinResultados, false);
                            dvMensajeLsvProductos.Visible = true;
                        }

                        lsvSuplementos.DataBind();
                        break;

                    case 6:
                        lsvDiarios.Visible = false;
                        lsvRevistas.Visible = false;
                        lsvColecciones.Visible = false;
                        lsvLibros.Visible = false;
                        lsvSuplementos.Visible = false;
                        lsvPeliculas.Visible = true;

                        var lstPeliculas = new BLL.PeliculaBLL().ObtenerPeliculasEdicion(oProductoFiltro);
                        if (lstPeliculas != null && lstPeliculas.Count > 0)
                            lsvPeliculas.DataSource = lstPeliculas;
                        else
                        {
                            dvMensajeLsvProductos.InnerHtml = MessageManager.Info(dvMensajeLsvProductos, Message.MsjeListadoProductoFiltrarTotalSinResultados, false);
                            dvMensajeLsvProductos.Visible = true;
                        }

                        lsvPeliculas.DataBind();
                        break;
                }
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
            lsvDiarios.Visible = false;
            lsvRevistas.Visible = false;
            lsvColecciones.Visible = false;
            lsvLibros.Visible = false;
            lsvSuplementos.Visible = false;
            lsvPeliculas.Visible = false;
        }

        private void LimpiarPantalla()
        {
            txtCodigoVenta.Text = new BLL.VentaBLL().ObtenerUltimaVenta().ToString();
            ddlFormaPago.SelectedIndex = 0;
            rdbPagadoNo.Checked = true;
            ddlTipoDocumento.SelectedIndex = 0;
            txtNroDocumento.Text = String.Empty;
            txtNombre.Text = String.Empty;
            txtApellido.Text = String.Empty;
            LimpiarSeleccionProductos();
            lsvVenta.Visible = false;
            lsvVenta.DataSource = null;
            lsvVenta.DataBind();
            MostrarOcultarDivsVentas();
            lblTotal.Text = String.Empty;
        }

        private List<VentaProductos> MapListViewToListObject(ListView pListView)
        {
            bool loResultado = false;

            List<VentaProductos> lstVentaProductos = new List<VentaProductos>();

            foreach (var loItem in pListView.Items)
            {
                VentaProductos oVentaProductos = new VentaProductos
                {
                    COD_PRODUCTO = Convert.ToInt32(((Label)loItem.Controls[1]).Text),
                    COD_PRODUCTO_EDICION = Convert.ToInt32(((Label)loItem.Controls[17]).Text.ToString()),
                    NOMBRE = ((Label)loItem.Controls[3]).Text,
                    TIPO_PRODUCTO = ((Label)loItem.Controls[5]).Text,
                    EDICION = ((Label)loItem.Controls[7]).Text,
                    PRECIO_UNITARIO = ((Label)loItem.Controls[9]).Text,
                    CANTIDAD = Convert.ToInt32(((Label)loItem.Controls[11]).Text),
                    VALOR_TOTAL = ((Label)loItem.Controls[13]).Text.ToString()
                };

                lstVentaProductos.Add(oVentaProductos);
            }

            if (loResultado)
                lstVentaProductos = null;

            return lstVentaProductos;
        }

        #endregion
    }

}