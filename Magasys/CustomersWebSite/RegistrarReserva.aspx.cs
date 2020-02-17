using BLL;
using BLL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.CustomersWebSite
{
    public partial class RegistrarReserva : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                ObtenerReservas();
        }

        protected void lsvProductos_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //try
            //{
            //    if (e.Item.ItemType != ListViewItemType.DataItem) return;

            //    var loImagen = ((ProductoCustomersWebSite)e.Item.DataItem).IMAGEN;
            //    HtmlImage imgProducto = ((HtmlImage)e.Item.FindControl("imgProducto"));

            //    if (string.IsNullOrEmpty(loImagen.ImageUrl))
            //        imgProducto.Attributes.Add("src", "~/AdminDashboard/img/preview_icons.png");
            //    else
            //        imgProducto.Attributes.Add("src", loImagen.ImageUrl);

            //    var loCodTipoProducto = ((ProductoCustomersWebSite)e.Item.DataItem).COD_TIPO_PRODUCTO;
            //    var loCodigoProducto = ((ProductoCustomersWebSite)e.Item.DataItem).COD_PRODUCTO.ToString();

            //    HtmlButton btnEdiciones = ((HtmlButton)e.Item.FindControl("btnEdiciones"));

            //    if (loCodTipoProducto == 3 || loCodTipoProducto == 2)  //Producto Colección, Revista
            //    {
            //        HtmlInputCheckBox chkCodigoProducto = ((HtmlInputCheckBox)e.Item.FindControl("chkCodigoProducto"));
            //        if (chkCodigoProducto != null)
            //        {
            //            chkCodigoProducto.Attributes.Add("style", "width: 23px; height: 23px; margin:0px;");
            //            chkCodigoProducto.Visible = false;
            //        }

            //        //Ocultar el Precio
            //        Label lblPrecio = ((Label)e.Item.FindControl("lblPrecio"));
            //        lblPrecio.Visible = false;
            //        btnEdiciones.Attributes.Add("value", string.Format("{0},{1}", loCodTipoProducto, loCodigoProducto));
            //    }
            //    else
            //        btnEdiciones.Visible = false;
            //}
            //catch (Exception ex)
            //{
            //    Logger loLogger = LogManager.GetCurrentClassLogger();
            //    loLogger.Error(ex);
            //}
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {

        }


        #endregion

        #region Métodos Privados

        private void ObtenerReservas()
        {
            List<ReservaCustomerWebSite> lstReservaCustomerWebSite = new List<ReservaCustomerWebSite>();
            ReservaCustomerWebSite oReservaCustomerWebSite = null;

            if (Session[Enums.Session.ListadoReserva.ToString()] != null)
            {
                var lstProductoCustomersWebSite = (List<ProductoCustomersWebSite>)Session[Enums.Session.ListadoReserva.ToString()];

                foreach (var oProductoCustomersWebSite in lstProductoCustomersWebSite)
                {
                    var oProductoMod = lstReservaCustomerWebSite.Find(p => p.COD_PRODUCTO == oProductoCustomersWebSite.COD_PRODUCTO.ToString());

                    if (oProductoMod != null)
                    {
                        oProductoMod.CANTIDAD += 1;
                        var loPrecio = oProductoMod.PRECIO.Replace(",", ".").Replace("$", "").Trim();
                        var loSubTotal = oProductoMod.SUBTOTAL.Replace(",", ".").Replace("$", "").Trim();
                        oProductoMod.SUBTOTAL = "$ " + string.Format(System.Globalization.CultureInfo.GetCultureInfo("de-DE"), "{0:0.00}", (double.Parse(loPrecio) + double.Parse(loSubTotal)));
                        lstReservaCustomerWebSite[lstReservaCustomerWebSite.FindIndex(p => p.COD_PRODUCTO == oProductoCustomersWebSite.COD_PRODUCTO.ToString())] = oProductoMod;
                    }
                    else
                    {
                        oReservaCustomerWebSite = new ReservaCustomerWebSite
                        {
                            COD_PRODUCTO = oProductoCustomersWebSite.COD_PRODUCTO.ToString(),
                            NOMBRE = oProductoCustomersWebSite.NOMBRE_PRODUCTO,
                            DESCRIPCION = oProductoCustomersWebSite.DESCRIPCION,
                            PRECIO = oProductoCustomersWebSite.PRECIO,
                            CANTIDAD = 1,
                            SUBTOTAL = oProductoCustomersWebSite.PRECIO
                        };

                        lstReservaCustomerWebSite.Add(oReservaCustomerWebSite);
                    }
                }
            }

            if (Session[Enums.Session.ListadoReservaEdicion.ToString()] != null)
            {
                var lstProdEdicionCustomersWebSite = (List<ProdEdicionCustomersWebSite>)Session[Enums.Session.ListadoReservaEdicion.ToString()];

                foreach (var oProdEdicionCustomersWebSite in lstProdEdicionCustomersWebSite)
                {
                    var oProductoMod = lstReservaCustomerWebSite.Find(p => p.COD_PRODUCTO_EDICION == oProdEdicionCustomersWebSite.COD_PRODUCTO_EDICION.ToString());

                    if (oProductoMod != null)
                    {
                        oProductoMod.CANTIDAD += 1;
                        var loPrecio = oProductoMod.PRECIO.Replace(",", ".").Replace("$", "").Trim();
                        var loSubTotal = oProductoMod.SUBTOTAL.Replace(",", ".").Replace("$", "").Trim();
                        oProductoMod.SUBTOTAL = "$ " + string.Format(System.Globalization.CultureInfo.GetCultureInfo("de-DE"), "{0:0.00}", (double.Parse(loPrecio) + double.Parse(loSubTotal)));
                        lstReservaCustomerWebSite[lstReservaCustomerWebSite.FindIndex(p => p.COD_PRODUCTO_EDICION == oProdEdicionCustomersWebSite.COD_PRODUCTO_EDICION.ToString())] = oProductoMod;
                    }
                    else
                    {
                        oReservaCustomerWebSite = new ReservaCustomerWebSite
                        {
                            COD_PRODUCTO_EDICION = oProdEdicionCustomersWebSite.COD_PRODUCTO_EDICION.ToString(),
                            NOMBRE = oProdEdicionCustomersWebSite.EDICION,
                            DESCRIPCION = oProdEdicionCustomersWebSite.DESCRIPCION,
                            FECHA_EDICION = oProdEdicionCustomersWebSite.FECHA_EDICION,
                            PRECIO = oProdEdicionCustomersWebSite.PRECIO,
                            CANTIDAD = 1,
                            SUBTOTAL = oProdEdicionCustomersWebSite.PRECIO
                        };

                        lstReservaCustomerWebSite.Add(oReservaCustomerWebSite);
                    }
                }
            }

            if (lstReservaCustomerWebSite.Count > 0)
            {
                lsvProductos.DataSource = lstReservaCustomerWebSite;
                lsvProductos.DataBind();
            }
        }

        #endregion
    }
}