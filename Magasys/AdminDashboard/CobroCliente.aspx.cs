using BLL;
using BLL.Common;
using BLL.DAL;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class CobroCliente : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClienteVentas();
                MostrarOcultarDivsCobro();
            }
        }

        protected void BtnCobrarTodo_Click(object sender, EventArgs e)
        {
            List<BLL.VentaListado> lstVentaListado = new List<BLL.VentaListado>();
            double loMontoTotal = 0;

            try
            {
                foreach (var loItem in lsvVentas.Items)
                {
                    BLL.VentaListado oVentaListado = new BLL.VentaListado
                    {
                        ID_VENTA = Convert.ToInt32(((Label)loItem.Controls[1]).Text.ToString()),
                        TOTAL = ((Label)loItem.Controls[5]).Text.ToString()
                    };

                    lstVentaListado.Add(oVentaListado);
                    var loTotal = oVentaListado.TOTAL.Replace('$', ' ').Trim();
                    loMontoTotal += Convert.ToInt32(loTotal);
                }

                if (loMontoTotal > 0)
                {
                    lsvCobro.DataSource = lstVentaListado;
                    lsvCobro.DataBind();
                    lblTotal.Text = loMontoTotal.ToString();
                    MostrarOcultarDivsCobro(true);
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.IdClienteCobro.ToString());
            Response.Redirect("Cobro.aspx", false);
        }

        protected void LsvVentas_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return; //Controla que los registros a procesar correspondan al cuerpo de la grilla (ItemTemplate)
                var loIdVenta = ((BLL.VentaListado)e.Item.DataItem).ID_VENTA.ToString();

                HtmlButton btnAgregar = ((HtmlButton)e.Item.FindControl("btnAgregar"));
                btnAgregar.Attributes.Add("value", loIdVenta);

                HtmlButton btnVisualizar = ((HtmlButton)e.Item.FindControl("btnVisualizar"));
                btnVisualizar.Attributes.Add("value", loIdVenta);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnAgregarItem_Click(object sender, EventArgs e)
        {
            List<BLL.VentaListado> lstVentaListado = null;
            double loMontoTotal = 0;

            try
            {
                var loIdVenta = ((HtmlButton)sender).Attributes["value"];
                lstVentaListado = new List<BLL.VentaListado>();

                if (!String.IsNullOrEmpty(lblTotal.Text))
                    loMontoTotal = Convert.ToDouble(lblTotal.Text);

                foreach (var loItem in lsvVentas.Items)
                {
                    if (((Label)loItem.Controls[1]).Text.ToString() == loIdVenta)
                    {
                        BLL.VentaListado oVentaListado = new BLL.VentaListado
                        {
                            ID_VENTA = Convert.ToInt32(((Label)loItem.Controls[1]).Text.ToString()),
                            TOTAL = ((Label)loItem.Controls[5]).Text.ToString()
                        };

                        lstVentaListado.Add(oVentaListado);
                        var loTotal = oVentaListado.TOTAL.Replace('$', ' ').Trim();
                        loMontoTotal += Convert.ToDouble(loTotal);
                        break;
                    }
                }

                if (lsvCobro.Items.Count == 0)
                {
                    lsvCobro.DataSource = lstVentaListado;
                }
                else
                {
                    var loItem = lsvCobro.Items.Where(x => ((Label)x.Controls[1]).Text.ToString().Equals(loIdVenta)).FirstOrDefault();

                    if (loItem != null)
                    { return; }

                    List<BLL.VentaListado> listViewCobro = MapListViewToListObject(lsvCobro);
                    if (listViewCobro != null)
                    {
                        lstVentaListado.ForEach(x => listViewCobro.Add(x));
                        lsvCobro.DataSource = listViewCobro;
                    }
                    else
                        return;
                }

                lsvCobro.DataBind();

                if (loMontoTotal > 0)
                {
                    lblTotal.Text = loMontoTotal.ToString();
                    MostrarOcultarDivsCobro(true);
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnVisualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Add(Enums.Session.CobroVisualizarIdVenta.ToString(), Convert.ToInt64(((HtmlButton)sender).Attributes["value"]));
                Response.Redirect("DetalleVentaProductos.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void LsvCobro_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return; //Controla que los registros a procesar correspondan al cuerpo de la grilla (ItemTemplate)
                var loIdVenta = ((BLL.VentaListado)e.Item.DataItem).ID_VENTA.ToString();

                HtmlButton btnEliminar = ((HtmlButton)e.Item.FindControl("btnEliminar"));

                btnEliminar.Attributes.Add("value", loIdVenta);
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

            var loIdVenta = ((HtmlButton)sender).Attributes["value"];

            var loItem = lsvCobro.Items.Where(x => ((Label)x.Controls[1]).Text.ToString().Equals(loIdVenta)).First();
            lsvCobro.Items.Remove(loItem);

            var loMonto = ((Label)loItem.Controls[3]).Text.Replace('$', ' ').Trim();
            loMontoTotal = loMontoTotal - Convert.ToDouble(loMonto);
            lblTotal.Text = loMontoTotal.ToString();
            lsvCobro.DataSource = MapListViewToListObject(lsvCobro);
            lsvCobro.DataBind();

            if (lsvCobro.Items.Count == 0)
                MostrarOcultarDivsCobro();
        }

        protected void BtnGuardarCobro_Click(object sender, EventArgs e)
        {
            List<BLL.DAL.DetalleCobro> lstDetalleCobro = new List<BLL.DAL.DetalleCobro>();
            BLL.DAL.Cobro oCobro = new BLL.DAL.Cobro();
            bool loResutado = false;

            oCobro.FECHA = DateTime.Now;
            oCobro.COD_ESTADO = 13; //Registrado
            oCobro.COD_CLIENTE = Convert.ToInt32(Session[Enums.Session.IdClienteCobro.ToString()]);
            oCobro.TOTAL = Convert.ToDouble(lblTotal.Text);

            foreach (var loItem in lsvCobro.Items)
            {
                BLL.DAL.DetalleCobro oDetalleCobro = new BLL.DAL.DetalleCobro()
                {
                    COD_VENTA = Convert.ToInt32(((Label)loItem.Controls[1]).Text.ToString()),
                    SUBTOTAL = Convert.ToDouble(((Label)loItem.Controls[3]).Text.ToString().Replace('$', ' ').Trim())
                };

                lstDetalleCobro.Add(oDetalleCobro);

                loResutado = new VentaBLL().PagarVenta(oDetalleCobro.COD_VENTA); // Actualizar la venta a PAGADA
                if (!loResutado)
                    break;
            }

            if (loResutado)
                loResutado = new CobroBLL().AltaCobro(oCobro, lstDetalleCobro);

            if (loResutado)
            {
                Session.Remove(Enums.Session.IdClienteCobro.ToString());
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeCobroSuccessAlta, "Alta Cobro", "Cobro.aspx"));
            }
            else
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeCobroFailure));
        }

        protected void BtnCancelarCobro_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.IdClienteCobro.ToString());
            Response.Redirect("Cobro.aspx", false);
        }

        #endregion

        #region Métodos Privados

        /// <summary>
        /// Muestra u oculta el div de cobro segun el parámetro que se le pase. Por defecto es falso. 
        /// </summary>
        /// <param name="pAcccion"></param>
        private void MostrarOcultarDivsCobro(bool pAcccion = false)
        {
            divCobro.Visible = pAcccion;
        }

        private void CargarClienteVentas()
        {
            try
            {
                if (Session[Enums.Session.IdClienteCobro.ToString()] != null)
                {
                    var loIdCliente = Convert.ToInt32(Session[Enums.Session.IdClienteCobro.ToString()]);

                    if (loIdCliente > 0)
                    {
                        using (var loRepCliente = new Repository<BLL.DAL.Cliente>())
                        {
                            var loCliente = loRepCliente.Find(p => p.ID_CLIENTE == loIdCliente);

                            if (loCliente != null)
                            {
                                txtTipoDocumento.Text = loCliente.TipoDocumento.DESCRIPCION;
                                txtNumeroDocumento.Text = loCliente.NRO_DOCUMENTO.ToString();
                                txtNombre.Text = loCliente.NOMBRE;
                                txtApellido.Text = loCliente.APELLIDO;

                                var lstVentas = new BLL.VentaBLL().ObtenerVentasACuenta(loCliente.ID_CLIENTE);
                                if (lstVentas != null && lstVentas.Count > 0)
                                {
                                    lsvVentas.DataSource = lstVentas;
                                    lsvVentas.DataBind();
                                }
                                else
                                {
                                    dvMensajeLsvVentas.InnerHtml = MessageManager.Info(dvMensajeLsvVentas, Message.MsjeListadoVentaACuentaSinResultados, false);
                                    dvMensajeLsvVentas.Visible = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private List<BLL.VentaListado> MapListViewToListObject(ListView pListView)
        {
            List<BLL.VentaListado> lstVentaListado = new List<BLL.VentaListado>();

            foreach (var loItem in pListView.Items)
            {
                BLL.VentaListado oVentaListado = new BLL.VentaListado
                {
                    ID_VENTA = Convert.ToInt32(((Label)loItem.Controls[1]).Text),
                    TOTAL = ((Label)loItem.Controls[3]).Text.ToString()
                };

                lstVentaListado.Add(oVentaListado);
            }

            return lstVentaListado;
        }

        #endregion
    }
}