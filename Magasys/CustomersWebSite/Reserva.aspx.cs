using BLL;
using BLL.Common;
using BLL.Filters;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PL.CustomersWebSite
{
    public partial class Reserva : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarCantidadDePedidosDesdeSession();
                CargarTiposProducto();
                CargarGeneros();
                lblTotalAbonar.Text = "0,00";
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            ObtenerProductos();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void BtnReservar_Click(object sender, EventArgs e)
        {
            List<ProductoCustomersWebSite> lstProductoCustomersWebSite = new List<ProductoCustomersWebSite>();
            ProductoCustomersWebSite oProductoCustomersWebSite = null;
            int loCantidadProductosSeleccionados = 0;

            if (Session[Enums.Session.ListadoReserva.ToString()] != null)
                lstProductoCustomersWebSite = (List<ProductoCustomersWebSite>)Session[Enums.Session.ListadoReserva.ToString()];

            foreach (var loItem in lsvProductos.Items.Where(p => ((HtmlInputCheckBox)p.Controls[1]).Checked))
            {
                oProductoCustomersWebSite = new ProductoCustomersWebSite
                {
                    COD_PRODUCTO = Convert.ToInt32(((HtmlInputCheckBox)loItem.Controls[1]).Attributes["title"].Split(';')[0]),
                    COD_TIPO_PRODUCTO = Convert.ToInt32(ddlTipoProducto.SelectedValue),
                    NOMBRE_PRODUCTO = ((Label)loItem.Controls[7]).Text,
                    DESCRIPCION = ((Label)loItem.Controls[9]).Text,
                    PRECIO = ((Label)loItem.Controls[5].Controls[1]).Text
                };

                oProductoCustomersWebSite.IMAGEN = new Image();

                if (!String.IsNullOrEmpty(((HtmlImage)loItem.Controls[3]).Src))
                {
                    // Covertir la iamgen a un base 64 para mostrarlo en un dato binario
                    string loImagenDataURL64 = ((HtmlImage)loItem.Controls[3]).Src;
                    oProductoCustomersWebSite.IMAGEN.ImageUrl = loImagenDataURL64;
                }

                if (lstProductoCustomersWebSite.Where(p => p.COD_PRODUCTO == oProductoCustomersWebSite.COD_PRODUCTO).Count() == 0)
                    loCantidadProductosSeleccionados += 1;

                lstProductoCustomersWebSite.Add(oProductoCustomersWebSite);

                ((HtmlInputCheckBox)loItem.Controls[1]).Checked = false;
            }

            if (lstProductoCustomersWebSite.Count > 0)
            {
                Session.Add(Enums.Session.ListadoReserva.ToString(), lstProductoCustomersWebSite);

                var loCantidadDePedidosSession = Session[Enums.Session.CantidadDePedidos.ToString()];

                if (loCantidadDePedidosSession != null)
                    Session[Enums.Session.CantidadDePedidos.ToString()] = Convert.ToInt32(loCantidadDePedidosSession) + loCantidadProductosSeleccionados;
                else
                    Session[Enums.Session.CantidadDePedidos.ToString()] = lstProductoCustomersWebSite.GroupBy(p => p.COD_PRODUCTO).Count();

                CargarCantidadDePedidosDesdeSession();
            }
        }

        protected void BtnSeleccionarTodo_Click(object sender, EventArgs e)
        {
            double loMontoTotal = 0;

            foreach (var loItem in lsvProductos.Items)
            {
                ((HtmlInputCheckBox)loItem.Controls[1]).Checked = true;
                var loPrecio = ((Label)loItem.Controls[5].Controls[1]).Text.Remove(0, 1).Replace(",", ".");
                loMontoTotal = loMontoTotal + Convert.ToDouble(loPrecio);
            }

            lblTotalAbonar.Text = string.Format(System.Globalization.CultureInfo.GetCultureInfo("de-DE"), "{0:0.00}", loMontoTotal);
        }

        protected void BtnDeseleccionarTodo_Click(object sender, EventArgs e)
        {
            foreach (var loItem in lsvProductos.Items.Where(p => ((HtmlInputCheckBox)p.Controls[1]).Checked))
            {
                ((HtmlInputCheckBox)loItem.Controls[1]).Checked = false;
            }

            lblTotalAbonar.Text = "0,00";
        }

        protected void BtnMostrarEdiciones_Click(object sender, EventArgs e)
        {
            try
            {
                var loCodTipoProducto = (((HtmlButton)sender).Attributes["value"]).ToString().Split(',')[0];
                var loProducto = (((HtmlButton)sender).Attributes["value"]).ToString();

                Session.Add(Enums.Session.ProductoReserva.ToString(), loProducto);

                if (loCodTipoProducto == "2")
                    Response.Redirect("ReservaRevista.aspx", false);
                else if (loCodTipoProducto == "3")
                    Response.Redirect("ReservaColeccion.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void lsvProductos_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loImagen = ((ProductoCustomersWebSite)e.Item.DataItem).IMAGEN;
                HtmlImage imgProducto = ((HtmlImage)e.Item.FindControl("imgProducto"));

                if (string.IsNullOrEmpty(loImagen.ImageUrl))
                    imgProducto.Attributes.Add("src", "~/AdminDashboard/img/preview_icons.png");
                else
                    imgProducto.Attributes.Add("src", loImagen.ImageUrl);

                var loCodTipoProducto = ((ProductoCustomersWebSite)e.Item.DataItem).COD_TIPO_PRODUCTO;
                var loCodigoProducto = ((ProductoCustomersWebSite)e.Item.DataItem).COD_PRODUCTO.ToString();
                var loNombreProducto = ((ProductoCustomersWebSite)e.Item.DataItem).NOMBRE_PRODUCTO.ToString();

                HtmlButton btnEdiciones = ((HtmlButton)e.Item.FindControl("btnEdiciones"));
                HtmlAnchor btnReservarEdiciones = ((HtmlAnchor)e.Item.FindControl("btnReservarEdiciones"));

                if (loCodTipoProducto == 3 || loCodTipoProducto == 2)  //Producto Colección, Revista
                {
                    HtmlInputCheckBox chkCodigoProducto = ((HtmlInputCheckBox)e.Item.FindControl("chkCodigoProducto"));
                    if (chkCodigoProducto != null)
                    {
                        chkCodigoProducto.Attributes.Add("style", "width: 23px; height: 23px; margin:0px;");
                        chkCodigoProducto.Visible = false;
                    }

                    //Ocultar el Precio
                    HtmlGenericControl divPrecio = ((HtmlGenericControl)e.Item.FindControl("divPrecio"));
                    divPrecio.Visible = false;

                    btnEdiciones.Attributes.Add("value", string.Format("{0},{1}", loCodTipoProducto, loCodigoProducto));

                    HiddenField hdIdProductoReservaEdicion = ((HiddenField)e.Item.FindControl("hdIdProductoReservaEdicion"));
                    // Se concatena el IdProducto y el Nombre del Producto:
                    hdIdProductoReservaEdicion.Value = string.Format("{0},{1}", loCodigoProducto, loNombreProducto);

                    //if (loCodTipoProducto == 3) // Producto Colección: para botón "RESERVAR EDICIONES"
                    //{
                    //    HiddenField hdIdProductoReservaEdicion = ((HiddenField)e.Item.FindControl("hdIdProductoReservaEdicion"));
                    //    // Se concatena el IdProducto y el Nombre del Producto:
                    //    hdIdProductoReservaEdicion.Value = string.Format("{0},{1}", loCodigoProducto, loNombreProducto);
                    //}
                    //else
                    //    btnReservarEdiciones.Visible = false; // Producto Revista, no se disponibiliza el botón "RESERVAR EDICIONES"
                }
                else
                {
                    btnEdiciones.Visible = false;
                    btnReservarEdiciones.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnReservaEdicionSeleccionado_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(hdIdProductoReservaEdicionSeleccionado.Value))
                {
                    var loIdProductoReservaEdicionSeleccionado = Convert.ToInt64(hdIdProductoReservaEdicionSeleccionado.Value);
                    if (Session[Enums.Session.ProductoReservaEdicionSeleccionados.ToString()] == null)
                    {
                        Session[Enums.Session.ProductoReservaEdicionSeleccionados.ToString()] = loIdProductoReservaEdicionSeleccionado;
                    }
                    else
                    {
                        var loProductosSeleccionados = Session[Enums.Session.ProductoReservaEdicionSeleccionados.ToString()];
                        if (!string.IsNullOrEmpty(loProductosSeleccionados.ToString()) && !loProductosSeleccionados.ToString().Contains(loIdProductoReservaEdicionSeleccionado.ToString()))
                            Session[Enums.Session.ProductoReservaEdicionSeleccionados.ToString()] += String.Format(";{0}", loIdProductoReservaEdicionSeleccionado);
                    }

                    if (Session[Enums.Session.CantidadDePedidos.ToString()] != null)
                    {
                        var loCantidadDePedidos = Convert.ToInt32(Session[Enums.Session.CantidadDePedidos.ToString()]);
                        Session[Enums.Session.CantidadDePedidos.ToString()] = loCantidadDePedidos + 1;
                    }
                    else
                        Session[Enums.Session.CantidadDePedidos.ToString()] = 1;

                    CargarCantidadDePedidosDesdeSession();
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

        private void CargarGeneros()
        {
            var oGenero = new BLL.GeneroBLL();

            try
            {
                ddlGenero.DataSource = oGenero.ObtenerGeneros();
                ddlGenero.DataTextField = "NOMBRE";
                ddlGenero.DataValueField = "ID_GENERO";
                ddlGenero.DataBind();
                ddlGenero.Items.Insert(0, new ListItem(String.Empty, String.Empty));
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

            if (!String.IsNullOrEmpty(txtNombreProducto.Text))
                oProductoFiltro.NombreProducto = txtNombreProducto.Text;

            if (!String.IsNullOrEmpty(txtDescripcion.Text))
                oProductoFiltro.DescripcionProducto = txtDescripcion.Text;

            if (!String.IsNullOrEmpty(ddlGenero.SelectedValue))
                oProductoFiltro.CodGenero = Convert.ToInt32(ddlGenero.SelectedValue);

            return oProductoFiltro;
        }

        private void ObtenerProductos()
        {
            try
            {
                var oProductoFiltro = CargarProductoFiltro();

                if (ddlTipoProducto.SelectedValue == "3" || ddlTipoProducto.SelectedValue == "2") // Producto Colección - Revista                
                    divReserva.Visible = false;
                else
                    divReserva.Visible = true;

                var lstProductos = new BLL.ProductoBLL().ObtenerProductosSeleccionados(oProductoFiltro);

                if (lstProductos != null && lstProductos.Count > 0)
                    lsvProductos.DataSource = lstProductos;

                lblTotalAbonar.Text = "0,00";
                lsvProductos.DataBind();
                lsvProductos.Visible = true;
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void LimpiarCampos()
        {
            FormReserva.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            FormReserva.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            lsvProductos.Visible = false;
        }

        private void CargarCantidadDePedidosDesdeSession()
        {
            Master.CantidadDePedidos = Convert.ToInt32(Session[Enums.Session.CantidadDePedidos.ToString()]);
        }

        #endregion
    }
}