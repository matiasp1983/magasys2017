using BLL.Common;
using BLL.Filters;
using NLog;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class VisualizarDeudas : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                txtFechaVentaDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFechaVentaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CargarTiposDocumento();
                CargarGrilla();
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void LsvDeudas_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loIdVenta = ((BLL.VentaListado)e.Item.DataItem).ID_VENTA.ToString();

                HtmlButton btnVisualizar = ((HtmlButton)e.Item.FindControl("btnVisualizar"));
                btnVisualizar.Attributes.Add("value", loIdVenta);

                //HiddenField hdIdVenta = ((HiddenField)e.Item.FindControl("hdIdVenta"));
                //hdIdVenta.Value = loIdVenta.ToString();

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

        private void OcultarDivsMensajes()
        {
            dvMensajeLsvDeudas.Visible = false;
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
                ddlTipoDocumento.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private VentaFiltro CargarDeudaFiltro()
        {
            VentaFiltro oVentaFiltro = null;

            if (!(!String.IsNullOrEmpty(txtFechaVentaDesde.Text) && !String.IsNullOrEmpty(txtFechaVentaHasta.Text) && (Convert.ToDateTime(txtFechaVentaDesde.Text) > Convert.ToDateTime(txtFechaVentaHasta.Text))))
            {
                oVentaFiltro = new VentaFiltro();

                oVentaFiltro.COD_ESTADO = 4; //Estado de la venta: "A Cuenta"

                if (!String.IsNullOrEmpty(txtFechaVentaDesde.Text))
                    oVentaFiltro.FECHAVENTADESDE = Convert.ToDateTime(txtFechaVentaDesde.Text);

                if (!String.IsNullOrEmpty(txtFechaVentaHasta.Text))
                    oVentaFiltro.FECHAVENTAHASTA = Convert.ToDateTime(txtFechaVentaHasta.Text);

                if (!String.IsNullOrEmpty(txtCodigoVenta.Text))
                    oVentaFiltro.ID_VENTA = Convert.ToInt32(txtCodigoVenta.Text);

                if (!String.IsNullOrEmpty(ddlTipoDocumento.SelectedValue) && !String.IsNullOrEmpty(txtNroDocumento.Text))
                {
                    oVentaFiltro.TIPO_DOCUMENTO = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
                    oVentaFiltro.NRO_DOCUMENTO = Convert.ToInt32(txtNroDocumento.Text);
                }

                if (!String.IsNullOrEmpty(txtNombre.Text))
                    oVentaFiltro.NOMBRE = txtNombre.Text;

                if (!String.IsNullOrEmpty(txtApellido.Text))
                    oVentaFiltro.APELLIDO = txtApellido.Text;

                if (!String.IsNullOrEmpty(txtAlias.Text))
                    oVentaFiltro.ALIAS = txtAlias.Text;
            }

            return oVentaFiltro;
        }

        private void CargarGrilla()
        {
            try
            {
                var oDeudaFiltro = CargarDeudaFiltro();

                if (oDeudaFiltro != null)
                {
                    var lstDeuda = new BLL.VentaBLL().ObtenerVentas(oDeudaFiltro);

                    if (lstDeuda != null && lstDeuda.Count > 0)
                    {
                        lsvDeudas.DataSource = lstDeuda;
                        lsvDeudas.Visible = true;
                    }
                    else
                    {
                        dvMensajeLsvDeudas.InnerHtml = MessageManager.Info(dvMensajeLsvDeudas, Message.MsjeVisualizarDeudasSinResultados, false);
                        dvMensajeLsvDeudas.Visible = true;
                    }
                }
                else
                {
                    dvMensajeLsvDeudas.InnerHtml = MessageManager.Info(dvMensajeLsvDeudas, Message.MsjeListadoFechaDesdeMayorQueFechaHasta, false);
                    dvMensajeLsvDeudas.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lsvDeudas.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            lsvDeudas.DataBind();
        }

        private void LimpiarCampos()
        {
            FormVisualizarDeudas.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            FormVisualizarDeudas.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            lsvDeudas.Visible = false;
        }

        #endregion
    }
}