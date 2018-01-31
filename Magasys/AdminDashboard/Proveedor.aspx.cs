using BLL;
using BLL.Common;
using NLog;
using System;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class Proveedor : System.Web.UI.Page
    {
        #region Enventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProveedorDesdeSession();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            var oProveedor = CargarProveedorDesdeControles();

            try
            {
                oProveedor.CUIT = oProveedor.CUIT.ToString().Replace("-", String.Empty).Trim();
                
                if (oProveedor.ID_PROVEEDOR == 0)
                {
                    var loProveedor = new ProveedorBLL().AltaProveedor(oProveedor);

                    if (loProveedor)
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal("Alta Proveedor", Message.MsjeProveedorSuccessAlta, "ProveedorListado.aspx"));
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal("Alta Proveedor", Message.MsjeProveedorFailure, "ProveedorListado.aspx"));
                }
                else
                {
                    var loProveedor = new ProveedorBLL().ModificarProveedor(oProveedor);

                    if (loProveedor)
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal("Modificación Proveedor", Message.MsjeProveedorSuccessModificacion, "ProveedorListado.aspx"));
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal("Modificación Proveedor", Message.MsjeProveedorFailure, "ProveedorListado.aspx"));
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            Session.Remove(Enums.Session.Proveedor.ToString());
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.Proveedor.ToString());
            Response.Redirect("ProveedorListado.aspx");
        }

        protected void DdlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProvincia.SelectedValue != String.Empty)
            {
                var idProvincia = Convert.ToInt64(ddlProvincia.SelectedValue);
                CargarLocalidades(idProvincia);
            }
            else
            {
                ddlLocalidad.Items.Clear();
            }
        }

        #endregion

        #region Métodos Privados

        private void EsModificacion()
        {
            ModificarTextoBreadcrumb();
            MostrarFilaCodigoFechaAlta();
        }

        private void ModificarTextoBreadcrumb()
        {
            lblBreadcrumbActive.Text = Message.MsjeProveedorTitulo;
        }

        private void MostrarFilaCodigoFechaAlta()
        {
            divRowCodigoFechaAlta.Attributes.Remove("style");
        }

        private void CargarProveedorDesdeSession()
        {
            try
            {
                if (Session[Enums.Session.Proveedor.ToString()] != null)
                {
                    var oProveedor = (BLL.DAL.Proveedor)Session[Enums.Session.Proveedor.ToString()];
                    CargarProvincias((long)oProveedor.ID_PROVINCIA, (long)oProveedor.ID_LOCALIDAD);

                    if (oProveedor.ID_PROVEEDOR == 0)
                    {
                        txtCuit.Text = oProveedor.CUIT;
                    }
                    else
                    {
                        EsModificacion();
                        if (oProveedor.ID_PROVEEDOR > 0)
                            txtCodigo.Text = oProveedor.ID_PROVEEDOR.ToString();
                        if (!String.IsNullOrEmpty(oProveedor.FECHA_ALTA.ToString()))
                            txtFechaAlta.Text = oProveedor.FECHA_ALTA.ToString("dd/MM/yyyy");
                        if (!String.IsNullOrEmpty(oProveedor.CUIT))
                            txtCuit.Text = Convert.ToInt64(oProveedor.CUIT).ToString("##-########-#");
                        txtRazonSocial.Text = oProveedor.RAZON_SOCIAL;
                        txtNombre.Text = oProveedor.NOMBRE.ToString();
                        txtApellido.Text = oProveedor.APELLIDO.ToString();
                        txtTelefonoMovil.Text = oProveedor.TELEFONO_MOVIL;
                        txtTelefonoFijo.Text = oProveedor.TELEFONO_FIJO;
                        txtEmail.Text = oProveedor.EMAIL;
                        txtCalle.Text = oProveedor.CALLE;
                        if (!String.IsNullOrEmpty(oProveedor.NUMERO.ToString()))
                            txtNumero.Text = oProveedor.NUMERO.ToString();
                        txtPiso.Text = oProveedor.PISO;
                        txtDepartamento.Text = oProveedor.DEPARTAMENTO;
                        txtBarrio.Text = oProveedor.BARRIO;
                        txtCodigoPostal.Text = oProveedor.CODIGO_POSTAL;
                    }
                }
                else
                    Response.Redirect("ProveedorListado.aspx");
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private BLL.DAL.Proveedor CargarProveedorDesdeControles()
        {
            var oProveedor = new BLL.DAL.Proveedor();

            if (!String.IsNullOrEmpty(txtCodigo.Text))
                oProveedor.ID_PROVEEDOR = Convert.ToInt32(txtCodigo.Text);
            else
                oProveedor.ID_PROVEEDOR = 0;

            if (!String.IsNullOrEmpty(txtFechaAlta.Text))
                oProveedor.FECHA_ALTA = Convert.ToDateTime(txtFechaAlta.Text);
            else
                oProveedor.FECHA_ALTA = DateTime.Now;

            oProveedor.CUIT = txtCuit.Text;
            oProveedor.RAZON_SOCIAL = txtRazonSocial.Text;
            oProveedor.NOMBRE = txtNombre.Text;
            oProveedor.APELLIDO = txtApellido.Text;
            oProveedor.TELEFONO_MOVIL = txtTelefonoMovil.Text;
            oProveedor.TELEFONO_FIJO = txtTelefonoFijo.Text;
            oProveedor.EMAIL = txtEmail.Text;
            oProveedor.CALLE = txtCalle.Text;
            oProveedor.NUMERO = Convert.ToInt32(txtNumero.Text);
            oProveedor.PISO = txtPiso.Text;
            oProveedor.DEPARTAMENTO = txtDepartamento.Text;
            oProveedor.BARRIO = txtBarrio.Text;
            oProveedor.CODIGO_POSTAL = txtCodigoPostal.Text;
            oProveedor.ID_PROVINCIA = Convert.ToInt32(ddlProvincia.SelectedValue);
            oProveedor.ID_LOCALIDAD = Convert.ToInt32(ddlLocalidad.SelectedValue);

            return oProveedor;
        }

        private void CargarProvincias(long idProvincia, long idLocalidad)
        {
            var oProvincia = new ProvinciaBLL();

            try
            {
                ddlProvincia.DataSource = oProvincia.ObtenerProvincias();
                ddlProvincia.DataTextField = "NOMBRE";
                ddlProvincia.DataValueField = "ID_PROVINCIA";
                ddlProvincia.DataBind();
                ddlProvincia.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                if (idProvincia == 0)
                    ddlProvincia.SelectedIndex = 0;
                else
                {
                    ddlProvincia.SelectedValue = idProvincia.ToString();
                    CargarLocalidades(idProvincia, idLocalidad);
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarLocalidades(long idProvincia, long idLocalidad = 0)
        {
            var oLocalidad = new LocalidadBLL();

            try
            {
                ddlLocalidad.DataSource = oLocalidad.ObtenerLocalidades(idProvincia);
                ddlLocalidad.DataTextField = "NOMBRE";
                ddlLocalidad.DataValueField = "ID_LOCALIDAD";
                ddlLocalidad.DataBind();
                ddlLocalidad.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                if (idLocalidad == 0)
                    ddlLocalidad.SelectedIndex = 0;
                else
                    ddlLocalidad.SelectedValue = idLocalidad.ToString();
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