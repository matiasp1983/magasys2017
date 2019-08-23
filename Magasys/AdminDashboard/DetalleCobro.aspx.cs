using BLL.Common;
using BLL.DAL;
using NLog;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class DetalleCobro : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarDetalleCobro();
        }

        protected void LsvDetalleCobro_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loIdVenta = ((BLL.VentaListado)e.Item.DataItem).ID_VENTA.ToString();

                HtmlButton btnVisualizar = ((HtmlButton)e.Item.FindControl("btnVisualizar"));
                btnVisualizar.Attributes.Add("value", loIdVenta);
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
                Session.Add(Enums.Session.IdVenta.ToString(), Convert.ToInt64(((HtmlButton)sender).Attributes["value"]));
                Response.Redirect("DetalleVentaProductos.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        #endregion

        #region Métodos Privados

        private void CargarDetalleCobro()
        {
            List<BLL.VentaListado> lstVentaListado = null;
            BLL.VentaListado oVentaListado = null;
            lsvDetalleCobro.DataSource = null;
            lsvDetalleCobro.Visible = false;
            int loIdCobro = 0;

            if (Convert.ToInt32(Session[Enums.Session.IdCobro.ToString()]) > 0)
                loIdCobro = Convert.ToInt32(Session[Enums.Session.IdCobro.ToString()]);

            if (loIdCobro > 0)
            {
                using (var loRepCobro = new Repository<BLL.DAL.Cobro>())
                {
                    var loCobro = loRepCobro.Find(p => p.ID_COBRO == loIdCobro);

                    if (loCobro != null)
                    {
                        lstVentaListado = new List<BLL.VentaListado>();
                        txtCodigoCobro.Text = loCobro.ID_COBRO.ToString();
                        txtFechaCobro.Text = loCobro.FECHA.ToString("dd/MM/yyyy");
                        txtEstado.Text = loCobro.Estado.NOMBRE;
                        if (loCobro.Cliente != null)
                        {
                            txtTipoDocumento.Text = loCobro.Cliente.TipoDocumento.DESCRIPCION;
                            txtNumeroDocumento.Text = loCobro.Cliente.NRO_DOCUMENTO.ToString();
                            txtNombre.Text = loCobro.Cliente.NOMBRE.ToString();
                            txtApellido.Text = loCobro.Cliente.APELLIDO.ToString();
                        }
                        lblTotal.Text = loCobro.TOTAL.ToString();

                        foreach (var loDetalleCobro in loCobro.DetalleCobro)
                        {
                            oVentaListado = new BLL.VentaListado
                            {
                                ID_VENTA = loDetalleCobro.COD_VENTA,
                                FECHA = loDetalleCobro.Venta.FECHA,
                                FORMA_PAGO = loDetalleCobro.Venta.FormaPago.DESCRIPCION,
                                TOTAL = "$ " + loDetalleCobro.Venta.TOTAL.ToString()
                            };

                            lstVentaListado.Add(oVentaListado);
                        }

                        if (lstVentaListado.Count > 0)
                        {
                            lsvDetalleCobro.Visible = true;
                            lsvDetalleCobro.DataSource = lstVentaListado;
                            lsvDetalleCobro.DataBind();
                        }
                    }
                }
            }
        }

        #endregion
    }
}