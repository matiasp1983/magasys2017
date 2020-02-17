using BLL;
using BLL.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PL.CustomersWebSite
{
    public partial class ReservaColeccion : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                ObtenerEdiciones();
        }

        protected void BtnReservar_Click(object sender, EventArgs e)
        {
            List<ProdEdicionCustomersWebSite> lstProdEdicionCustomersWebSite = new List<ProdEdicionCustomersWebSite>();
            ProdEdicionCustomersWebSite oProdEdicionCustomersWebSite = null;

            if (Session[Enums.Session.ListadoReservaEdicion.ToString()] != null)
                lstProdEdicionCustomersWebSite = (List<ProdEdicionCustomersWebSite>)Session[Enums.Session.ListadoReservaEdicion.ToString()];

            foreach (var loItem in lsvProductos.Items)
            {
                if (((HtmlInputCheckBox)loItem.Controls[1]).Checked)
                {
                    oProdEdicionCustomersWebSite = new ProdEdicionCustomersWebSite
                    {
                        COD_PRODUCTO_EDICION = Convert.ToInt32(((HtmlInputCheckBox)loItem.Controls[1]).Attributes["title"].Split(';')[0]),
                        EDICION = ((Label)loItem.Controls[7]).Text,
                        DESCRIPCION = ((Label)loItem.Controls[9]).Text,
                        PRECIO = ((Label)loItem.Controls[5]).Text
                    };

                    lstProdEdicionCustomersWebSite.Add(oProdEdicionCustomersWebSite);
                }
            }

            if (lstProdEdicionCustomersWebSite.Count > 0)
                Session.Add(Enums.Session.ListadoReservaEdicion.ToString(), lstProdEdicionCustomersWebSite);
        }

        protected void BtnSeleccionarTodo_Click(object sender, EventArgs e)
        {
            double loMontoTotal = 0;

            foreach (var loItem in lsvProductos.Items)
            {
                ((HtmlInputCheckBox)loItem.Controls[1]).Checked = true;
                var loPrevio = ((Label)loItem.Controls[5]).Text.Remove(0, 2).Replace(",", ".");
                loMontoTotal = loMontoTotal + Convert.ToDouble(loPrevio);
            }

            lblTotalAbonar.Text = string.Format(System.Globalization.CultureInfo.GetCultureInfo("de-DE"), "{0:0.00}", loMontoTotal);
        }

        protected void BtnDeseleccionarTodo_Click(object sender, EventArgs e)
        {
            foreach (var loItem in lsvProductos.Items)
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

        #endregion
    }
}