using BLL;
using BLL.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
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
            {
                CargarCantidadDePedidosDesdeSession();
                ObtenerReservas();
                CargarIdUsuarioLogueado();
            }
        }

        protected void lsvProductos_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            string loCodProducto;
            string loCodProductoEdicion;
            string loProductoEdicion;

            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loImagen = ((ReservaCustomerWebSite)e.Item.DataItem).IMAGEN;
                HtmlImage imgProducto = ((HtmlImage)e.Item.FindControl("imgProducto"));
                HtmlGenericControl divFechas = ((HtmlGenericControl)e.Item.FindControl("divFechas"));

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

                if (((ReservaCustomerWebSite)e.Item.DataItem).PRODUCTO_EDICION != null)
                    loProductoEdicion = ((ReservaCustomerWebSite)e.Item.DataItem).PRODUCTO_EDICION.ToString();
                else
                    loProductoEdicion = string.Empty;

                var loCodTipoProducto = ((ReservaCustomerWebSite)e.Item.DataItem).COD_TIPO_PRODUCTO;

                if (loCodTipoProducto == 1 || loCodTipoProducto == 5)
                    divFechas.Visible = true;

                if ((loCodTipoProducto == 3 || loCodTipoProducto == 2) && loProductoEdicion == loCodProducto) // cuando se trate de la reserva de todas las COLECCIONES y REVISTAS
                {
                    HtmlGenericControl divCantidad = ((HtmlGenericControl)e.Item.FindControl("divCantidad"));
                    divCantidad.Visible = false;
                    HtmlGenericControl divPrecio = ((HtmlGenericControl)e.Item.FindControl("divPrecio"));
                    divPrecio.Visible = false;
                    HtmlGenericControl divSubtotal = ((HtmlGenericControl)e.Item.FindControl("divSubtotal"));
                    divSubtotal.Visible = false;
                    if (loCodTipoProducto == 2) // habilitamos las fechas para todas las ediciones de REVISTA
                        divFechas.Visible = true;
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

        private void ObtenerReservas()
        {
            lblCantidadItems.Text = "0";
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
                            PRODUCTO_EDICION = oProductoCustomersWebSite.COD_PRODUCTO.ToString(),
                            COD_TIPO_PRODUCTO = oProductoCustomersWebSite.COD_TIPO_PRODUCTO,
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
                            COD_PRODUCTO = oProdEdicionCustomersWebSite.COD_PRODUCTO.ToString(),
                            PRODUCTO_EDICION = oProdEdicionCustomersWebSite.COD_PRODUCTO + "_" + oProdEdicionCustomersWebSite.COD_PRODUCTO_EDICION,
                            COD_TIPO_PRODUCTO = oProdEdicionCustomersWebSite.COD_TIPO_PRODUCTO,
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

            if (Session[Enums.Session.ProductoReservaEdicionSeleccionados.ToString()] != null)
            {
                var loProductos = Session[Enums.Session.ProductoReservaEdicionSeleccionados.ToString()];
                var lstProductos = loProductos.ToString().Split(';');

                foreach (var item in lstProductos)
                {
                    var loProducto = new ProductoBLL().ObtenerProductoPorCodigo(Convert.ToInt32(item));

                    oReservaCustomerWebSite = new ReservaCustomerWebSite
                    {
                        PRODUCTO_EDICION = loProducto.ID_PRODUCTO.ToString(),
                        COD_PRODUCTO = loProducto.ID_PRODUCTO.ToString(),
                        COD_TIPO_PRODUCTO = loProducto.COD_TIPO_PRODUCTO,
                        NOMBRE = loProducto.NOMBRE,
                        DESCRIPCION = loProducto.DESCRIPCION,
                        PRECIO = string.Empty,
                        SUBTOTAL = string.Empty
                    };

                    oReservaCustomerWebSite.IMAGEN = new Image();

                    if (loProducto.Imagen != null)
                    {
                        // Covertir la iamgen a un base 64 para mostrarlo en un dato binario
                        string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(loProducto.Imagen.IMAGEN1);
                        oReservaCustomerWebSite.IMAGEN.ImageUrl = loImagenDataURL64;
                    }
                    else
                        oReservaCustomerWebSite.IMAGEN.ImageUrl = "~/AdminDashboard/img/preview_icons.png";

                    lstReservaCustomerWebSite.Add(oReservaCustomerWebSite);
                }


            }

            if (lstReservaCustomerWebSite.Count > 0)
            {   
                lblCantidadItems.Text = lstReservaCustomerWebSite.Count.ToString();
                lsvProductos.DataSource = lstReservaCustomerWebSite;
                lsvProductos.DataBind();
            }
        }

        private void CargarIdUsuarioLogueado()
        {
            if (Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()] != null)
                hfIdUsuarioLogueado.Value = ((BLL.DAL.Usuario)Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()]).ID_USUARIO.ToString();
        }

        private void CargarCantidadDePedidosDesdeSession()
        {
            Master.CantidadDePedidos = Convert.ToInt32(Session[Enums.Session.CantidadDePedidos.ToString()]);
        }

        private static void LimpiarSessiones()
        {
            if (System.Web.HttpContext.Current.Session[Enums.Session.ListadoReserva.ToString()] != null)
                System.Web.HttpContext.Current.Session.Remove(Enums.Session.ListadoReserva.ToString());

            if (System.Web.HttpContext.Current.Session[Enums.Session.ListadoReservaEdicion.ToString()] != null)
                System.Web.HttpContext.Current.Session.Remove(Enums.Session.ListadoReservaEdicion.ToString());

            if (System.Web.HttpContext.Current.Session[Enums.Session.ProductoReservaEdicionSeleccionados.ToString()] != null)
                System.Web.HttpContext.Current.Session.Remove(Enums.Session.ProductoReservaEdicionSeleccionados.ToString());

            if (System.Web.HttpContext.Current.Session[Enums.Session.CantidadDePedidos.ToString()] != null)
                System.Web.HttpContext.Current.Session.Remove(Enums.Session.CantidadDePedidos.ToString());
        }        

        //private List<ReservaCustomerWebSite> MapListViewToListObject(ListView pListView)
        //{
        //    List<ReservaCustomerWebSite> lstReservaCustomerWebSite = new List<ReservaCustomerWebSite>();

        //    foreach (var loItem in pListView.Items)
        //    {
        //        ReservaCustomerWebSite oReservaCustomerWebSite = new ReservaCustomerWebSite
        //        {
        //            NOMBRE = ((Label)loItem.Controls[3]).Text,
        //            COD_PRODUCTO = ((Label)loItem.Controls[5]).Text,
        //            COD_PRODUCTO_EDICION = ((Label)loItem.Controls[7]).Text,
        //            DESCRIPCION = ((Label)loItem.Controls[9]).Text,
        //            FECHA_EDICION = ((Label)loItem.Controls[11]).Text,
        //            PRECIO = ((Label)loItem.Controls[13]).Text,
        //            RETIRA_LOCAL = ((RadioButton)loItem.Controls[17]).Checked,
        //            ENVIO_DOMICILIO = ((RadioButton)loItem.Controls[20]).Checked,
        //            CANTIDAD = Convert.ToInt32(((TextBox)loItem.Controls[24]).Text),
        //            SUBTOTAL = ((Label)loItem.Controls[26]).Text
        //        };

        //        oReservaCustomerWebSite.IMAGEN = new Image();
        //        if (!String.IsNullOrEmpty(((HtmlImage)loItem.Controls[1]).Src))
        //            oReservaCustomerWebSite.IMAGEN.ImageUrl = ((HtmlImage)loItem.Controls[1]).Src;

        //        lstReservaCustomerWebSite.Add(oReservaCustomerWebSite);
        //    }

        //    return lstReservaCustomerWebSite;
        //}

        #endregion

        #region Métodos Públicos

        [WebMethod]
        public static int ValidarReserva(string[] pReservas)
        {
            int loResutado = 1;

            try
            {
                var loListReservas = pReservas.ToList();
                BLL.DAL.Cliente oCliente = null;

                // Validaciones
                foreach (var item in loListReservas)
                {
                    var loSplitReseva = item.Split(';');

                    if (loSplitReseva[0].ToString().Contains("IdUsuario:"))
                    {
                        oCliente = new BLL.ClienteBLL().ObtenerClientePorUsuario(Convert.ToInt32(loSplitReseva[0].ToString().Replace("IdUsuario:", string.Empty).Trim()));
                        continue;
                    }

                    if (oCliente != null)
                    {
                        if (loSplitReseva[2].ToString() == "D" && string.IsNullOrEmpty(oCliente.DIRECCION_MAPS)) // se debe controlar que el campo DIRECCION_MAPS tenga la dirección
                        {
                            //"La forma de entrega “Envío a Domicilio” requiere que el cliente complete los datos de la dirección."
                            loResutado = 2;
                            break;
                        }
                    }

                    if (Convert.ToInt32(loSplitReseva[6].ToString()) == 1 || (Convert.ToInt32(loSplitReseva[6].ToString()) == 2 && String.IsNullOrEmpty(loSplitReseva[1].ToString())) || Convert.ToInt32(loSplitReseva[6].ToString()) == 5) // Diario, todas lasediciones de Revista o Suplemento 
                    {
                        // Entonces el TIPO DE RESERVA es PERIODICA
                        if (!string.IsNullOrEmpty(loSplitReseva[5].ToString()) && Convert.ToDateTime(loSplitReseva[5].ToString()) < Convert.ToDateTime(loSplitReseva[4].ToString())) // valida si la fecha fin es menor que la fecha de inicio
                        {
                            //"La Fecha de fin debe ser mayor que la Fecha de inicio."
                            loResutado = 3;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            return loResutado;
        }

        [WebMethod]
        public static bool GuardarReserva(string[] pReservas)
        {
            bool loResutado = true;
            int loUnidadesReserva = 0;

            try
            {
                var loListReservas = pReservas.ToList();
                BLL.DAL.Cliente oCliente = null;

                foreach (var item in loListReservas)
                {
                    var loSplitReseva = item.Split(';');

                    if (loSplitReseva[0].ToString().Contains("IdUsuario:"))
                    {
                        oCliente = new BLL.ClienteBLL().ObtenerClientePorUsuario(Convert.ToInt32(loSplitReseva[0].ToString().Replace("IdUsuario:", string.Empty).Trim()));
                        continue;
                    }

                    BLL.DAL.Reserva loReserva = new BLL.DAL.Reserva()
                    {
                        FECHA = DateTime.Now,
                        COD_ESTADO = 16, //Registrada
                        COD_CLIENTE = oCliente.ID_CLIENTE,
                        COD_PRODUCTO = Convert.ToInt32(loSplitReseva[0].ToString()),
                    };

                    if (!string.IsNullOrEmpty(loSplitReseva[4].ToString()))
                        loReserva.FECHA_INICIO = Convert.ToDateTime(loSplitReseva[4].ToString());
                    else
                        loReserva.FECHA_INICIO = null;

                    if (!string.IsNullOrEmpty(loSplitReseva[5].ToString()))
                        loReserva.FECHA_FIN = Convert.ToDateTime(loSplitReseva[5].ToString());
                    else
                        loReserva.FECHA_FIN = null;

                    switch (Convert.ToInt32(loSplitReseva[6].ToString())) // Tipo de Producto
                    {
                        case 1: //Diario
                            loReserva.COD_TIPO_RESERVA = 2; //Periódica
                            break;
                        case 2: //Revista
                            if (loReserva.FECHA_INICIO == null && !String.IsNullOrEmpty(loSplitReseva[1].ToString()))
                                loReserva.COD_TIPO_RESERVA = 1; //Única
                            else
                                loReserva.COD_TIPO_RESERVA = 2; //Periódica
                            break;
                        case 3: //Colección
                            loReserva.COD_TIPO_RESERVA = 1; //Única
                            break;
                        case 4: //Libro
                            loReserva.COD_TIPO_RESERVA = 1; //Única
                            break;
                        case 5: //Suplemento
                            loReserva.COD_TIPO_RESERVA = 2; //Periódica
                            break;
                        case 6: //Película
                            loReserva.COD_TIPO_RESERVA = 1; //Única
                            break;
                    }

                    if (loSplitReseva[2].ToString() == "D")
                        loReserva.ENVIO_DOMICILIO = "X"; // Se indica que la forma de entrega es “Envío a Domicilio”

                    if (!String.IsNullOrEmpty(loSplitReseva[1].ToString())) // tiene código Producto Edición?
                    {
                        BLL.DAL.ReservaEdicion loReservaEdicion = new BLL.DAL.ReservaEdicion()
                        {
                            FECHA = DateTime.Now,
                            COD_PROD_EDICION = Convert.ToInt32(loSplitReseva[1].ToString()),
                            COD_ESTADO = 10 //Registrada
                        };

                        loReserva.ReservaEdicion.Add(loReservaEdicion);
                    }

                    loUnidadesReserva = Convert.ToInt32(loSplitReseva[3].ToString()); // Cantidad de unidades a reservar

                    for (int i = 0; i < loUnidadesReserva; i++)
                    {
                        loResutado = new ReservaBLL().AltaReserva(loReserva);
                        if (!loResutado)
                            break;
                    }

                    if (!loResutado)
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            LimpiarSessiones();

            return loResutado;
        }

        [WebMethod]
        public static bool Cancelar()
        {
            bool loResutado = false;            

            try
            {
                LimpiarSessiones();
                loResutado = true;
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            return loResutado;
        }

        #endregion
    }
}