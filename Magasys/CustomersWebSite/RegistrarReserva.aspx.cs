﻿using BLL;
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
            string loCodProducto;
            string loCodProductoEdicion;

            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loImagen = ((ReservaCustomerWebSite)e.Item.DataItem).IMAGEN;
                HtmlImage imgProducto = ((HtmlImage)e.Item.FindControl("imgProducto"));

                if (string.IsNullOrEmpty(loImagen.ImageUrl))
                    imgProducto.Attributes.Add("src", "~/AdminDashboard/img/preview_icons.png");
                else
                    imgProducto.Attributes.Add("src", loImagen.ImageUrl);

                if (((ReservaCustomerWebSite)e.Item.DataItem).COD_PRODUCTO != null)
                    loCodProducto = ((ReservaCustomerWebSite)e.Item.DataItem).COD_PRODUCTO.ToString();
                else
                    loCodProducto = string.Empty;

                if (((ReservaCustomerWebSite)e.Item.DataItem).COD_PRODUCTO_EDICION != null)
                    loCodProductoEdicion = ((ReservaCustomerWebSite)e.Item.DataItem).COD_PRODUCTO_EDICION.ToString();
                else
                    loCodProductoEdicion = string.Empty;

                HtmlButton btnEliminar = ((HtmlButton)e.Item.FindControl("btnEliminar"));

                var loCodigo = String.Format("{0}-{1}", loCodProducto, loCodProductoEdicion);

                btnEliminar.Attributes.Add("value", loCodigo);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        /*protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            ListViewDataItem loItemProducto;
            ListViewDataItem loItemProductoEdicion;

            var loCodigos = ((HtmlButton)sender).Attributes["value"];

            var loCodigosItem = loCodigos.Split('-');
            var loCodigoProducto = loCodigosItem[0];
            var loCodigoProductoEdicion = loCodigosItem[1];

            // Eliminar Producto del listado
            if (!String.IsNullOrWhiteSpace(loCodigoProducto))
            {
                loItemProducto = lsvProductos.Items.Where(x => ((Label)x.Controls[5]).Text.ToString().Equals(loCodigoProducto)).First();
                lsvProductos.Items.Remove(loItemProducto);
            }

            // Eliminar la Edición del listado
            if (!String.IsNullOrWhiteSpace(loCodigoProductoEdicion))
            {
                loItemProductoEdicion = lsvProductos.Items.Where(x => ((Label)x.Controls[7]).Text.ToString().Equals(loCodigoProductoEdicion)).First();
                lsvProductos.Items.Remove(loItemProductoEdicion);
            }

            lsvProductos.DataSource = MapListViewToListObject(lsvProductos);
            lsvProductos.DataBind();
        }*/

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
                        oProductoMod.SUBTOTAL = "$" + string.Format(System.Globalization.CultureInfo.GetCultureInfo("de-DE"), "{0:0.00}", (double.Parse(loPrecio) + double.Parse(loSubTotal)));
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
                            SUBTOTAL = oProductoCustomersWebSite.PRECIO,
                            IMAGEN = oProductoCustomersWebSite.IMAGEN
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
                        oProductoMod.SUBTOTAL = "$" + string.Format(System.Globalization.CultureInfo.GetCultureInfo("de-DE"), "{0:0.00}", (double.Parse(loPrecio) + double.Parse(loSubTotal)));
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
                            SUBTOTAL = oProdEdicionCustomersWebSite.PRECIO,
                            IMAGEN = oProdEdicionCustomersWebSite.IMAGEN
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

        private List<ReservaCustomerWebSite> MapListViewToListObject(ListView pListView)
        {
            List<ReservaCustomerWebSite> lstReservaCustomerWebSite = new List<ReservaCustomerWebSite>();

            foreach (var loItem in pListView.Items)
            {
                ReservaCustomerWebSite oReservaCustomerWebSite = new ReservaCustomerWebSite
                {
                    NOMBRE = ((Label)loItem.Controls[3]).Text,
                    COD_PRODUCTO = ((Label)loItem.Controls[5]).Text,
                    COD_PRODUCTO_EDICION = ((Label)loItem.Controls[7]).Text,
                    DESCRIPCION = ((Label)loItem.Controls[9]).Text,
                    FECHA_EDICION = ((Label)loItem.Controls[11]).Text,
                    PRECIO = ((Label)loItem.Controls[13]).Text,
                    RETIRA_LOCAL = ((RadioButton)loItem.Controls[17]).Checked,
                    ENVIO_DOMICILIO = ((RadioButton)loItem.Controls[20]).Checked,
                    CANTIDAD = Convert.ToInt32(((TextBox)loItem.Controls[24]).Text),
                    SUBTOTAL = ((Label)loItem.Controls[26]).Text
                };

                oReservaCustomerWebSite.IMAGEN = new Image();
                if (!String.IsNullOrEmpty(((HtmlImage)loItem.Controls[1]).Src))
                    oReservaCustomerWebSite.IMAGEN.ImageUrl = ((HtmlImage)loItem.Controls[1]).Src;

                lstReservaCustomerWebSite.Add(oReservaCustomerWebSite);
            }

            return lstReservaCustomerWebSite;
        }

        #endregion
    }
}