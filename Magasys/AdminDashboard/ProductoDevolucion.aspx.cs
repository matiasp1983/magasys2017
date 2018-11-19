using System;
using NLog;
using BLL.Filters;
using BLL;
using BLL.Common;
using System.Collections.Generic;
using System.Linq;
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
                txtCodigoDevolucion.Text = new ProductoDevolucionBLL().ObtenerUltimoProductodevolucion().ToString();
                txtFechaDevoluion.Text = DateTime.Now.ToString("dd/MM/yyyy");

                CargarTiposProducto();
                CargarProveedores();
                MostrarOcultarDivsDevolucion();
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

        protected void LsvDevolucion_ItemDataBound(object sender, ListViewItemEventArgs e)
        {  // El evento OnItemDataBound se llama cuando se setea la fuente de datos (DataSource) de la grilla
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
                if (((Devolucion)e.Item.DataItem).FECHA_DEVOLUCION < DateTime.Now)
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

        protected void LsvDetalleDevolucion_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return; //Controla que los registros a procesas correspondan al cuerpo de la grilla (ItemTemplate)

                var loCodProducto = ((DetalleDevolucion)e.Item.DataItem).COD_PRODUCTO.ToString();
                var loEdicion = ((DetalleDevolucion)e.Item.DataItem).EDICION.ToString();

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
            List<DetalleDevolucion> lstDetalleDevolucion = null;
            bool loResutado = false;

            try
            {
                var loProducto = ((HtmlButton)sender).Attributes["value"];

                var loProductoItem = loProducto.Split('-');
                var loCodigoProducto = loProductoItem[0];
                var loEdicion = loProductoItem[1];

                lsvDetalleDevolucion.Visible = true;
                lstDetalleDevolucion = new List<DetalleDevolucion>();


                foreach (var loItem in lsvDevolucion.Items)
                {
                    if (((Label)loItem.Controls[1]).Text.ToString() == loCodigoProducto && ((Label)loItem.Controls[7]).Text.ToString() == loEdicion)
                    {
                        if (Convert.ToInt32(((TextBox)loItem.Controls[15]).Text.ToString()) > 0 && Convert.ToInt32(((TextBox)loItem.Controls[15]).Text.ToString()) <= Convert.ToInt32(((Label)loItem.Controls[13]).Text.ToString())) //CANTIDAD_DISPONIBLE debe ser mayor a 0 y menor o igual a al Sotck
                        {
                            DetalleDevolucion oDetalleDevolucion = new DetalleDevolucion
                            {
                                COD_PRODUCTO = Convert.ToInt32(((Label)loItem.Controls[1]).Text.ToString()),
                                COD_PRODUCTO_EDICION = Convert.ToInt32(((Label)loItem.Controls[19]).Text.ToString()),
                                NOMBRE = ((Label)loItem.Controls[3]).Text.ToString(),
                                TIPO_PRODUCTO = ((Label)loItem.Controls[5]).Text.ToString(),
                                EDICION = ((Label)loItem.Controls[7]).Text.ToString(),
                                CANTIDAD = Convert.ToInt32(((TextBox)loItem.Controls[15]).Text.ToString()),
                            };

                            lstDetalleDevolucion.Add(oDetalleDevolucion);
                            loResutado = true;
                            // Eliminar borde rojo de la celda
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

                if (loResutado)
                {
                    if (lsvDetalleDevolucion.Items.Count == 0)
                    {
                        lsvDetalleDevolucion.DataSource = lstDetalleDevolucion;
                    }
                    else
                    {
                        var loItem = lsvDetalleDevolucion.Items.Where(x => ((Label)x.Controls[1]).Text.ToString().Equals(loCodigoProducto) && ((Label)x.Controls[7]).Text.ToString().Equals(loEdicion)).FirstOrDefault();

                        if (loItem != null)
                        { return; }

                        List<DetalleDevolucion> listViewDetalleDevolucion = MapListViewToListObject(lsvDetalleDevolucion);
                        if (listViewDetalleDevolucion != null)
                        {
                            lstDetalleDevolucion.ForEach(x => listViewDetalleDevolucion.Add(x));
                            lsvDetalleDevolucion.DataSource = listViewDetalleDevolucion;
                        }
                        else
                            return;
                    }

                    lsvDetalleDevolucion.DataBind();
                    MostrarOcultarDivsDevolucion(true);
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
            var loProducto = ((HtmlButton)sender).Attributes["value"];

            var loProductoItem = loProducto.Split('-');
            var loCodigoProducto = loProductoItem[0];
            var loEdicion = loProductoItem[1];

            var loItem = lsvDetalleDevolucion.Items.Where(x => ((Label)x.Controls[1]).Text.ToString().Equals(loCodigoProducto)
            && ((Label)x.Controls[7]).Text.ToString().Equals(loEdicion)).First();
            lsvDetalleDevolucion.Items.Remove(loItem);

            lsvDetalleDevolucion.DataSource = MapListViewToListObject(lsvDetalleDevolucion);
            lsvDetalleDevolucion.DataBind();

            if (lsvDetalleDevolucion.Items.Count == 0)
                MostrarOcultarDivsDevolucion();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            bool loResutado = false;
            List<BLL.DAL.DetalleProductoDevolucion> lstDetalleProductoDevolucion = new List<BLL.DAL.DetalleProductoDevolucion>();


            BLL.DAL.ProductoDevolucion oProductoDevolucion = new BLL.DAL.ProductoDevolucion()
            {
                FECHA = DateTime.Now,
                COD_ESTADO = 1,
            };

            foreach (var loItem in lsvDetalleDevolucion.Items)
            {
                BLL.DAL.DetalleProductoDevolucion oDetalleProductoDevolucion = new BLL.DAL.DetalleProductoDevolucion
                {
                    CANTIDAD = Convert.ToInt32(((Label)loItem.Controls[9]).Text.ToString()),
                    COD_PRODUCTO_EDICION = Convert.ToInt32(((Label)loItem.Controls[13]).Text.ToString())
                };

                lstDetalleProductoDevolucion.Add(oDetalleProductoDevolucion);

                // Actualizar Stock
                loResutado = new ProductoEdicionBLL().ActualizarCantidadDisponible(oDetalleProductoDevolucion.COD_PRODUCTO_EDICION, oDetalleProductoDevolucion.CANTIDAD);
            }

            if (loResutado)
                loResutado = new ProductoDevolucionBLL().AltaDevolucion(oProductoDevolucion, lstDetalleProductoDevolucion);

            if (loResutado)
            {
                LimpiarPantalla();
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeDevolucionSuccessAlta));
            }
            else
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeDevolucionFailure));

        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #endregion

        #region Métodos Privados

        private void OcultarDivsMensajes()
        {
            dvMensajeLsvDevolucion.Visible = false;
        }

        /// <summary>
        /// Muestra u oculta el div de devolución segun el parámetro que se le pase. Por defecto es falso. 
        /// </summary>
        /// <param name="pAcccion"></param>
        private void MostrarOcultarDivsDevolucion(bool pAcccion = false)
        {
            divDevolucion.Visible = pAcccion;
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

        private List<DetalleDevolucion> MapListViewToListObject(ListView pListView)
        {
            bool loResultado = false;

            List<DetalleDevolucion> lstDetalleDevolucion = new List<DetalleDevolucion>();

            foreach (var loItem in pListView.Items)
            {
                DetalleDevolucion oDetalleDevolucion = new DetalleDevolucion
                {
                    COD_PRODUCTO = Convert.ToInt32(((Label)loItem.Controls[1]).Text),
                    COD_PRODUCTO_EDICION = Convert.ToInt32(((Label)loItem.Controls[13]).Text.ToString()),
                    NOMBRE = ((Label)loItem.Controls[3]).Text,
                    TIPO_PRODUCTO = ((Label)loItem.Controls[5]).Text,
                    EDICION = ((Label)loItem.Controls[7]).Text,
                    CANTIDAD = Convert.ToInt32(((Label)loItem.Controls[9]).Text),
                };

                lstDetalleDevolucion.Add(oDetalleDevolucion);
            }

            if (loResultado)
                lstDetalleDevolucion = null;

            return lstDetalleDevolucion;
        }

        private void LimpiarPantalla()
        {
            txtCodigoDevolucion.Text = new BLL.ProductoDevolucionBLL().ObtenerUltimoProductodevolucion().ToString();
            LimpiarSeleccionProductos();
            lsvDetalleDevolucion.Visible = false;
            lsvDetalleDevolucion.DataSource = null;
            lsvDetalleDevolucion.DataBind();
            MostrarOcultarDivsDevolucion();
        }

        #endregion
    }
}