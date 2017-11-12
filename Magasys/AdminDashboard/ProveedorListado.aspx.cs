using System;
using System.Web.UI.WebControls;
using BLL.Common;
using BLL.Filters;
using System.Web.UI.HtmlControls;
using BLL;

namespace PL.AdminDashboard
{
    public partial class ProveedorListado : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                CargarGrillaProveedores();
            }
        }

        protected void BtnCrearProveedor_Click(object sender, EventArgs e)
        {
            AltaProveedor();
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrillaProveedores();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void LsvProveedores_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loIdProveedor = ((BLL.DAL.Proveedor)e.Item.DataItem).ID_PROVEEDOR.ToString();
                var loCuitProveedor = ((BLL.DAL.Proveedor)e.Item.DataItem).CUIT.ToString();

                HtmlButton btnVisualizar = ((HtmlButton)e.Item.FindControl("btnVisualizar"));
                btnVisualizar.Attributes.Add("value", loIdProveedor);

                HtmlButton btnModificar = ((HtmlButton)e.Item.FindControl("btnModificar"));
                btnModificar.Attributes.Add("value", loIdProveedor);

                HiddenField hdIdCuitProveedorBaja = ((HiddenField)e.Item.FindControl("hdIdCuitProveedorBaja"));
                hdIdCuitProveedorBaja.Value = string.Format("{0},{1}", loIdProveedor, loCuitProveedor);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.DangerModal("Error", String.Format(Message.MsjeSistemaError, ex.Message)));
            }
        }

        protected void BtnVisualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var oProveedor = new ProveedorBLL().ObtenerProveedor(Convert.ToInt64(((HtmlButton)sender).Attributes["value"]));
                Session.Add(Enums.Session.Proveedor.ToString(), oProveedor);
                Response.Redirect("ProveedorVisualizar.aspx", false);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.DangerModal("Error", String.Format(Message.MsjeSistemaError, ex.Message)));
            }
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                var oProveedor = new ProveedorBLL().ObtenerProveedor(Convert.ToInt64(((HtmlButton)sender).Attributes["value"]));
                Session.Add(Enums.Session.Proveedor.ToString(), oProveedor);
                Response.Redirect("Proveedor.aspx", false);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.DangerModal("Error", String.Format(Message.MsjeSistemaError, ex.Message)));
            }
        }

        protected void BtnBaja_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdIdProveedorBaja.Value))
                {
                    var loIdProveedor = Convert.ToInt64(hdIdProveedorBaja.Value);
                    var oProveedor = new ProveedorBLL();
                    if (oProveedor.BajaProveedor(loIdProveedor))
                    {
                        CargarGrillaProveedores();
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.DangerModal("Error", String.Format(Message.MsjeSistemaError, ex.Message)));
            }
        }

        #endregion

        #region Métodos Privados

        private void OcultarDivsMensajes()
        {
            dvMensajeCuit.Visible = false;
            dvMensajeLsvProveedores.Visible = false;
        }

        private static bool ValidaCuit(string cuit)
        {           
            if (long.Parse(cuit) <= 0) return false;
            var loDigitoCalcu = Utilities.CalcularDigitoCuit(cuit);
            var loParseSubStr = int.Parse(cuit.Substring(10));
            return loDigitoCalcu == loParseSubStr;
        }

        private void AltaProveedor()
        {
            if (string.IsNullOrEmpty(txtCuitAlta.Text))
            {
                dvMensajeCuit.InnerHtml = MessageManager.Warning(dvMensajeCuit, Message.MsjeCuitProveedorVacio);
                dvMensajeCuit.Visible = true;
            }
            else
            {
                try
                {
                    if (ValidaCuit(txtCuitAlta.Text))
                    {
                        bool esNuevoCuit = new ProveedorBLL().ConsultarExistenciaCuit(txtCuitAlta.Text);

                        if (esNuevoCuit)
                        {
                            var oProveedor = new BLL.DAL.Proveedor
                            {
                                CUIT = txtCuitAlta.Text
                            };
                            Session.Add(Enums.Session.Proveedor.ToString(), oProveedor);
                            Response.Redirect("Proveedor.aspx");
                        }
                        else
                        {
                            dvMensajeCuit.InnerHtml = MessageManager.Info(dvMensajeCuit, Message.MsjeCuitProveedorExist);
                            dvMensajeCuit.Visible = true;
                        }
                    }
                    else
                    {
                        dvMensajeCuit.InnerHtml = MessageManager.Warning(dvMensajeCuit, Message.MsjeCuitProveedorFailure);
                        dvMensajeCuit.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    dvMensajeCuit.InnerHtml = MessageManager.Warning(dvMensajeCuit, ex.Message);
                    dvMensajeCuit.Visible = true;
                }
            }
        }

        private ProveedorFiltro CargarProveedorFiltro()
        {
            var oProveedorFiltro = new ProveedorFiltro();

            if (string.IsNullOrEmpty(txtCodigo.Text))
                oProveedorFiltro.IdProveedor = 0;
            else
            {
                long loIdProveedor;
                bool loResultado = long.TryParse(txtCodigo.Text, out loIdProveedor);
                if (loResultado)
                    oProveedorFiltro.IdProveedor = loIdProveedor;
                else
                    oProveedorFiltro.IdProveedor = -1;
            }
            if (!string.IsNullOrEmpty(txtCuitBusqueda.Text))
            {
                oProveedorFiltro.Cuit = txtCuitBusqueda.Text;
            }
            if (!string.IsNullOrEmpty(txtFechaAltaDesde.Text))
            {
                oProveedorFiltro.FechaAltaDesde = Convert.ToDateTime(txtFechaAltaDesde.Text);
            }
            if (!string.IsNullOrEmpty(txtFechaAltaHasta.Text))
            {
                oProveedorFiltro.FechaAltaHasta = Convert.ToDateTime(txtFechaAltaHasta.Text);
            }
            if (!string.IsNullOrEmpty(txtRazonSocial.Text))
            {
                oProveedorFiltro.RazonSocial = txtRazonSocial.Text;
            }

            return oProveedorFiltro;
        }

        private void CargarGrillaProveedores()
        {
            try
            {
                var oProveedorFiltro = CargarProveedorFiltro();
                var lstProveedores = new ProveedorBLL().ObtenerProveedores(oProveedorFiltro);

                if (lstProveedores != null && lstProveedores.Count > 0)
                    lsvProveedores.DataSource = lstProveedores;
                else
                {
                    dvMensajeLsvProveedores.InnerHtml = MessageManager.Info(dvMensajeLsvProveedores, Message.MsjeListadoProveedorFiltrarTotalSinResultados, false);
                    dvMensajeLsvProveedores.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lsvProveedores.DataSource = null;
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.DangerModal("Error", String.Format(Message.MsjeSistemaError, ex.Message)));
            }

            lsvProveedores.DataBind();
        }

        private void Limpiar()
        {
            txtCodigo.Text = string.Empty;
            txtCuitBusqueda.Text = string.Empty;
            txtFechaAltaDesde.Text = string.Empty;
            txtFechaAltaHasta.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
            Session.Remove(Enums.Session.Proveedor.ToString());
            CargarGrillaProveedores();
        }

        #endregion        
    }
}