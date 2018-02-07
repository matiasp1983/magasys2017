using BLL;
using NLog;
using System;
using System.Linq;
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
                CargarPeriodicidades();
                CargarDiarios();
                CargarAnios();
            }              
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                AltaProducto();
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }       

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {            
            Response.Redirect("ProductoListado.aspx");
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
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

        private void CargarPeriodicidades()
        {
            var oPeriodicidad = new PeriodicidadBLL();

            try
            {
                var lstPeriodicidades = oPeriodicidad.ObtenerPeriodicidades();

                ddlPeriodicidadRevista.DataSource = lstPeriodicidades;
                ddlPeriodicidadRevista.DataTextField = "NOMBRE";
                ddlPeriodicidadRevista.DataValueField = "ID_PERIODICIDAD";
                ddlPeriodicidadRevista.DataBind();
                ddlPeriodicidadRevista.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                ddlPeriodicidadColeccion.DataSource = lstPeriodicidades;
                ddlPeriodicidadColeccion.DataTextField = "NOMBRE";
                ddlPeriodicidadColeccion.DataValueField = "ID_PERIODICIDAD";
                ddlPeriodicidadColeccion.DataBind();
                ddlPeriodicidadColeccion.Items.Insert(0, new ListItem(String.Empty, String.Empty));                
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarDiarios()
        {
            var oDiario = new DiarioBLL();

            try
            {
                var lstDiarios = oDiario.ObtenerDiarios();

                ddlDiarioSuplemento.DataSource = lstDiarios;
                ddlDiarioSuplemento.DataTextField = "NOMBRE";
                ddlDiarioSuplemento.DataValueField = "ID_DIARIO";
                ddlDiarioSuplemento.DataBind();
                ddlDiarioSuplemento.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarAnios()
        {
            var oAnio = new AnioBLL();

            try
            {
                var lstAnios = oAnio.ObtenerAnios();

                ddlAnioEdicionLibro.DataSource = lstAnios;
                ddlAnioEdicionLibro.DataTextField = "DESCRIPCION";
                ddlAnioEdicionLibro.DataValueField = "DESCRIPCION";
                ddlAnioEdicionLibro.DataBind();
                ddlAnioEdicionLibro.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                ddlAnioDeEstrenoPelicula.DataSource = lstAnios;
                ddlAnioDeEstrenoPelicula.DataTextField = "DESCRIPCION";
                ddlAnioDeEstrenoPelicula.DataValueField = "DESCRIPCION";
                ddlAnioDeEstrenoPelicula.DataBind();
                ddlAnioDeEstrenoPelicula.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void AltaProducto()
        {
            throw new NotImplementedException();
        }

        private void LimpiarCampos()
        {
            FormProducto.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            FormProducto.Controls.OfType<DropDownList>().ToList().ForEach(y => y.SelectedIndex = 0);
        }

        #endregion
    }
}