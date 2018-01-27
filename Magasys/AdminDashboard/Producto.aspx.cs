using BLL;
using NLog;
using System;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class Producto : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProveedores();
                CargarGeneros();
                CargarDiasDeSemana();
            }              
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {            
            Response.Redirect("ProductoListado.aspx");
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Métodos Privados

        private void CargarProveedores()
        {
            var oProveedor = new ProveedorBLL();

            try
            {
                ddlProveedor.DataSource = oProveedor.ObtenerProveedores();
                ddlProveedor.DataTextField = "RAZON_SOCIAL";
                ddlProveedor.DataValueField = "ID_PROVEEDOR";
                ddlProveedor.DataBind();
                ddlProveedor.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {                                
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarGeneros()
        {
            var oGenero = new GeneroBLL();

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

        private void CargarDiasDeSemana()
        {
            var oDiaSemana = new DiaSemanaBLL();

            try
            {
                var lstDiasDeSemana = oDiaSemana.ObtenerDiasDeSemana();

                ddlDiaDeEntregaRevista.DataSource = lstDiasDeSemana;
                ddlDiaDeEntregaRevista.DataTextField = "NOMBRE";
                ddlDiaDeEntregaRevista.DataValueField = "ID_DIA_SEMANA";
                ddlDiaDeEntregaRevista.DataBind();
                ddlDiaDeEntregaRevista.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                ddlDiaDeEntregaColeccion.DataSource = lstDiasDeSemana;
                ddlDiaDeEntregaColeccion.DataTextField = "NOMBRE";
                ddlDiaDeEntregaColeccion.DataValueField = "ID_DIA_SEMANA";
                ddlDiaDeEntregaColeccion.DataBind();
                ddlDiaDeEntregaColeccion.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                ddlDiaDeEntregaSuplemento.DataSource = lstDiasDeSemana;
                ddlDiaDeEntregaSuplemento.DataTextField = "NOMBRE";
                ddlDiaDeEntregaSuplemento.DataValueField = "ID_DIA_SEMANA";
                ddlDiaDeEntregaSuplemento.DataBind();
                ddlDiaDeEntregaSuplemento.Items.Insert(0, new ListItem(String.Empty, String.Empty));
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