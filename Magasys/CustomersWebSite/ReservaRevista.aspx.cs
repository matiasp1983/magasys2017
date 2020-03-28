using BLL;
using BLL.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PL.CustomersWebSite
{
    public partial class ReservaRevista : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarCantidadDePedidosDesdeSession();
                ObtenerEdiciones();
            }
        }

        protected void BtnReservar_Click(object sender, EventArgs e)
        {
            List<ProdEdicionCustomersWebSite> lstProdEdicionCustomersWebSite = new List<ProdEdicionCustomersWebSite>();
            ProdEdicionCustomersWebSite oProdEdicionCustomersWebSite = null;
            int loCantidadProductosSeleccionados = 0;

            if (Session[Enums.Session.ListadoReservaEdicion.ToString()] != null)
                lstProdEdicionCustomersWebSite = (List<ProdEdicionCustomersWebSite>)Session[Enums.Session.ListadoReservaEdicion.ToString()];

            foreach (var loItem in lsvProductos.Items.Where(p => ((HtmlInputCheckBox)p.Controls[1]).Checked))
            {
                oProdEdicionCustomersWebSite = new ProdEdicionCustomersWebSite
                {
                    COD_PRODUCTO_EDICION = Convert.ToInt32(((HtmlInputCheckBox)loItem.Controls[1]).Attributes["title"].Split(';')[0]),
                    COD_TIPO_PRODUCTO = 2,
                    EDICION = ((Label)loItem.Controls[7]).Text,
                    DESCRIPCION = ((Label)loItem.Controls[9]).Text,
                    PRECIO = ((Label)loItem.Controls[5]).Text,
                    FECHA_EDICION = ((Label)loItem.Controls[11]).Text,
                    COD_PRODUCTO = Convert.ToInt32(((Label)loItem.Controls[13]).Text)
                };

                oProdEdicionCustomersWebSite.IMAGEN = new Image();

                if (!String.IsNullOrEmpty(((HtmlImage)loItem.Controls[3]).Src))
                {
                    // Covertir la iamgen a un base 64 para mostrarlo en un dato binario
                    string loImagenDataURL64 = ((HtmlImage)loItem.Controls[3]).Src;
                    oProdEdicionCustomersWebSite.IMAGEN.ImageUrl = loImagenDataURL64;
                }

                if (lstProdEdicionCustomersWebSite.Where(p => p.COD_PRODUCTO_EDICION == oProdEdicionCustomersWebSite.COD_PRODUCTO_EDICION).Count() == 0)
                    loCantidadProductosSeleccionados += 1;

                lstProdEdicionCustomersWebSite.Add(oProdEdicionCustomersWebSite);

                ((HtmlInputCheckBox)loItem.Controls[1]).Checked = false;
            }

            if (lstProdEdicionCustomersWebSite.Count > 0)
            {
                Session.Add(Enums.Session.ListadoReservaEdicion.ToString(), lstProdEdicionCustomersWebSite);

                var loCantidadDePedidosSession = Session[Enums.Session.CantidadDePedidos.ToString()];

                if (loCantidadDePedidosSession != null)
                    Session[Enums.Session.CantidadDePedidos.ToString()] = Convert.ToInt32(loCantidadDePedidosSession) + loCantidadProductosSeleccionados;
                else
                    Session[Enums.Session.CantidadDePedidos.ToString()] = lstProdEdicionCustomersWebSite.GroupBy(p => p.COD_PRODUCTO_EDICION).Count();

                CargarCantidadDePedidosDesdeSession();
            }
        }

        protected void BtnSeleccionarTodo_Click(object sender, EventArgs e)
        {
            double loMontoTotal = 0;

            foreach (var loItem in lsvProductos.Items)
            {
                ((HtmlInputCheckBox)loItem.Controls[1]).Checked = true;
                var loPrevio = ((Label)loItem.Controls[5]).Text.Remove(0, 1).Replace(",", ".");
                loMontoTotal = loMontoTotal + Convert.ToDouble(loPrevio);
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

        protected void lsvProductos_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loImagen = ((ProdEdicionCustomersWebSite)e.Item.DataItem).IMAGEN;
                HtmlImage imgProducto = ((HtmlImage)e.Item.FindControl("imgProducto"));

                if (string.IsNullOrEmpty(loImagen.ImageUrl))
                    imgProducto.Attributes.Add("src", "~/AdminDashboard/img/preview_icons.png");
                else
                    imgProducto.Attributes.Add("src", loImagen.ImageUrl);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        #endregion

        #region Métodos Privados

        private void ObtenerEdiciones()
        {
            try
            {
                if (Session[Enums.Session.ProductoReserva.ToString()] != null)
                {
                    var loCodTipoProducto = Session[Enums.Session.ProductoReserva.ToString()].ToString().Split(',')[0];
                    var loCodProducto = Session[Enums.Session.ProductoReserva.ToString()].ToString().Split(',')[1];

                    if (Convert.ToInt32(loCodProducto) > 0)
                    {
                        var lstProductos = new ProductoEdicionBLL().ObtenerEdiciones(Convert.ToInt32(loCodTipoProducto), Convert.ToInt32(loCodProducto));
                        if (lstProductos != null && lstProductos.Count > 0)
                            lsvProductos.DataSource = lstProductos;
                    }
                }

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

        private void CargarCantidadDePedidosDesdeSession()
        {
            Master.CantidadDePedidos = Convert.ToInt32(Session[Enums.Session.CantidadDePedidos.ToString()]);
        }

        #endregion
    }
}