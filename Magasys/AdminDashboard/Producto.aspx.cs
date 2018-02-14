using BLL;
using BLL.Common;
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

        protected void DdlTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            OcultarDivTipoProducto();

            switch (((DropDownList)sender).SelectedValue)
            {
                case "Revista":
                    divRevista.Visible = true;
                    break;
                case "Coleccion":
                    divColeccion.Visible = true;
                    break;
                case "Libro":
                    divLibro.Visible = true;
                    break;
                case "Suplemento":
                    divSuplemento.Visible = true;
                    break;
                case "Pelicula":
                    divPelicula.Visible = true;
                    break;
                default:
                    divDiario.Visible = true;
                    break;
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool loResutado = false;
                var oProducto = CargarProductoDesdeControles();

                switch (ddlTipoProducto.SelectedValue)
                {
                    case "Revista":
                        var oRevista = CargarRevistaDesdeControles();
                        loResutado = new RevistaBLL().AltaRevista(oProducto, oRevista);
                        break;
                    case "Coleccion":
                        break;
                    case "Libro":
                        break;
                    case "Suplemento":
                        break;
                    case "Pelicula":
                        break;
                    default:
                        break;
                }

                if (loResutado)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal("Alta Producto", Message.MsjeProductoSuccessAlta));
                    LimpiarCampos();
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal("Alta Producto", Message.MsjeProductoFailure));
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal("Alta Producto", Message.MsjeProductoFailure));

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

        private void OcultarDivTipoProducto(bool pDesdeBotonLimpiar = false)
        {
            divRevista.Visible = divColeccion.Visible = divLibro.Visible = divSuplemento.Visible = divPelicula.Visible = false;

            if (!pDesdeBotonLimpiar)
                divDiario.Visible = false;
            else
                divDiario.Visible = true;
        }

        private BLL.DAL.Producto CargarProductoDesdeControles()
        {
            var oProducto = new BLL.DAL.Producto
            {
                NOMBRE = txtNombre.Text,               
                COD_PROVEEDOR = Convert.ToInt32(ddlProveedor.SelectedValue),
                COD_GENERO = Convert.ToInt32(ddlGenero.SelectedValue),
                FECHA_ALTA = DateTime.Now,
                COD_ESTADO = 1 /*Ver para que se usa esta columna. ????*/
            };

            if (!String.IsNullOrEmpty(txtDescripcion.Text))
                oProducto.DESCRIPCION = txtDescripcion.Text;
            else
                oProducto.DESCRIPCION = null;

            return oProducto;
        }

        private BLL.DAL.Revista CargarRevistaDesdeControles()
        {
            var oRevista = new BLL.DAL.Revista
            {
                COD_PERIODICIDAD = Convert.ToInt32(ddlPeriodicidadRevista.SelectedValue),
                PRECIO = Convert.ToDouble(txtPrecioRevista.Text)                
            };

            if (!String.IsNullOrEmpty(ddlDiaDeEntregaRevista.SelectedValue))
                oRevista.ID_DIA_SEMANA = Convert.ToInt32(ddlDiaDeEntregaRevista.SelectedValue);
            else
                oRevista.ID_DIA_SEMANA = null;

            return oRevista;
        }

        private void LimpiarCampos()
        {
            FormProducto.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            FormProducto.Controls.OfType<DropDownList>().ToList().ForEach(y => y.SelectedIndex = 0);
            OcultarDivTipoProducto(true);
        }

        #endregion
    }
}